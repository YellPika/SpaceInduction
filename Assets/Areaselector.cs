using UnityEngine;
using System.Collections;

public class Areaselector : MonoBehaviour {

	private string textAreaString = "";
	
	
	
	
	void OnGUI () {
		textAreaString = GUI.TextArea (new Rect (Screen.width/2, Screen.height/2, 100, 30), textAreaString);
		if (!textAreaString.Equals ("")) { 
			GUI.Label (new Rect (Screen.width/2, Screen.height/2 + 50 , 100, 30), "Press Space");
		}
		if (textAreaString.Equals ("Forbidden"))
		{
			GUI.Label (new Rect (0, 0, 100, 100), "Hello");
		}
		
		
	}
	

}
