using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardController : MonoBehaviour
{
	private bool _faceUp = true;
	public bool faceUp { get { return _faceUp; } }

	private SpriteRenderer spriteRenderer;
	private Sprite _frontSprite;
	private Sprite _backSprite;

	public GameObject targetPoint;
	public float smoothTranslateTime = 0.1f;
	private Vector2 translateVelocity = Vector3.zero;
	public float smoothRotateTime = 0.1f;
	public float rotateVelocity = 0f;

	[SerializeField]
	private GameObject offset;

	public class GameObjectEvent : UnityEvent<GameObject> { }
	public GameObjectEvent grabbed = new GameObjectEvent();
	public UnityEvent dropped = new UnityEvent();
	public bool canBeGrabbed = true; // TODO: Will be false by default
	private bool isGrabbed = false;
	private GameObject grabber = null;

	public class CardAnimationEvent : UnityEvent<ICardAnimation> { }
	public CardAnimationEvent claimedByDeckLayout = new CardAnimationEvent();
	ICardAnimation cardAnimation = null;
	private float randFloatingX;
	private float randFloatingY;

	private void Awake()
	{
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		grabbed.AddListener(OnGrab);
		dropped.AddListener(OnGrabRelease);
		claimedByDeckLayout.AddListener(OnClaimedByDeckLayout);

		randFloatingX = Random.Range(0.0f, 3.14f);
		randFloatingY = Random.Range(0.0f, 3.14f);
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
		SmoothGoToTargetPoint();

		AnimateCard();
	}

	private void SmoothGoToTargetPoint()
	{
		// If is grabbed instantly move to grabber pos, else smooth translation
		// Rotation is always smooth
		if (isGrabbed)
		{
			// Translation
			transform.position = grabber.transform.position;

			// Rotation
			transform.eulerAngles = transform.eulerAngles.With(z: Mathf.SmoothDampAngle(transform.eulerAngles.z, grabber.transform.eulerAngles.z, ref rotateVelocity, smoothRotateTime));
		}
		else
		{
			// Translation // TODO: check if this if statement is necessary
			float distanceToTargetPoint = Vector2.Distance(transform.position, targetPoint.transform.position);
			if (distanceToTargetPoint > 0.00001f)
			{
				// Move towards position target
				transform.position = Vector2.SmoothDamp(transform.position, targetPoint.transform.position, ref translateVelocity, smoothTranslateTime);
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




	private void OnGrab(GameObject grabber)
	{
		isGrabbed = true;
		this.grabber = grabber;
	}

	private void OnGrabRelease()
	{
		isGrabbed = false;
		this.grabber = null;
	}

	public int GetRenderedZIndex()
	{
		return spriteRenderer.sortingOrder;
	}

	public void SetRenderedZIndex(int index)
	{
		spriteRenderer.sortingOrder = index;
	}



	private void OnClaimedByDeckLayout(ICardAnimation cardAnimation)
	{
		offset.transform.position = transform.position;
		this.cardAnimation = cardAnimation;
	}

	// By moving offset
	private void AnimateCard()
	{
		switch (cardAnimation)
		{
			case CardAnimationFloating a:
				offset.transform.position = transform.position + new Vector3(a.x * Mathf.Sin(Time.time + randFloatingX), a.y * Mathf.Sin(Time.time + randFloatingY));
				break;

		}
	}
}




public interface ICardAnimation { }

public struct CardAnimationNone : ICardAnimation { }

public struct CardAnimationFloating : ICardAnimation 
{
	public float x, y;

	public CardAnimationFloating(float x, float y)
	{
		this.x = x;
		this.y = y;
	}
}