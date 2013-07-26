using UnityEngine;
using System.Collections;

public class Arewesafe : MonoBehaviour {
	
	public GameObject safeAreaLevel1;
	private float safeCheck;
	private bool safetyCheck = false;
	
	void Start ()
	{
	GameObject safeAreaLeveL1 = GameObject.Find("SafeAreaLeveL1");
	SafetyRangeDetection safetyRangeDetection = safeAreaLevel1.GetComponent<SafetyRangeDetection>();
	safetyRangeDetection.safeCheck = 1;
	
	}
	
	void Update ()
	{
		
	SafetyRangeDetection safetyRangeDetection = safeAreaLevel1.GetComponent<SafetyRangeDetection>();
	if (safetyRangeDetection.safeCheck == 1)
		{
			safetyCheck = true;
			Debug.Log ("safeCheck = 1");
		}
	if (safetyRangeDetection.safeCheck == 2)
		{
			safetyCheck = false;
			Debug.Log ("safeCheck = 2");
		}
	
	}
	
	
	
	
	
}
