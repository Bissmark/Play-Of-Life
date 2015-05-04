using UnityEngine;
using System.Collections;

public class DeleteModel : MonoBehaviour 
{
	// Update is called once per frame
	void OnTriggerEnter(Collider attachedObject) 
	{	
		Destroy(attachedObject.gameObject);
	}
}