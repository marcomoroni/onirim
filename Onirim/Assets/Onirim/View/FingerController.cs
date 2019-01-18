using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// TODO:
//   - Control z render

public class FingerController : MonoBehaviour
{
	private Camera cam;
	private float zIndex = -3;

	private OnirimGameController controller;
	private OnirimGameView view;

	private GameObject pieceGrabbed = null;

	private void Start()
	{
		cam = Camera.main;
		controller = FindObjectOfType<OnirimGameController>();
		view = FindObjectOfType<OnirimGameView>();
	}

	private void Update()
	{
		FollowCursor();

		if (Input.GetMouseButtonDown(0))
		{
			TryToGrabCardOrClick();
		}

		if (Input.GetMouseButtonUp(0))
		{
			if (pieceGrabbed != null)
			{
				DropCard();
			}
		}
	}

	private void FollowCursor()
	{
		Vector2 cursorPosition = cam.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector3(cursorPosition.x, cursorPosition.y, zIndex);
	}

	// TODO: Attempt moves here
	private void DropCard()
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
		DropArea dropArea = null;

		foreach (RaycastHit2D hit in hits)
		{
			dropArea = hit.collider.gameObject.GetComponentInParent<DropArea>();

			// If it's a drop area stop loop
			if (dropArea != null) break;
		}

		Move move = null;

		// If piece dropped in drop area
		if (dropArea != null)
		{
			// Move depends on 
			//    - current step
			//    - droparea

			move = ComputeMove(controller.currentStep, pieceGrabbed, dropArea);
		}

		pieceGrabbed.GetComponent<BoardGamePieceController>().dropped.Invoke();
		pieceGrabbed = null;

		if (move != null)
		{
			// Attempt Move
			OnirimGameController.moveAttempt.Invoke(move, () => { }, () => { });
		}
	}

	private Move ComputeMove(Step currentStep, GameObject item, DropArea dropArea)
	{
		if (currentStep.name == StepName.Phase1_MoveChoice && dropArea.name == DropAreaName.Labirinth)
		{
			Card card = view.cardModelDictionary.FirstOrDefault(x => x.Value == item).Key;
			return new Move_Phase1_PlayCardInLabirinth(card);
		}
		else if (currentStep.name == StepName.Phase1_MoveChoice && dropArea.name == DropAreaName.DiscardPile)
		{
			Card card = view.cardModelDictionary.FirstOrDefault(x => x.Value == item).Key;
			return new Move_Phase1_DiscardCard(card);
		}
		else if (currentStep.name == StepName._ChooseDoorToObtainFromMainDeck && dropArea.name == DropAreaName.ObtainedDoors)
		{
			Card card = view.cardModelDictionary.FirstOrDefault(x => x.Value == item).Key;
			return new Move_Phase1_GetDoorAfterPlayingCardInLabirinth(card);
		}

		// No moves available
		return null;
	}

	private void TryToGrabCardOrClick()
	{
		// Check for click first, otherwise check grab

		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);

		// Click
		foreach (RaycastHit2D hit in hits)
		{
			// ...
		}

		// Grab
		// (Use sorting layer to determine which card is on top)
		GameObject cardOnTop = null;
		int highestZ = int.MinValue;
		foreach (RaycastHit2D hit in hits)
		{
			BoardGamePieceController bgpc = hit.collider.gameObject.GetComponentInParent<BoardGamePieceController>();

			// If it's a board game piece
			if (bgpc != null)
			{
				GameObject go = bgpc.gameObject;

				// If it can be grabbed
				if (bgpc.canBeGrabbed)
				{
					// If z is higher
					if(bgpc.GetRenderedZIndex() > highestZ)
					{
						cardOnTop = go;
						highestZ = bgpc.GetRenderedZIndex();
					}
				}
			}
		}
		if (cardOnTop != null)
		{
			pieceGrabbed = cardOnTop;
			cardOnTop.GetComponent<BoardGamePieceController>().grabbed.Invoke(gameObject);
		}
	}
}
