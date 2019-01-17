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
			TryToGrabCard();
		}

		if (Input.GetMouseButtonUp(0))
		{
			DropCard();
		}

		if (cardGrabbed != null)
		{
			cardGrabbed.transform.position = new Vector2(transform.position.x, transform.position.y);
		}
	}

	private void FollowCursor()
	{
		Vector2 cursorPosition = cam.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector3(cursorPosition.x, cursorPosition.y, zIndex);
	}

	private void DropCard()
	{
		cardGrabbed.GetComponent<CardController>().followTargetPoint = true;
		cardGrabbed = null;
	}

	private void TryToGrabCard()
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
		Debug.Log("Hits: " + hits.Length);
		foreach (RaycastHit2D hit in hits)
		{
			cardGrabbed = hit.collider.gameObject;
			CardController deckController = cardGrabbed.GetComponent<CardController>();

			deckController.followTargetPoint = false;

			break;
		}
	}
}
