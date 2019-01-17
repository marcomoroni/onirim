using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
	private bool _faceUp = true;
	public bool faceUp { get { return _faceUp; } }

	private SpriteRenderer spriteRenderer;
	private Sprite _frontSprite;
	private Sprite _backSprite;

	public GameObject targetPoint;
	public float smoothTranslateTime = 0.1f;
	private Vector3 translateVelocity = Vector3.zero;
	public float smoothRotateTime = 0.1f;
	private float rotateVelocity = 0f;

	public bool canBeGrabbed = true; // Will be false by default

	public bool followTargetPoint = true; // [?]

	private void Awake()
	{
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}

	public void SetSprites(Sprite front, Sprite back)
	{
		_frontSprite = front;
		_backSprite = back;
		spriteRenderer.sprite = _frontSprite;
	}

	public void FlipFaceUp()
	{
		if (_frontSprite != null) spriteRenderer.sprite = _frontSprite;
		_faceUp = true;
	}

	public void FlipFaceDown()
	{
		if (_backSprite != null) spriteRenderer.sprite = _backSprite;
		_faceUp = false;
	}

	private void Update()
	{
		if (followTargetPoint)
		{
			SmoothGoToTargetPoint();
		}
	}

	// OnGrab...

	// OnGrabRelease...

	private void SmoothGoToTargetPoint()
	{
		// Translation
		float distanceToTargetPoint = Vector2.Distance(transform.position, targetPoint.transform.position);
		if (distanceToTargetPoint > 0.00001f)
		{
			// Move towards position target
			transform.position = Vector3.SmoothDamp(transform.position, targetPoint.transform.position, ref translateVelocity, smoothTranslateTime);
		}
		else if (distanceToTargetPoint > 9.99999944E-11f)
		{
			// Move to exact position
			transform.position = targetPoint.transform.position;
		}

		// Rotation
		transform.eulerAngles = transform.eulerAngles.With(z: Mathf.SmoothDampAngle(transform.eulerAngles.z, targetPoint.transform.eulerAngles.z, ref rotateVelocity, smoothRotateTime));
	}
}
