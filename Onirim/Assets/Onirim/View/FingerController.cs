using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerController : MonoBehaviour
{
	private Camera cam;
	private float zIndex = -3;

	private GameObject cardGrabbed = null;

	private void Start()
	{
		cam = Camera.main;
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
			if (cardGrabbed != null)
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
		cardGrabbed.GetComponent<CardController>().dropped.Invoke();
		cardGrabbed = null;
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
			GameObject go = hit.collider.gameObject.transform.parent.gameObject.transform.parent.gameObject; // (parent of parent)
			CardController cc = go.GetComponent<CardController>();

			// If it's a card (parent has CardController component)
			if (cc != null)
			{
				// If it can be grabbed
				if (cc.canBeGrabbed)
				{
					// If z is higher
					if(cc.GetRenderedZIndex() > highestZ)
					{
						cardOnTop = go;
						highestZ = cc.GetRenderedZIndex();
					}
				}
			}
		}
		if (cardOnTop != null)
		{
			cardGrabbed = cardOnTop;
			cardOnTop.GetComponent<CardController>().grabbed.Invoke(gameObject);
		}
	}
}
