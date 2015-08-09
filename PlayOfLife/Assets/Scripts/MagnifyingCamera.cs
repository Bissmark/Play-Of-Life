using UnityEngine;
using System.Collections;

public class MagnifyingCamera : MonoBehaviour 
{
	//int ScrollTouchID = -1;
	//Vector2 ScrollTouchOrigin;
	public Camera mainCameraRay;
	public Camera magnifyingCamera;
	public GameObject movingCamera;
	
	void Update()
	{
		
		//foreach(Touch touch in Input.touches)
		//{
			//if(touch.phase == TouchPhase.Began)
			//{
		//if(Input.GetMouseButtonDown(0))
		//{
		//	if(ScrollTouchID == -1)
		//	{
		//		if(gameObject.tag == "box1")
		//		{
		//			Debug.Log("Test");
					//
					//magnifyCamera.enabled = true;
					//magnifyingCamera = new Vector3(x, y, z);
					//ScrollTouchID = touch.fingerId;
					//ScrollTouchOrigin = touch.position;
		//		}
		//	}
		//}
		if ( Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = mainCameraRay.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast (ray, out hit, 100.0F))
			{
				if(hit.collider)
				{
					if(hit.collider.tag == "TableOfContents")
					{
						//Debug.Log("Success");
						movingCamera.transform.position = new Vector3(ray.origin.x, ray.origin.y);
						magnifyingCamera.pixelRect = new Rect(ray.origin.x, ray.origin.y, 100, 50);
					}
					else
					{
						//Debug.Log("Failed");
					}
				}
			}
		}
		
		//if((touch.phase == TouchPhase.Ended) || (touch.phase == TouchPhase.Canceled))
		//{
		//	ScrollTouchID = -1;
		//}
		//if(touch.phase == TouchPhase.Moved)
		//{
		//	if(touch.fingerId == ScrollTouchID)
		//	{
				//Vector3 CameraPos = magnifyCamera.main.transform;
				//Camera.main.transform = new Vector3(CameraPos.x, CameraPos.y + touch.deltaPosition.y, CameraPos.z);
		//	}
		//}
	}
}