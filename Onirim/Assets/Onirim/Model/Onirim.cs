using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Onirim : BoardGame<OnirimGameState>
{
	public Onirim() : base(new OnirimGameState(), OnirimStepFactory.Create(OnirimSteps.Step1)) { }
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

public enum OnirimSteps
{
	Step1
}

public static class OnirimStepFactory
{
	public static Step<OnirimGameState> Create(OnirimSteps name)
	{
		switch (name)
		{
			case OnirimSteps.Step1:
				return new ComputeStep<OnirimGameState>(OnirimSteps.Step1)
				{
					executeMethod = (g) =>
					{
						Stack<Step<OnirimGameState>> newSteps = new Stack<Step<OnirimGameState>>();
						newSteps.Push(Create(OnirimSteps.Step1));
						return newSteps;
					}
				};

			default:
				return null;
		}
	}
	/*public static Dictionary<Enum, Func<OnirimGameState, Step<OnirimGameState>>> stepsFactory = new Dictionary<Enum, Func<OnirimGameState, Step<OnirimGameState>>>()
	{
		{ OnirimSteps.Step1, (g) => { return new ComputeStep<OnirimGameState>(OnirimSteps.Step1); } }
	};*/
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