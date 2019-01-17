using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// There should be a lot of events here

public class OnirimGameView : MonoBehaviour
{
	OnirimGameController controller;
	GameState gameState;

	public GameObject cardPrefab;
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

	public GameObject targetPointPrefab;

	private Dictionary<Card, GameObject> cardModelDictionary = new Dictionary<Card, GameObject>();
	private Dictionary<GameObject, GameObject> targetPointsDictionary = new Dictionary<GameObject, GameObject>();

	private void Start()
	{
		controller = GetComponent<OnirimGameController>();
	}

	public void Initialise(GameState gameState)
	{
		this.gameState = gameState;
		CreateAllCards(gameState.allCards);
		AddListeners();
	}

	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			OnirimGameController.flowContinueRequested.Invoke();
		}

		if (Input.GetKeyDown("1"))
		{
			Move_Phase1_PlayCardInLabirinth newMove = new Move_Phase1_PlayCardInLabirinth(gameState.hand[0]);
			OnirimGameController.moveAttempted.Invoke(newMove, () => { }, () => { });
		}
	}

	private void CreateAllCards(List<Card> allCards)
	{
		// Create all cards
		// (then remove the ones not in use)

		foreach(Card card in allCards)
		{
			Sprite f = spriteRedKey; // Maybe have a *wrong* default sprite
			Sprite b = spriteBack;

			switch (card.category)
			{
				case Onirim_Category.Dream:
					f = spriteNightmare;
					break;

				case Onirim_Category.Location:
					{
						Onirim_Color? color = card.color;
						Onirim_Symbol? symbol = card.symbol;

						if (color == Onirim_Color.Red && symbol == Onirim_Symbol.Sun)
						{
							f = spriteRedSun; break;
						}
						else if (color == Onirim_Color.Red && symbol == Onirim_Symbol.Moon)
						{
							f = spriteRedMoon; break;
						}
						else if (color == Onirim_Color.Red && symbol == Onirim_Symbol.Key)
						{
							f = spriteRedKey; break;
						}
						else if (color == Onirim_Color.Green && symbol == Onirim_Symbol.Sun)
						{
							f = spriteGreenSun; break;
						}
						else if (color == Onirim_Color.Green && symbol == Onirim_Symbol.Moon)
						{
							f = spriteGreenMoon; break;
						}
						else if (color == Onirim_Color.Green && symbol == Onirim_Symbol.Key)
						{
							f = spriteGreenKey; break;
						}
						else if (color == Onirim_Color.Blue && symbol == Onirim_Symbol.Sun)
						{
							f = spriteBlueSun; break;
						}
						else if (color == Onirim_Color.Blue && symbol == Onirim_Symbol.Moon)
						{
							f = spriteBlueMoon; break;
						}
						else if (color == Onirim_Color.Blue && symbol == Onirim_Symbol.Key)
						{
							f = spriteBlueKey; break;
						}
						else if (color == Onirim_Color.Brown && symbol == Onirim_Symbol.Sun)
						{
							f = spriteBrownSun; break;
						}
						else if (color == Onirim_Color.Brown && symbol == Onirim_Symbol.Moon)
						{
							f = spriteBrownMoon; break;
						}
						else if (color == Onirim_Color.Brown && symbol == Onirim_Symbol.Key)
						{
							f = spriteBrownKey; break;
						}
					}
					break;

				case Onirim_Category.Door:
					{
						Onirim_Color? color = card.color;

						switch (color)
						{
							case Onirim_Color.Red:
								f = spriteRedDoor;
								break;

							case Onirim_Color.Blue:
								f = spriteBlueDoor;
								break;

							case Onirim_Color.Green:
								f = spriteGreenDoor;
								break;

							case Onirim_Color.Brown:
								f = spriteBrownDoor;
								break;
						}
						break;
					}

				default:
					Debug.LogWarning("A card doesn't have a front sprite.");
					break;
			}

			GameObject newViewCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
			newViewCard.name = "Card";
			newViewCard.GetComponent<CardController>().SetSprites(f, b);

			// Create target point
			GameObject newTargetPoint = Instantiate(targetPointPrefab, Vector3.zero, Quaternion.identity);
			newTargetPoint.name = "Card Target Point";
			newViewCard.GetComponent<CardController>().targetPoint = newTargetPoint;

			// Save to dictionaries
			cardModelDictionary.Add(card, newViewCard);
			targetPointsDictionary.Add(newTargetPoint, newViewCard);
		}
	}

	private void AddListeners()
	{
		//OnirimGameModel.mainDeckShuffled.AddListener(OnMainDeckShuffled);
		//OnirimGameModel.cardDrawnAddedToHand.AddListener(OnCardAddedToHand);
		OnirimGameModel.stepEntered.AddListener(OnStepEntered);
		OnirimGameModel.stepExecuted.AddListener(OnStepExecuted);
		OnirimGameModel.stepExited.AddListener(OnStepExited);
	}





	private void OnStepEntered(StepName stepName)
	{

	}

	private void OnStepExecuted(StepName stepName)
	{
		switch (stepName)
		{
			case StepName.Setup_AddDrawnCardToHand:
				Debug.Log("Adding card to hand...");
				break;

			case StepName.Setup_ShuffleLimboBackIntoDeck:
				Debug.Log("Shuffling...");
				break;
		}
	}

	private void OnStepExited(StepName stepName)
	{

	}
}
