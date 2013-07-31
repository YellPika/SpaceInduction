using System;
using UnityEngine;

public sealed class RespawnPointInventory : MonoBehaviour
{
    [SerializeField]
    private RespawnPoint current;
	
	
	
	
	void Start (){
	
	RespawnPoint newCurrent = GameObject.Find ("/" + PlayerPrefs.GetString ("levelChoice") + "/Entrance/Respawn Point");

	}
    public RespawnPoint Current
    {
    	
		
		get { return current; }
        set { current = value; }
    
	}
	
		
	
	
	
	
}
