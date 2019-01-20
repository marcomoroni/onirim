using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Anything that can be grabbed and placend in decks
// It follows the offset of a target point

public abstract class BoardGamePieceController : MonoBehaviour
{
	//public readonly W wrapped { get; set; }
	public abstract GameObject targetPoint { get; set; } // Should only be set when initialised
}
