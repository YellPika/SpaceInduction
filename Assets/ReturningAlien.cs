using UnityEngine;
using System.Collections;

public sealed class ReturningAlien : MonoBehaviour {
	
	private Transform Startingpos; 
	public Transform Alienpos;
	
	// Use this for initialization
	void Start()
	{
		Startingpos.position = GetComponent<NavMeshAgent>().destination;
	}
	void Awake ()
	{
		Alienpos.position = GetComponent<NavMeshAgent>().destination;
		if (Alienpos = Startingpos)
		{
			return;
		}
		
	}
	public void ReturningtoStart () {
		
		 	GetComponent<NavMeshAgent>().destination = Startingpos.position;
		
	}
		
}
