using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class OnirimGameController : MonoBehaviour
{
	OnirimGameModel model;
	OnirimGameView view;
	private Step _currentStep; // only for reference for view
	public Step currentStep { get { return _currentStep; } }

	public List<Type> allowedMoves = new List<Type>();

	private void Start()
	{
		model = new OnirimGameModel();
		view = GetComponent<OnirimGameView>();

		view.Initialise(model.gameState);
		AddListeners();
	}

	private void AddListeners()
	{
		// Move choice step
		flowContinueRequested.AddListener(OnFlowContinueRequest);
		moveAttempted.AddListener(OnMoveAttempted);
		OnirimGameModel.stepEntered.AddListener(OnStepEntered);
		OnirimGameModel.stepExecuted.AddListener(OnStepExecuted);
	}

	private void OnStepEntered(Step step)
	{
		_currentStep = step;

		switch (step)
		{
			case MoveChoiceStep s:
				allowedMoves = s.allowedMoves;
				break;
		}
	}

	private void OnStepExecuted(Step step)
	{
		allowedMoves = new List<Type>();
	}

	// Function for view to continue flow
	public static UnityEvent flowContinueRequested = new UnityEvent();
	private void OnFlowContinueRequest()
	{
		model.ContinueFlow();
	}

	// Get a move from view
	public class AttemptMoveEvent : UnityEvent<Move, Action, Action> { }
	public static AttemptMoveEvent moveAttempted = new AttemptMoveEvent();
	private void OnMoveAttempted(Move move, Action callbackValid, Action callbackInvalid)
	{
		if (allowedMoves.Contains(move.GetType()) && move.IsValid(model.gameState))
		{
			callbackValid();
			model.ContinueFlow(move);
			return;
		}

		if (!allowedMoves.Contains(move.GetType())) Debug.Log("<color=teal>Move is allowed.</color>");
		if (!move.IsValid(model.gameState)) Debug.Log("<color=teal>Move is not valid.</color>");
		callbackInvalid();
	}



	#region Debug

	// For debug editor window
	public OnirimGameModel Debug_GetModel()
	{
		return model;
	}

	#endregion
}
