using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO:
//   - Control z render

public class Finger : MonoBehaviour
{
	Onirim onirim;

	private Camera cam;
	private readonly float zIndex = -3;

	private GameObject grabbed = null;

	private void Start()
	{
		cam = Camera.main;
		onirim = FindObjectOfType<OnirimViewManager>().onirim;
	}

	private void Update()
	{
		FollowCursor();

		// Click
		if (Input.GetMouseButtonDown(0))
		{
			TryToGrab();
		}

		// Release click
		if (Input.GetMouseButtonUp(0))
		{
			if (grabbed != null)
			{
				Drop();
			}
		}
	}

	private void FollowCursor()
	{
		Vector2 cursorPosition = cam.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector3(cursorPosition.x, cursorPosition.y, zIndex);
	}

	private void TryToGrab()
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);

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
					if (bgpc.GetRenderedZIndex() > highestZ)
					{
						cardOnTop = go;
						highestZ = bgpc.GetRenderedZIndex();
					}
				}
			}
		}
		if (cardOnTop != null)
		{
			grabbed = cardOnTop;
			cardOnTop.GetComponent<BoardGamePieceController>().grabbed.Invoke(gameObject);
		}
	}

	private void Drop()
	{
		grabbed.GetComponent<BoardGamePieceController>().dropped.Invoke();
		grabbed = null;
	}
}
