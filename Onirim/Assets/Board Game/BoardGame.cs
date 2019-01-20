using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// G: game state

public abstract class BoardGame<G> where G : GameState
{
	public readonly G gameState;
	public readonly GameContext<G> gameContext;

	private Flow<G> _flow;

	protected BoardGame(G gameState, Step<G> firstStep, GameModifications<G> gameModifications)
	{
		this.gameState = gameState;
		this.gameContext = new GameContext<G>(gameModifications);
		_flow = new Flow<G>(this.gameState, this.gameContext, firstStep);
	}

	public void TryContinue()
	{
		_flow.TryContinue();
	}

	public void TryMove(Move<G> move)
	{
		_flow.TryMove(move);
	}
}





public abstract class GameState { }

public abstract class GameModifications<G> { }

public sealed class GameContext<G>
{
	// To be read outside and used by the boardgame class

	public Step<G> currentStep = null;
	public readonly GameModifications<G> gameModifications;

	public GameContext(GameModifications<G> gameModifications)
	{
		this.gameModifications = gameModifications;
	}
}




public abstract class Move<G>
{
	public virtual Stack<Step<G>> Execute(G g, GameContext<G> ctx) { return null; }
	public virtual bool IsValid(G g, GameContext<G> ctx) { return true; }
}






public abstract class Step<G>
{

}

public abstract class MoveChoiceStep<G> : Step<G>
{
	public abstract List<Type> allowedMoves { get; }
}

public abstract class ComputeStep<G> : Step<G>
{
	public bool isInstant { get; }
	public virtual Stack<Step<G>> Execute(G g, GameContext<G> ctx) { return null; }
}

// gameover step ?





public sealed class Flow<G>
{
	private Stack<Step<G>> stepsStack = new Stack<Step<G>>();
	private bool _awaitingMove = false;
	private G _gameState;
	private GameContext<G> _gameContext;

	public Flow(G gameState, GameContext<G> gameContext, Step<G> firstStep)
	{
		_gameState = gameState;
		_gameContext = gameContext;
		stepsStack.Push(firstStep);
	}

	public void TryContinue()
	{
		if (_awaitingMove)
		{
			Debug.LogWarning("<color=purple>Cannot continue until move is given.</color>");
			return;
		}

		_gameContext.currentStep = stepsStack.Pop();
		Debug.Log("<color=purple>Step: <b>" + _gameContext.currentStep.GetType().Name + "</b></color>");
		// event enter state..

		switch (_gameContext.currentStep)
		{
			case ComputeStep<G> s:

				Stack<Step<G>> newSteps = s.Execute(_gameState, _gameContext);

				// step executed event...

				// Push back new steps
				if (newSteps != null)
				{
					while (newSteps.Count > 0)
					{
						stepsStack.Push(newSteps.Pop());
					}
				}

				if (s.isInstant) TryContinue();

				break;

			case MoveChoiceStep<G> s:
				_awaitingMove = true;
				break;
		}
	}

	public void TryMove(Move<G> move)
	{
		if (!_awaitingMove)
		{
			Debug.LogWarning("<color=purple>Not awaiting for move.</color>");
			return;
		}

		// Check if move is valid
		if (!move.IsValid(_gameState, _gameContext))
		{
			Debug.LogWarning("<color=purple>Move not valid.</color>");
			return;
		}

		// Execute
		Debug.Log("<color=purple>Move: <b>" + move.GetType().Name + "</b></color>");
		Stack<Step<G>> newSteps = move.Execute(_gameState, _gameContext);

		// Push back new steps
		if (newSteps != null)
		{
			while (newSteps.Count > 0)
			{
				stepsStack.Push(newSteps.Pop());
			}
		}

		_awaitingMove = false;
	}
}