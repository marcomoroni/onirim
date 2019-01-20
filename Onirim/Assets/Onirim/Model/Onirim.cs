using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Onirim : BoardGame<OnirimGameState>
{
	public Onirim() : base(new OnirimGameState(), new Step_TestStep1(), new OnirimGameModifications()) { }
}





// NEW FILE - GAMESTATE

public class OnirimGameState : GameState
{
	public List<OnirimNormalCard> allCards = new List<OnirimNormalCard>();

	public readonly int maxNoOfCardsInHand = 5;

	public List<OnirimNormalCard> mainDeck = new List<OnirimNormalCard>();
	public List<OnirimNormalCard> hand = new List<OnirimNormalCard>();
	public List<OnirimNormalCard> discardPile = new List<OnirimNormalCard>();
	public List<OnirimNormalCard> labirinth = new List<OnirimNormalCard>();
	public int usedCardsLabirinth = 0; // Number of used cards that cannot be used again to open a Door
	public List<OnirimNormalCard> obtainedDoors = new List<OnirimNormalCard>();
	public List<OnirimNormalCard> limbo = new List<OnirimNormalCard>();

	// For example
	public List<Onirim_Color> bookOfStepsLostAndFound = new List<Onirim_Color>();

	public OnirimGameState()
	{
		Debug.Log("Initalising OnirimGameState...");
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

public class Step_TestStep1 : ComputeStep<OnirimGameState>
{
	public override Stack<Step<OnirimGameState>> Execute(OnirimGameState g, GameContext<OnirimGameState> ctx)
	{
		Stack<Step<OnirimGameState>> newSteps = new Stack<Step<OnirimGameState>>();
		newSteps.Push(new Step_TestStep2());
		newSteps.Push(new Step_TestStep1());
		return newSteps;
	}
}

public class Step_TestStep2 : MoveChoiceStep<OnirimGameState>
{
	public override List<Type> allowedMoves => new List<Type>() { typeof(Move_TestMove1) };
}




// NEW FILE - MOVES

public class Move_TestMove1 : Move<OnirimGameState>
{

}

public class Move_Phase1PlayCardInLabirinth : Move<OnirimGameState>
{

}






// NEW FILE - OTHER

public enum Onirim_Category { Location, Door, Dream };
public enum Onirim_Color { Red, Blue, Green, Brown };
public enum Onirim_Symbol { Sun, Moon, Key };
public enum Onirim_Dream { Nightmare };

public class OnirimNormalCard
{
	public bool faceUp = true;

	public readonly Onirim_Category category;
	public readonly Onirim_Symbol? symbol;
	public readonly Onirim_Color? color;
	public readonly Onirim_Dream? dream;

	public OnirimNormalCard(
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