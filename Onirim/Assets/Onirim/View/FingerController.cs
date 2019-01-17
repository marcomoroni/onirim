using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerController : MonoBehaviour
{
	private Camera cam;
	private float zIndex = -3;

	private void Start()
	{
		cam = Camera.main;

		if (Input.GetMouseButtonDown(0))
		{
			// ...
		}

	}

	private void Update()
	{
		FollowCursor();
	}

	private void FollowCursor()
	{
		Vector2 cursorPosition = cam.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector3(cursorPosition.x, cursorPosition.y, zIndex);
	}


}
