using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TouchHandlerGameObject : MonoBehaviour 
{
	[System.NonSerialized]
	public Vector3 prevMousePos = Vector3.zero;

	[System.NonSerialized]
	public float stationaryDuration = 0;

	void Update()
	{
			//Reset timer if mouse has moved, clicked, or released
			if (prevMousePos != Input.mousePosition || Input.GetMouseButtonDown (0) || Input.GetMouseButtonUp (0))
					stationaryDuration = Time.deltaTime;
			else
					stationaryDuration += Time.deltaTime;
	}

	void LateUpdate()
	{
		prevMousePos = Input.mousePosition;
	}
}
