using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnirimViewManager : MonoBehaviour
{
	Onirim onirim;

	private void Start()
	{
		onirim = new Onirim();
		Debug.Log("Onirm game created");
	}

	private void Update()
	{
		// TEMP
		if (Input.GetKeyDown("space"))
		{
			onirim.TryContinue();
		}
	}
}
