using UnityEngine;
using System.Collections;

public class Arewesafe : MonoBehaviour {
	
	public GameObject SafeAreaLVL1;
	float Safebool;
	private bool Safetybool = false;
	
	void Start ()
	{
	GameObject SafeAreaLVL1 = GameObject.Find("Safe Area LVL 1");
	SafetyRangeDetection safetyRangeDetection = SafeAreaLVL1.GetComponent<SafetyRangeDetection>();
	safetyRangeDetection.Safebool = 1;
	
	}
	
	void Update ()
	{
		
	SafetyRangeDetection safetyRangeDetection = SafeAreaLVL1.GetComponent<SafetyRangeDetection>();
	if (safetyRangeDetection.Safebool == 1)
		{
			Safetybool = true;
			Debug.Log ("Safebool = 1");
		}
	if (safetyRangeDetection.Safebool == 2)
		{
			Safetybool = false;
			Debug.Log ("Safebool = 2");
		}
	
	}
	
	
	
	
	
}
