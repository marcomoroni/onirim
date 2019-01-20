using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointController : MonoBehaviour
{
	public GameObject targetPointPivot;
	public GameObject targetPointAnimated;




	// Floating test

	[Header("Floating")]
	public Vector2 maxFloatingDistance;
	private Vector2 randFloating;
	private Vector2 floatingSpeed;

	private void Start()
	{
		randFloating.x = Random.Range(-3.14f, 3.14f);
		randFloating.y = Random.Range(-3.14f, 3.14f);
		floatingSpeed.x = Random.Range(1f, 1.5f);
		floatingSpeed.y = Random.Range(1f, 1.5f);
	}

	private void Update()
	{
		//targetPointAnimated.transform.position = targetPointPivot.transform.position + new Vector3(
		//	maxFloatingDistance.x * Mathf.Sin(Time.time * floatingSpeed.x + randFloating.x), 
		//	maxFloatingDistance.y * Mathf.Sin(Time.time * floatingSpeed.y + randFloating.y));
	}
}
