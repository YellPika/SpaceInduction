using UnityEngine;
using System.Collections;

public class GUItesting : MonoBehaviour {

	
	
	void OnGUI() {
		GUI.Label (new Rect (0, 0, 100, 100), "Hello");
	}
}
