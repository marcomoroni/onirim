using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// - Reorder cards
// - Have a deck structure
//      - reordable // NO, may lead to bugs
//      - with events

public abstract class BoardGame<G> where G : GameState
{
	public readonly G gameState; // Do not change it outside
	public readonly GameContext<G> gameContext; // Do not change it outside

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
	public Stack<Step<G>> stepsToPush = new Stack<Step<G>>();
	public virtual void Execute(G g, GameContext<G> ctx) { }
	public virtual bool IsValid(G g, GameContext<G> ctx) => true;
}






public abstract class Step<G>
{

}

public abstract class MoveChoiceStep<G>  : Step<G>
{
	// All moves, not the valid ones only
	//public abstract List<Type> allowedMoves { get; }
	public abstract List<Type> GetAllowedMoves(GameContext<G> ctx); // output can be different depending on game modifications
}

public abstract class ComputeStep<G> : Step<G>
{
	public virtual bool isInstant => false;
	public Stack<Step<G>> stepsToPush = new Stack<Step<G>>();
	public virtual void Execute(G g, GameContext<G> ctx) { }
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
			Debug.LogWarning("<color=purple>Waiting for a move.</color>");
			return;
		}

		_gameContext.currentStep = stepsStack.Pop();
		Debug.Log("<color=purple>Step: <b>" + _gameContext.currentStep.GetType().Name + "</b></color>");
		// event enter state..

		switch (_gameContext.currentStep)
		{
			case ComputeStep<G> s:

				 s.Execute(_gameState, _gameContext);

				// step executed event...

				// Push back new steps
				Stack<Step<G>> newSteps = s.stepsToPush;
				while (newSteps.Count > 0)
				{
					stepsStack.Push(newSteps.Pop());
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

		// Check if move is allowed
		MoveChoiceStep<G> currentMoveChoiceStep = _gameContext.currentStep as MoveChoiceStep<G>;
		if (!currentMoveChoiceStep.GetAllowedMoves(_gameContext).Contains(move.GetType()))
		{
			Debug.LogWarning("<color=purple>Move not allowed.</color>");
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
		move.Execute(_gameState, _gameContext);

		// Push back new steps
		Stack<Step<G>> newSteps = move.stepsToPush;
		while (newSteps.Count > 0)
		{
			stepsStack.Push(newSteps.Pop());
		}

		_awaitingMove = false;
	}
}