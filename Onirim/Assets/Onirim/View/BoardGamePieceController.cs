using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectEvent : UnityEvent<GameObject> { }

public abstract class BoardGamePieceController : MonoBehaviour
{
	public abstract GameObjectEvent grabbed { get; }
	public abstract UnityEvent dropped { get; }
	public abstract bool canBeGrabbed { get; set; }
	public abstract int GetRenderedZIndex();
	public abstract void SetRenderedZIndex(int index);
	public abstract GameObject targetPoint { get; set; } // Should only be set when initialised
}
