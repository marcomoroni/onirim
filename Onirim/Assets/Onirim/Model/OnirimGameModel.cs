using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

// Moves execute: no events, yes push steps
// Non-instant step: no events, yes push steps
// Instant step: yes events, yes push steps

public enum Onirim_Color { Red, Blue, Green, Brown };
public enum Onirim_Symbol { Sun, Moon, Key };
public enum Onirim_Category { Location, Door, Dream };
public enum Onirim_Dream { Nightmare };

public class OnirimGameModel
{
	public GameState gameState;
	private Flow flow;

	public OnirimGameModel()
	{
		gameState = new GameState();
		flow = new Flow(gameState);
	}

	#region Events for conroller

	public class EnterMoveChoiceStep : UnityEvent<MoveChoiceStep> { }
	public static EnterMoveChoiceStep moveChoiceStepEntered = new EnterMoveChoiceStep();
	public static UnityEvent moveChoiceStepExited = new UnityEvent();

	#endregion

	#region Events for view

	public class StepEvent : UnityEvent<StepName> { }
	public static StepEvent stepEntered = new StepEvent();
	public static StepEvent stepExecuted = new StepEvent();

	#endregion

	#region Flow control for controller

	public void ContinueFlow()
	{
		flow.OnContinue();
	}

	public void ContinueFlow(Move validMove)
	{
		flow.OnMoveChosen(validMove);
	}

	#endregion
}

#region Card

public class Card
{
	public bool faceUp = true;

	public readonly Onirim_Category category;
	public readonly Onirim_Symbol? symbol;
	public readonly Onirim_Color? color;
	public readonly Onirim_Dream? dream;

	public Card(
		Onirim_Category category,
		Onirim_Symbol? symbol = null,
		Onirim_Color? color = null,
		Onirim_Dream? dream = null)
	{
		this.category = category;
		this.symbol = symbol;
		this.color = color;
		this.dream = dream;
	}
}

#endregion

#region Moves

public abstract class Move
{
	public virtual void Execute(GameState g, Stack<Step> steps) { }
	public virtual bool IsValid(GameState g) { return true; }
	//public virtual bool CanBeValid(GameState g) { return true; } // ? static, name
}

public class Move_Phase1_PlayCardInLabirinth : Move
{
	public Card card { get; }

	public Move_Phase1_PlayCardInLabirinth(Card card)
	{
		this.card = card;
	}

	public override void Execute(GameState g, Stack<Step> steps)
	{
		g.hand.Remove(card);
		g.labirinth.Add(card);

		// add step to check for door
	}

	public override bool IsValid(GameState g)
	{
		// Card has to be in hand
		bool cardIsInHand = g.hand.Contains(card);
		if (!cardIsInHand) return false;

		// Card has to be a Location
		bool cardIsLocation = card.category == Onirim_Category.Location;
		if (!cardIsLocation) return false;

		// Labirinth has be empty or its last card has to have a different symbol
		bool labirinthIsEmpty = g.labirinth.Count <= 0;
		if (labirinthIsEmpty) return true;
		bool goldenRuleRespected = card.symbol != g.labirinth.GetLastItem().symbol;
		if (goldenRuleRespected) return true;

		return false;
	}
}

public class Move_Phase1_DiscardCard : Move
{
	public Card card { get; }

	public Move_Phase1_DiscardCard(Card card)
	{
		this.card = card;
	}

	public override void Execute(GameState g, Stack<Step> steps)
	{
		if (card.symbol == Onirim_Symbol.Key)
		{
			//steps.Push(StepFactory.Create(prohpecy...))
		}
	}
}

// No no no NO NOOO
// NO
// One move per choice!

#endregion

#region GameState

public class GameState
{
	// Store all cards as a reference
	public List<Card> allCards = new List<Card>();

	public readonly int maxNoOfCardsInHand = 5;

	public Card drawn; // Later it may be a list of lists?

	public List<Card> mainDeck = new List<Card>();
	public List<Card> hand = new List<Card>();
	public List<Card> discardPile = new List<Card>();
	public List<Card> labirinth = new List<Card>();
	public int usedCardsLabirinthIndex = 0;             // This and the previous cards cannot be used for obtaining a Door
	public List<Card> obtainedDoors = new List<Card>();
	public List<Card> limbo = new List<Card>();

	public GameState()
	{
		// Generate all cards
		for (int i = 0; i < 2; i++)
		{
			allCards.Add(new Card(category: Onirim_Category.Door, color: Onirim_Color.Blue));
			allCards.Add(new Card(category: Onirim_Category.Door, color: Onirim_Color.Brown));
			allCards.Add(new Card(category: Onirim_Category.Door, color: Onirim_Color.Green));
			allCards.Add(new Card(category: Onirim_Category.Door, color: Onirim_Color.Red));
		}
		for (int i = 0; i < 9; i++)
		{
			allCards.Add(new Card(category: Onirim_Category.Location, color: Onirim_Color.Red, symbol: Onirim_Symbol.Sun));
		}
		for (int i = 0; i < 8; i++)
		{
			allCards.Add(new Card(category: Onirim_Category.Location, color: Onirim_Color.Blue, symbol: Onirim_Symbol.Sun));
		}
		for (int i = 0; i < 7; i++)
		{
			allCards.Add(new Card(category: Onirim_Category.Location, color: Onirim_Color.Green, symbol: Onirim_Symbol.Sun));
		}
		for (int i = 0; i < 6; i++)
		{
			allCards.Add(new Card(category: Onirim_Category.Location, color: Onirim_Color.Brown, symbol: Onirim_Symbol.Sun));
		}
		for (int i = 0; i < 4; i++)
		{
			allCards.Add(new Card(category: Onirim_Category.Location, color: Onirim_Color.Red, symbol: Onirim_Symbol.Moon));
			allCards.Add(new Card(category: Onirim_Category.Location, color: Onirim_Color.Blue, symbol: Onirim_Symbol.Moon));
			allCards.Add(new Card(category: Onirim_Category.Location, color: Onirim_Color.Green, symbol: Onirim_Symbol.Moon));
			allCards.Add(new Card(category: Onirim_Category.Location, color: Onirim_Color.Brown, symbol: Onirim_Symbol.Moon));
		}
		for (int i = 0; i < 3; i++)
		{
			allCards.Add(new Card(category: Onirim_Category.Location, color: Onirim_Color.Red, symbol: Onirim_Symbol.Key));
			allCards.Add(new Card(category: Onirim_Category.Location, color: Onirim_Color.Blue, symbol: Onirim_Symbol.Key));
			allCards.Add(new Card(category: Onirim_Category.Location, color: Onirim_Color.Green, symbol: Onirim_Symbol.Key));
			allCards.Add(new Card(category: Onirim_Category.Location, color: Onirim_Color.Brown, symbol: Onirim_Symbol.Key));
		}
		for (int i = 0; i < 10; i++)
		{
			allCards.Add(new Card(category: Onirim_Category.Dream, dream: Onirim_Dream.Nightmare));
		}

		// Add all cards to main deck face down
		mainDeck.AddRange(allCards);
		mainDeck.ForEach(card => card.faceUp = false);
		mainDeck.Shuffle();
	}

	public void MoveCardsFromLimboToMainDeck()
	{
		// ... maybe create extension method
	}
}

#endregion

#region Flow

#region Step names

public enum StepName
{
	Setup_ComputeInit,
	Setup_CheckIfHandIsComplete,
	Setup_DrawCard,
	Setup_ComputeWhatToDoWithDrawnCard,
	Setup_AddDrawnCardToHand,
	Setup_PutDrawnCardInLimbo,
	Setup_ComputeIfLimboNeedsToBeReshuffled,
	Setup_ShuffleLimboBackIntoDeck,

	NewTurn_ComputeInit,

	Phase1_MoveChoice,


	Phase3_ShuffleLimboBackIntoDeck
}

#endregion

#region Step types

public abstract class Step
{
	public abstract StepName name { get; }
}

public class ExecuteStep : Step
{
	private StepName _name;
	public override StepName name { get { return _name; } }
	public bool isInstant;

	public ExecuteStep(StepName name, bool isInstant = false)
	{
		_name = name;
		this.isInstant = isInstant;
	}

	public Action<GameState, Stack<Step>> executeMethod = (g, stepsStack) => { };
}

public class MoveChoiceStep : Step
{
	private StepName _name;
	public override StepName name { get { return _name; } }

	public List<Type> allowedMoves = new List<Type>();

	public MoveChoiceStep(StepName name, List<Type> allowedMoves)
	{
		_name = name;
		this.allowedMoves = allowedMoves;
	}
}

#endregion

#region Step factory

static class StepFactory
{
	public static Step Create(StepName stepName)
	{
		switch (stepName)
		{
			case StepName.Setup_ComputeInit:
				{
					ExecuteStep s = new ExecuteStep(stepName, true)
					{
						executeMethod = (g, stepsStack) =>
						{
							// Add new turn step
							stepsStack.Push(StepFactory.Create(StepName.NewTurn_ComputeInit));

							// Shuffle back Limbo
							stepsStack.Push(StepFactory.Create(StepName.Setup_ComputeIfLimboNeedsToBeReshuffled));

							// First hand
							stepsStack.Push(StepFactory.Create(StepName.Setup_CheckIfHandIsComplete));
						}
					};
					return s;
				}

			case StepName.Setup_CheckIfHandIsComplete:
				{
					ExecuteStep s = new ExecuteStep(stepName, true)
					{
						executeMethod = (g, stepsStack) =>
						{
							if (g.hand.Count < g.maxNoOfCardsInHand)
							{
								// Push new check
								stepsStack.Push(StepFactory.Create(StepName.Setup_CheckIfHandIsComplete));

								// Push draw card
								stepsStack.Push(StepFactory.Create(StepName.Setup_DrawCard));
							}
						}
					};
					return s;
				}

			case StepName.Setup_DrawCard:
				{
					ExecuteStep s = new ExecuteStep(stepName)
					{
						executeMethod = (g, stepsStack) =>
						{
							Card drawnCard = g.mainDeck.GetLastItem();
							g.mainDeck.Remove(drawnCard);
							g.drawn = drawnCard;

							stepsStack.Push(StepFactory.Create(StepName.Setup_ComputeWhatToDoWithDrawnCard));
						}
					};
					return s;
				}

			case StepName.Setup_ComputeWhatToDoWithDrawnCard:
				{
					ExecuteStep s = new ExecuteStep(stepName, true)
					{
						executeMethod = (g, stepsStack) =>
						{
							if (g.drawn.category == Onirim_Category.Location)
							{
								stepsStack.Push(StepFactory.Create(StepName.Setup_AddDrawnCardToHand));
							}
							else
							{
								stepsStack.Push(StepFactory.Create(StepName.Setup_PutDrawnCardInLimbo));
							}
						}
					};
					return s;
				}

			case StepName.Setup_AddDrawnCardToHand:
				{
					ExecuteStep s = new ExecuteStep(stepName)
					{
						executeMethod = (g, stepsStack) =>
						{
							g.hand.Add(g.drawn);
							g.drawn = null;
						}
					};
					return s;
				}

			case StepName.Setup_PutDrawnCardInLimbo:
				{
					ExecuteStep s = new ExecuteStep(stepName)
					{
						executeMethod = (g, stepsStack) =>
						{
							g.limbo.Add(g.drawn);
							g.drawn = null;
						}
					};
					return s;
				}

			case StepName.Setup_ComputeIfLimboNeedsToBeReshuffled:
				{
					ExecuteStep s = new ExecuteStep(stepName, true)
					{
						executeMethod = (g, stepsStack) =>
						{
							if (g.limbo.Count > 0)
							{
								stepsStack.Push(StepFactory.Create(StepName.Setup_ShuffleLimboBackIntoDeck));
							}
						}
					};
					return s;
				}

			case StepName.Setup_ShuffleLimboBackIntoDeck:
				{
					ExecuteStep s = new ExecuteStep(stepName)
					{
						executeMethod = (g, stepsStack) =>
						{
							g.mainDeck.AddRange(g.limbo);
							g.limbo = null;
							g.mainDeck.Shuffle();
						}
					};
					return s;
				}

			case StepName.NewTurn_ComputeInit:
				{
					ExecuteStep s = new ExecuteStep(stepName, true)
					{
						executeMethod = (g, stepsStack) =>
						{
							// TO CHANGE of course

							stepsStack.Push(StepFactory.Create(StepName.NewTurn_ComputeInit));
							stepsStack.Push(StepFactory.Create(StepName.Phase3_ShuffleLimboBackIntoDeck));
							//phase 2
							stepsStack.Push(StepFactory.Create(StepName.Phase1_MoveChoice));
						}
					};
					return s;
				}

			case StepName.Phase1_MoveChoice:
				return new MoveChoiceStep(stepName, new List<Type> { typeof(Move_Phase1_PlayCardInLabirinth), typeof(Move_Phase1_DiscardCard) });

			case StepName.Phase3_ShuffleLimboBackIntoDeck:
				{
					ExecuteStep s = new ExecuteStep(stepName)
					{
						executeMethod = (g, stepsStack) =>
						{
							g.MoveCardsFromLimboToMainDeck();
							g.mainDeck.Shuffle();
						}
					};
					return s;
				}

			default:
				return null;
		}
	}
}

#endregion

public class Flow
{
	public Stack<Step> stepsStack = new Stack<Step>();
	private GameState g;
	Step currentStep;
	private bool _awaitingMove = false;

	public Flow(GameState g)
	{
		this.g = g;
		stepsStack.Push(StepFactory.Create(StepName.Setup_ComputeInit));
	}

	public void OnContinue()
	{
		if (_awaitingMove)
		{
			Debug.LogWarning("<color=purple>Cannot continue until move is given.</color>");
			return;
		}

		currentStep = stepsStack.Pop();
		Debug.Log("<color=purple>Step: <b>" + currentStep.name + "</b></color>");
		OnirimGameModel.stepEntered.Invoke(currentStep.name);

		switch (currentStep)
		{
			case ExecuteStep s:
				s.executeMethod(g, stepsStack);
				OnirimGameModel.stepExecuted.Invoke(currentStep.name);
				if (s.isInstant) OnContinue();
				break;

			case MoveChoiceStep s:
				_awaitingMove = true;
				OnirimGameModel.moveChoiceStepEntered.Invoke(s);
				break;
		}
	}

	public void OnMoveChosen(Move move)
	{
		if (!_awaitingMove)
		{
			Debug.LogWarning("<color=purple>Not awaiting for move.</color>");
			return;
		}

		move.Execute(g, stepsStack);
		OnirimGameModel.stepExecuted.Invoke(currentStep.name);
		OnirimGameModel.moveChoiceStepExited.Invoke();
		_awaitingMove = false;
		//OnContinue();
	}
}

#endregion
