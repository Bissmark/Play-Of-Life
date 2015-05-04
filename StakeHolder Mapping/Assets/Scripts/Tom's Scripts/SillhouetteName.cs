using UnityEngine;
using System.Collections;

public class SillhouetteName : MonoBehaviour
{
	public GameObject Parent;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetComponent<UILabel> ().text = Parent.GetComponent<StakeholderManager> ().currentHolder.name;
	}
}
