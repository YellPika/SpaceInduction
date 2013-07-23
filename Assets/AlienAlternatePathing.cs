using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Transform))]
public class AlienAlternatePathing : MonoBehaviour {
	
	public Transform posofplayer;
	public Transform posofAlien;
	public GameObject SafeAreaLVL1;
	private float Safebool;
	private bool Safetybool = false;
	
	public Transform Startingpos;
	
	// Use this for initialization
	void Start () {
	GameObject SafeAreaLVL1 = GameObject.Find("Safe Area LVL 1");
	SafetyRangeDetection safetyRangeDetection = SafeAreaLVL1.GetComponent<SafetyRangeDetection>();
	safetyRangeDetection.Safebool = 1;
	Startingpos.position = GetComponent <NavMeshAgent>().destination;
		
	}
	
	// Update is called once per frame
	void Update () {
	SafetyRangeDetection safetyRangeDetection = SafeAreaLVL1.GetComponent<SafetyRangeDetection>();
	if (safetyRangeDetection.Safebool == 1)
		{
			Safetybool = true;
			Debug.Log ("Alien will stay still");
			GetComponent <NavMeshAgent>().destination = Startingpos.position;
			
			
		}
	if (safetyRangeDetection.Safebool == 2)
		{
			Safetybool = false;
			Debug.Log ("Alien will now attack");
			GetComponent <NavMeshAgent>().destination = posofplayer.position;
			
		}
	
	}
		
	}

