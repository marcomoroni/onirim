using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onirim : BoardGame<OnirimGameState>
{
	private OnirimGameState _gameState;
	public override OnirimGameState gameState { get { return _gameState; } }

	public Onirim() : base()
	{
		_gameState = new OnirimGameState();
	}
}

public class OnirimGameState : GameState
{
	public List<OnirimNormalCard> allCards = new List<OnirimNormalCard>();
}





public enum Onirim_Category { Location, Door, Dream };
public enum Onirim_Color { Red, Blue, Green, Brown };
public enum Onirim_Symbol { Sun, Moon, Key };
public enum Onirim_Dream { Nightmare };

public class OnirimNormalCard
{

}