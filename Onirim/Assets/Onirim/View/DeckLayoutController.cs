using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Notes:
//    - When claiming card, also flip it to the correct position and set sorting layer
//    - Multiple views for one deck, eg:
//       - show all cards from discard
//       - select card(s) from deck
//       - when hovering hand, increase the gap between the cards
//    - TargetPoint are placed IMMEDIATELY, cards can have a delay on when to start moving

// It's a deckLAYOUTcontroller because it's not related to one single model deck,
// it should only laydown cards in a certain way

// I think floating should be done by a card, not deck, because the deck only claims
// the target pos once

public class DeckLayoutController : MonoBehaviour
{




	/*public virtual (Vector3, Quaternion) GetTransformData(int index, int totCards)
	{
		return (transform.position, transform.rotation);
	}*/







	public void ClaimTargetPoints(List<Card> deckModel, Dictionary<Card, GameObject> cardModelDictionary) // parameters?
	{
		for (int i = 0; i < deckModel.Count; i++)
		{
			Card card = deckModel[i];
			GameObject cardGameObject = cardModelDictionary[card];
			BoardGamePieceController boardGamePieceController = cardGameObject.GetComponent<BoardGamePieceController>();
			GameObject targetPoint = boardGamePieceController.targetPoint;

			// Set z render
			boardGamePieceController.SetRenderedZIndex(i);

			// Flip (if card)
			if (boardGamePieceController is CardController cardController)
			{
				if (card.faceUp) cardController.FlipFaceUp();
				else cardController.FlipFaceDown();
			}

			// TEMP: set cards in a line
			targetPoint.transform.position = transform.position + new Vector3(0.2f * i, 0);
		}
	}

}
