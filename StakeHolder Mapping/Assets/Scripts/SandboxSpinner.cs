using UnityEngine;
using System.Collections;

public class SandboxSpinner : MonoBehaviour {

	public Camera ViewCamera; /* The main camera the player sees the scene through */


	private int 			iTouchID = -1; /* The ID of the current touch. -1 denotes no current active touch */ 
	//private ObjectSpawner 	oSpawner = null; /* The spwaner object attached to the sandbox */
	private float 			fRotationalVelocity = 0;

	//void Start()
	//{
	//	oSpawner = GetComponent<ObjectSpawner> ();
	//}

	// Update is called once per frame
	void Update () 
	{
        float multiplyer = 1f;
		Ray r;
		RaycastHit info;
		//If we don't have a valid touch yet, check if there is one
		if ( iTouchID == -1 )
		{
			SimpleTouch t = new SimpleTouch();

			for ( int i = 0 ; i < TouchHandler.touchCount ; ++ i )
			{
				t = TouchHandler.GetTouch(i);
				r = ViewCamera.ScreenPointToRay(t.position);

				if (t.phase == TouchPhase.Began)
				{
					if ( Physics.Raycast(r, out info ))
					{
						iTouchID = t.fingerId;
						break;
					}
				}
			}
		}
		else
		{
            multiplyer = 5f;
			//iTouchID = -1;

			//if the touch is over, return
			if ( TouchHandler.GetTouch(iTouchID).phase == TouchPhase.Ended )
			{
				iTouchID = -1;
				return;
			}
			fRotationalVelocity = TouchHandler.GetTouch(iTouchID).deltaPosition.x / Time.deltaTime * 0.2f;
		}

		transform.Rotate(Vector3.up, fRotationalVelocity * Time.deltaTime);
        fRotationalVelocity -= fRotationalVelocity * Time.deltaTime * 2f * multiplyer;
        //Debug.Log(fRotationalVelocity);
	}
}
