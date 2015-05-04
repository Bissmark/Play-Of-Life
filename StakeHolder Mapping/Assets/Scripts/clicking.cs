using UnityEngine;
using System.Collections;

public class clicking : MonoBehaviour 
{
	public GameObject graph;

	// Use this for initialization
	public void GraphSelection()
	{
		if(graph.active == true)
		{
			NGUITools.SetActive(graph, false);
		}
		else if(graph.active == false)
		{
			NGUITools.SetActive(graph, true);
		}
	}
}
