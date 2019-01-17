using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Notes:
//    - When claiming card, also flip it to the correct position
//    - Multiple views for one deck, eg:
//       - show all cards from discard
//       - select card(s) from deck
//       - when hovering hand, increase the gap between the cards
//    - TargetPoint are placed IMMEDIATELY, cards can have a delay on when to start moving

public enum DeckAnimation { None, Floating };

public class DeckController : MonoBehaviour
{
	public DeckAnimation deckAnimation = DeckAnimation.None;

	// Floating animation values
	public float floatingX = 1;
	public float floatingY = 1;




	/*public virtual (Vector3, Quaternion) GetTransformData(int index, int totCards)
	{
		return (transform.position, transform.rotation);
	}*/

	public List<Card> deckModel;

	// Position the target points by reading the gamestate's deck
	public void ClaimCards()
	{

	}
}
