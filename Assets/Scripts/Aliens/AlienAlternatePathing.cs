using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Transform))]
public class AlienAlternatePathing : MonoBehaviour {
	
	public Transform positionOfPlayer;
	public Transform positionOfAlien;
	public GameObject safeAreaLevel1;
	private float safeCheck;
	private bool safetyCheck = false;
	public Transform startingPosition;
	
	// Use this for initialization
	void Start () {
	GameObject safeAreaLevel1 = GameObject.Find("SafeAreaLevel1");
	SafetyRangeDetection safetyRangeDetection = safeAreaLevel1.GetComponent<SafetyRangeDetection>();
	safetyRangeDetection.safeCheck = 1;
	startingPosition.position = GetComponent <NavMeshAgent>().destination;
		
	}
	
	// Update is called once per frame
	void Update () {
	SafetyRangeDetection safetyRangeDetection = safeAreaLevel1.GetComponent<SafetyRangeDetection>();
	if (safetyRangeDetection.safeCheck == 1)
		{
			safetyCheck = true;
			Debug.Log ("Alien will stay still");
			if (positionOfAlien.position != startingPosition.position)
				GetComponent <NavMeshAgent>().destination = startingPosition.position;
			
			
		}
	if (safetyRangeDetection.safeCheck == 2)
		{
			safetyCheck = false;
			Debug.Log ("Alien will now attack");
			GetComponent <NavMeshAgent>().destination = positionOfPlayer.position;
			
		}
	
	}
		
	}

