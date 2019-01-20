using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// G: game state

public abstract class BoardGame<G>
	where G : GameState
{
	private G _gameState;
	public G gameState { get { return _gameState; } }

	private GameContext _gameContext;
	public GameContext gameContext { get { return _gameContext; } }

	private Flow<G> _flow;

	protected BoardGame(G gameState, Step<G> firstStep)
	{
		_gameState = gameState;
		_flow = new Flow<G>(gameState, firstStep);
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

public sealed class GameContext
{
	// current step info, maybe without passing the step itself
}




public abstract class Move<G>
{
	public virtual Stack<Step<G>> Execute(G g) { return new Stack<Step<G>>(); }
	public virtual bool IsValid(G g) { return true; }
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
	public virtual Stack<Step<G>> Execute(G g) { return new Stack<Step<G>>(); }
}

// gameover step ?





public sealed class Flow<G>
{
	private Stack<Step<G>> stepsStack = new Stack<Step<G>>();
	private Step<G> _currentStep;
	private bool _awaitingMove = false;
	private G _gameState;

	public Flow(G gameState, Step<G> firstStep) //, Step firstStep or stepname
	{
		_gameState = gameState;
		stepsStack.Push(firstStep);
	}

	public void TryContinue()
	{
		if (_awaitingMove)
		{
			Debug.LogWarning("<color=purple>Cannot continue until move is given.</color>");
			return;
		}

		_currentStep = stepsStack.Pop();
		Debug.Log("<color=purple>Step: <b>" + _currentStep.GetType().Name + "</b></color>");
		// event enter state

		switch (_currentStep)
		{
			case ComputeStep<G> s:

				Stack<Step<G>> newSteps = s.Execute(_gameState);

				// step executed event...

				// Push back new steps
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

		// Check if move is valid
		if (!move.IsValid(_gameState))
		{
			Debug.LogWarning("<color=purple>Move not valid.</color>");
			return;
		}

		// Execute
		Stack<Step<G>> newSteps = move.Execute(_gameState);

		// Push back new steps
		while (newSteps.Count > 0)
		{
			stepsStack.Push(newSteps.Pop());
		}
	}
}