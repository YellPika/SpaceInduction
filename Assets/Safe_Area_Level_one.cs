using UnityEngine;
using System.Collections;

public class Safe_Area_Level_one : MonoBehaviour {
	
	public Transform m_player;
	public bool Safety = true;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
			
	}
			
	void OnGUI ()
	{
		GUI.Label (new Rect (0, 0, 100, 100), "" );
		
	}
}
