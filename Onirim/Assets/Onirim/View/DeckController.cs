﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Notes:
// When claiming card, also flip it to the correct position

public abstract class DeckController : MonoBehaviour
{
	/*public virtual (Vector3, Quaternion) GetTransformData(int index, int totCards)
	{
		return (transform.position, transform.rotation);
	}*/

	public List<Card> deckModel;

	public void ClaimCards()
	{

	}
}
