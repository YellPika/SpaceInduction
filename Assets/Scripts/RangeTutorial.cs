using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Transform))]
public class RangeTutorial : MonoBehaviour {
	
	Transform positionOfPlayer;
	public Transform positionOfObject;
	Vector3 checkDistance = new Vector3 (0, 0, 0);//uninitiated
	public float closeDistance = 10F;
	Vector3 checkRayForwards = new Vector3 (0, 0, 0);//uninitiated
	int myGUIWorks = 2;
	
	// Use this for initialization
	void Start () {
		positionOfPlayer = GameObject.FindWithTag("Player").transform;

	}
	// Update is called once per frame
	void Update () {
		Vector3 checkRayForwards = positionOfPlayer.TransformDirection(Vector3.forward);
		Vector3 checkRayObject = positionOfPlayer.position - positionOfObject.position;
		float angleDot = Vector3.Dot (checkRayForwards, checkRayObject);
		float checkLength = Vector3.Magnitude (positionOfPlayer.position - positionOfObject.position);
       	Debug.Log (checkLength.ToString ());
		Debug.Log (positionOfPlayer.position.ToString ());
		if (angleDot < 0.0 && checkLength < closeDistance * closeDistance)
		{
			RaycastHit hit;
			Debug.Log ("working");
			if (Physics.Raycast(positionOfObject.position, checkRayObject, out hit)){
            	if (hit.collider!= null)
				{
					Debug.Log ("The other transform is close to me!");
					myGUIWorks = 1;
					
				}
				else
				{
					Debug.Log ("Not close");
					myGUIWorks = 2;
				}
			}
		}
		else
			Debug.Log ("Not working");
			myGUIWorks = 2;
		
		
		
	}
	
	
	void OnGUI (){
		
	if (myGUIWorks == 1){
		GUI.Label (new Rect (0,0,100, 100), "Pick Up ROd");
	}
	}
	
}
	


