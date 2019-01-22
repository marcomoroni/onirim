using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnirimViewManager : MonoBehaviour
{
	public Onirim onirim;

	[Header("Prefabs")]
	public GameObject cardPrefab;
	public GameObject targetPointPrefab;

	[Header("Sprites")]
	public Sprite spriteBack;
	public Sprite spriteRedSun;
	public Sprite spriteRedMoon;
	public Sprite spriteRedKey;
	public Sprite spriteRedDoor;
	public Sprite spriteGreenSun;
	public Sprite spriteGreenMoon;
	public Sprite spriteGreenKey;
	public Sprite spriteGreenDoor;
	public Sprite spriteBlueSun;
	public Sprite spriteBlueMoon;
	public Sprite spriteBlueKey;
	public Sprite spriteBlueDoor;
	public Sprite spriteBrownSun;
	public Sprite spriteBrownMoon;
	public Sprite spriteBrownKey;
	public Sprite spriteBrownDoor;
	public Sprite spriteNightmare;

	private void Start()
	{
		onirim = new Onirim();

		GeneratePieces(onirim.gameState);
	}

	private void GeneratePieces(OnirimGameState gameState)
	{
		for (int i = 0; i < 10; i++)
		{
			GameObject newTargetPoint = Instantiate(targetPointPrefab, Vector3.zero, Quaternion.identity);
			newTargetPoint.name = targetPointPrefab.name;

			GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
			newCard.name = cardPrefab.name;
			newCard.GetComponent<CardController>().Initialize(newTargetPoint.GetComponent<TargetPointController>().targetPointAnimated, spriteNightmare, spriteBack);
		}
	}

	private void Update()
	{
		// TEMP
		if (Input.GetKeyDown("space"))
		{
			onirim.TryContinue();
		}
		if (Input.GetKeyDown("1"))
		{
			onirim.TryMove(new MoveTEMP_TestMove1());
		}
		if (Input.GetKeyDown("2"))
		{
			onirim.TryMove(new MoveTEMP_TestMove2());
		}
	}
}

public class GameObjectEvent : UnityEvent<GameObject> { }