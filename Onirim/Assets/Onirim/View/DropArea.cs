using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropAreaName { Labirinth, DiscardPile, ObtainedDoors }

[RequireComponent(typeof(BoxCollider2D))]
public class DropArea : MonoBehaviour
{
	public DropAreaName name;

	private BoxCollider2D boxCollider2D;

	private void Start()
	{
		boxCollider2D = GetComponent<BoxCollider2D>();
	}

	private void OnDrawGizmos()
	{
		BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>(); // see if there's a better way
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, Vector3.zero.With(x: boxCollider2D.size.x, y: boxCollider2D.size.y));
	}
}
