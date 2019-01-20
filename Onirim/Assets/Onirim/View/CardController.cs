using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : BoardGamePieceController
{
	private bool _faceUp = true;
	public bool faceUp { get { return _faceUp; } }

	private SpriteRenderer spriteRenderer;
	private Sprite _frontSprite;
	private Sprite _backSprite;

	private void Awake()
	{
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}

	public void Initialize(GameObject targetPoint, Sprite front, Sprite back)
	{
		Initialize(targetPoint);

		_frontSprite = front;
		_backSprite = back;
		spriteRenderer.sprite = _frontSprite;

		base.grabbed.AddListener(OnGrab);
		base.dropped.AddListener(OnDrop);
	}

	public void Flip(bool faceUp)
	{
		if (faceUp)
		{
			if (_frontSprite != null) spriteRenderer.sprite = _frontSprite;
			_faceUp = true;
		}
		else
		{
			if (_backSprite != null) spriteRenderer.sprite = _backSprite;
			_faceUp = false;
		}
	}

	private void OnGrab(GameObject grabber)
	{

	}

	private void OnDrop()
	{

	}

	public override int GetRenderedZIndex()
	{
		return spriteRenderer.sortingOrder; ;
	}
}