using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Anything that can be grabbed and placend in decks
// It follows the offset of a target point

public abstract class BoardGamePieceController : MonoBehaviour
{
	//public readonly W wrapped { get; set; }
	public GameObject targetPoint { get; set; } // Should only be set when initialised

	public float smoothTranslateTime = 0.1f;
	private Vector2 translateVelocity = Vector3.zero;
	public float smoothRotateTime = 0.1f;
	private float rotateVelocity = 0f;

	public GameObjectEvent grabbed { get; } = new GameObjectEvent();
	public UnityEvent dropped { get; } = new UnityEvent();
	public bool canBeGrabbed = true;
	protected bool isGrabbed = false;
	protected GameObject grabber = null;

	protected void Initialize(GameObject targetPoint)
	{
		this.targetPoint = targetPoint;

		grabbed.AddListener(OnGrab);
		dropped.AddListener(OnDrop);
	}

	protected void Update()
	{
		if (isGrabbed)
		{
			SmoothFollow(grabber, false);
		}
		else
		{
			SmoothFollow(targetPoint);
		}
	}

	private void SmoothFollow(GameObject target, bool smoothTranslation = true, bool smoothRotation = true)
	{
		// Translation
		if (smoothTranslation)
		{
			float distanceToTargetPoint = Vector2.Distance(transform.position, target.transform.position);
			if (distanceToTargetPoint > 0.00001f)
			{
				// Move towards position target
				transform.position = Vector2.SmoothDamp(transform.position, target.transform.position, ref translateVelocity, smoothTranslateTime);
			}
			else if (distanceToTargetPoint > 9.99999944E-11f)
			{
				// Move to exact position
				transform.position = target.transform.position;
			}
		}
		else
		{

			transform.position = target.transform.position;
		}

		// Rotation
		if (smoothRotation)
		{
			float deltaAgles = Mathf.Abs(transform.eulerAngles.z - target.transform.eulerAngles.z);
			if (deltaAgles > 3f)
				transform.eulerAngles = transform.eulerAngles.With(z: Mathf.SmoothDampAngle(transform.eulerAngles.z, target.transform.eulerAngles.z, ref rotateVelocity, smoothRotateTime));
			else if (deltaAgles > 0.1f)
				transform.eulerAngles = target.transform.eulerAngles;
		}
		else
		{
			transform.eulerAngles = target.transform.eulerAngles;
		}
	}

	private void OnGrab(GameObject grabber)
	{
		isGrabbed = true;
		this.grabber = grabber;
	}

	private void OnDrop()
	{
		isGrabbed = false;
		this.grabber = null;
	}

	public virtual int GetRenderedZIndex()
	{
		return 1;
	}
}
