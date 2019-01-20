using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Onirim : BoardGame<OnirimGameState>
{
	public Onirim() : base(new OnirimGameState(), new Step_TestStep1()) { }
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
	public int usedCardsLabirinthIndex = 0;             // This and the previous cards cannot be used for obtaining a Door
	public List<OnirimNormalCard> obtainedDoors = new List<OnirimNormalCard>();
	public List<OnirimNormalCard> limbo = new List<OnirimNormalCard>();

	public OnirimGameState()
	{
		Debug.Log("Initalising OnirimGameState...");
	}
}





// NEW FILE - STEPS

public class Step_TestStep1 : ComputeStep<OnirimGameState>
{
	public override Stack<Step<OnirimGameState>> Execute(OnirimGameState g)
	{
		Stack<Step<OnirimGameState>> newSteps = new Stack<Step<OnirimGameState>>();
		newSteps.Push(new Step_TestStep1());
		return newSteps;
	}
}




// NEW FILE - MOVES

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

}