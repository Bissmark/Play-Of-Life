using UnityEngine;
using System.Collections;

public class MovingPersonOutput : MonoBehaviour 
{
	private Vector3 screenPoint;
	public GameObject outputActive;
	
	void OnMouseDown()
	{
		if(outputActive)
		{
			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		}
	}
	
	void OnDrag()
	{
		if(outputActive)
		{
			Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint);
			transform.position = currentPos;
		}
	}
	
	//void Update() 
	//{
	//	if(Input.GetMouseButtonDown(0))
	//	{
	//		OnMouseDrag();
	//	}
	//	else
	//	{
	//		OnMouseDown();
	//	}
	//}
}