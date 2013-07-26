using UnityEngine;
using System.Collections;

public class SafeAreaLevelOne : MonoBehaviour {
	
	public Transform m_player;
	public bool safety = true;
	
			
	void OnGUI ()
	{
		GUI.Label (new Rect (0, 0, 100, 100), "" );
	}
}
