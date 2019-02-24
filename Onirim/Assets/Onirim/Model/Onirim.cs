using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Onirim : BoardGame<OnirimGameState>
{
	public Onirim() : base(new OnirimGameState(), new Step_InitialiseTurn(), new OnirimGameModifications()) { }
}





// NEW FILE - GAMESTATE AND MODIFICATIONS

public class OnirimGameState : GameState
{
	public List<OnirimCard> allCards = new List<OnirimCard>();

	public readonly int maxNoOfCardsInHand = 5;

	public List<OnirimCard> mainDeck = new List<OnirimCard>();
	public List<OnirimCard> hand = new List<OnirimCard>();
	public List<OnirimCard> discardPile = new List<OnirimCard>();
	public List<OnirimCard> labirinth = new List<OnirimCard>();
	public int usedCardsLabirinth = 0; // Number of used cards that cannot be used again to open a Door
	public List<OnirimCard> obtainedDoors = new List<OnirimCard>();
	public List<OnirimCard> limbo = new List<OnirimCard>();

	// Only add all pieces to ONE list
	public OnirimGameState()
	{
		// Generate all cards
		for (int i = 0; i < 2; i++)
		{
			allCards.Add(new OnirimCard(category: Onirim_Category.Door, color: Onirim_Color.Blue));
			allCards.Add(new OnirimCard(category: Onirim_Category.Door, color: Onirim_Color.Brown));
			allCards.Add(new OnirimCard(category: Onirim_Category.Door, color: Onirim_Color.Green));
			allCards.Add(new OnirimCard(category: Onirim_Category.Door, color: Onirim_Color.Red));
		}
		for (int i = 0; i < 9; i++)
		{
			allCards.Add(new OnirimCard(category: Onirim_Category.Location, color: Onirim_Color.Red, symbol: Onirim_Symbol.Sun));
		}
		for (int i = 0; i < 8; i++)
		{
			allCards.Add(new OnirimCard(category: Onirim_Category.Location, color: Onirim_Color.Blue, symbol: Onirim_Symbol.Sun));
		}
		for (int i = 0; i < 7; i++)
		{
			allCards.Add(new OnirimCard(category: Onirim_Category.Location, color: Onirim_Color.Green, symbol: Onirim_Symbol.Sun));
		}
		for (int i = 0; i < 6; i++)
		{
			allCards.Add(new OnirimCard(category: Onirim_Category.Location, color: Onirim_Color.Brown, symbol: Onirim_Symbol.Sun));
		}
		for (int i = 0; i < 4; i++)
		{
			allCards.Add(new OnirimCard(category: Onirim_Category.Location, color: Onirim_Color.Red, symbol: Onirim_Symbol.Moon));
			allCards.Add(new OnirimCard(category: Onirim_Category.Location, color: Onirim_Color.Blue, symbol: Onirim_Symbol.Moon));
			allCards.Add(new OnirimCard(category: Onirim_Category.Location, color: Onirim_Color.Green, symbol: Onirim_Symbol.Moon));
			allCards.Add(new OnirimCard(category: Onirim_Category.Location, color: Onirim_Color.Brown, symbol: Onirim_Symbol.Moon));
		}
		for (int i = 0; i < 3; i++)
		{
			allCards.Add(new OnirimCard(category: Onirim_Category.Location, color: Onirim_Color.Red, symbol: Onirim_Symbol.Key));
			allCards.Add(new OnirimCard(category: Onirim_Category.Location, color: Onirim_Color.Blue, symbol: Onirim_Symbol.Key));
			allCards.Add(new OnirimCard(category: Onirim_Category.Location, color: Onirim_Color.Green, symbol: Onirim_Symbol.Key));
			allCards.Add(new OnirimCard(category: Onirim_Category.Location, color: Onirim_Color.Brown, symbol: Onirim_Symbol.Key));
		}
		for (int i = 0; i < 10; i++)
		{
			allCards.Add(new OnirimCard(category: Onirim_Category.Dream, dream: Onirim_Dream.Nightmare));
		}

		allCards.Shuffle();
	}
}

public class OnirimGameModifications : GameModifications<OnirimGameState>
{
	public readonly bool theBookOfStepsLostAndFound;
	public readonly bool theGlyphs;
	public readonly bool theDreamcatchers;
	public readonly bool theTowers;
	public readonly bool happyDreamsAndDarkPremonitions;
	public readonly bool crossroadsAndDeadEnds;
	public readonly bool theDoorToTheOniverse;
	public readonly bool sphinxDiverAndConfusion;
	public readonly bool theMirrors;

	public OnirimGameModifications(
		bool theBookOfStepsLostAndFound = false,
		bool theGlyphs = false,
		bool theDreamcatchers = false,
		bool theTowers = false,
		bool happyDreamsAndDarkPremonitions = false,
		bool crossroadsAndDeadEnds = false,
		bool theDoorToTheOniverse = false,
		bool sphinxDiverAndConfusion = false,
		bool theMirrors = false
		)
	{
		this.theBookOfStepsLostAndFound = theBookOfStepsLostAndFound;
		this.theGlyphs = theGlyphs;
		this.theDreamcatchers = theDreamcatchers;
		this.theTowers = theTowers;
		this.happyDreamsAndDarkPremonitions = happyDreamsAndDarkPremonitions;
		this.crossroadsAndDeadEnds = crossroadsAndDeadEnds;
		this.theDoorToTheOniverse = theDoorToTheOniverse;
		this.sphinxDiverAndConfusion = sphinxDiverAndConfusion;
		this.theMirrors = theMirrors;
	}
}





// NEW FILE - STEPS

public class Step_InitialiseTurn : ComputeStep<OnirimGameState>
{
	public override bool IsInstant => true;

	public override void Execute(OnirimGameState g, GameContext<OnirimGameState> ctx)
	{
		stepsToPush.Push(new Step_InitialisePhase1());
		stepsToPush.Push(new StepTEMP_MoveChoiceTest());
		stepsToPush.Push(new Step_InitialisePhase2());
		stepsToPush.Push(new Step_InitialisePhase3());
		stepsToPush.Push(new Step_InitialiseTurn());
	}
}

public class Step_InitialisePhase1 : ComputeStep<OnirimGameState>
{

}

public class Step_InitialisePhase2 : ComputeStep<OnirimGameState>
{

}

public class Step_InitialisePhase3 : ComputeStep<OnirimGameState>
{

}

public class Step_Phase1 : MoveChoiceStep<OnirimGameState>
{
	public override List<Type> GetAllowedMoves(GameContext<OnirimGameState> ctx)
	{
		return new List<Type> { typeof(Move_PlayCardInLabirinth) };
	}
}

//////

public class StepTEMP_MoveChoiceTest : MoveChoiceStep<OnirimGameState>
{
	public override List<Type> GetAllowedMoves(GameContext<OnirimGameState> ctx)
	{
		return new List<Type> { typeof(MoveTEMP_TestMove1) };
	}
}

public class StepTEMP_ChooseDoorFromMainDeck : MoveChoiceStep<OnirimGameState>
{
	public override List<Type> GetAllowedMoves(GameContext<OnirimGameState> ctx)
	{
		return new List<Type> { typeof(MoveTEMP_ChooseDoorFromMainDeck) };
	}

	public StepTEMP_ChooseDoorFromMainDeck(Onirim_Color color)
	{
		parameters["color"] = color;
	}
}




// NEW FILE - MOVES

public class Move_PlayCardInLabirinth : Move<OnirimGameState>
{
	public readonly OnirimCard card;

	public Move_PlayCardInLabirinth(OnirimCard card)
	{
		this.card = card;
	}

	public override bool IsValid(OnirimGameState g, GameContext<OnirimGameState> ctx)
	{
		// TODO
		return true;
	}

	public override void Execute(OnirimGameState g, GameContext<OnirimGameState> ctx)
	{
		// Remove card from initial collection (most likely hand)...

		g.labirinth.Add(card);

		// ...
	}
}

//////

public class MoveTEMP_TestMove1 : Move<OnirimGameState>
{

}

public class MoveTEMP_TestMove2 : Move<OnirimGameState>
{

}

public class MoveTEMP_ChooseDoorFromMainDeck : Move<OnirimGameState>
{
	public readonly OnirimCard card;

	public override bool IsValid(OnirimGameState g, GameContext<OnirimGameState> ctx)
	{
		if (card.color == (Onirim_Color)ctx.currentStep.parameters["color"]) // ...
			return true;

		return false;
	}

	public MoveTEMP_ChooseDoorFromMainDeck(OnirimCard card)
	{
		this.card = card;
	}
}






// NEW FILE - OTHER

public enum Onirim_Category { Location, Door, Dream };
public enum Onirim_Color { Red, Blue, Green, Brown };
public enum Onirim_Symbol { Sun, Moon, Key };
public enum Onirim_Dream { Nightmare };

public class OnirimCard
{
	public bool faceUp = true;

	public readonly Onirim_Category category;
	public readonly Onirim_Symbol? symbol;
	public readonly Onirim_Color? color;
	public readonly Onirim_Dream? dream;

	public OnirimCard(
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