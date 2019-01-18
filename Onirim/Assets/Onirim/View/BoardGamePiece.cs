using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectEvent : UnityEvent<GameObject> { }

public abstract class BoardGamePiece : MonoBehaviour
{
	public abstract GameObjectEvent grabbed { get; }
	public abstract UnityEvent dropped { get; }
	public abstract bool canBeGrabbed { get; set; }
	public abstract int GetRenderedZIndex();
}
