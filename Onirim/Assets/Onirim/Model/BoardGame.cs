using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardGame<G>
	where G : GameState
{
	public abstract G gameState { get; }

	private GameContext _gameContext;
	public GameContext gameContext { get { return _gameContext; } }

	private Flow<G> _flow;

	protected BoardGame() //, first step
	{
		_flow = new Flow<G>(gameState);
	}

	public bool TryContinue()
	{
		return _flow.TryContinue();
	}

	public bool TryMove(Move<G> move)
	{
		return _flow.TryMove(move);
	}
}

public abstract class GameState
{

}

public sealed class GameContext
{
	// current step info, maybe without passing the step itself
}

public abstract class Move<G>
{
	// Pop here in the Execute() the steps to push
	public Stack<Step> stepsToPush;

	public virtual void Execute(G g) { }
	public virtual bool IsValid(G g) { return true; }
	
}

public sealed class Flow<G>
{
	public Stack<Step> stepsStack = new Stack<Step>();
	private Step _currentStep;
	private bool _awaitingMove = false;
	private G _gameState;

	public Flow(G gameState)
	{
		_gameState = gameState;
		// first step...
	}

	public bool TryContinue()
	{
		return true;
	}

	public bool TryMove(Move<G> move)
	{
		return true;
	}

	private void TransitionToNextStep()
	{

	}
}

public abstract class Step
{
	//public abstract StepName name { get; }
}