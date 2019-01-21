using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DropArea : MonoBehaviour
{
	private BoxCollider2D _boxCollider2D;

	private void Start()
	{
		_boxCollider2D = GetComponent<BoxCollider2D>();
	}
}
