using UnityEngine;
using System.Collections;

public class NewLevel : MonoBehaviour {
	
	private GameObject m_player = GameObject.FindWithTag("Player");
	void Start ()
	{
	
	 var inventory = m_player.GetComponent<RespawnPointInventory>();
     if (inventory != null){
     	inventory.Current = GameObject.Find ("/" + PlayerPrefs.GetString ("levelChoice") + "/Entrance/Respawn Point");
		
		}
	}
}
