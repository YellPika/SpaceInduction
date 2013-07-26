using UnityEngine;
using System.Collections;

public class SafetyRangeDetection : MonoBehaviour {
	
	
	
	
	public float safe_x;
	public float safe_y;
	public float safe_z;
	public float safeCheck = 1;
	
	void Start (){
		safeCheck = 1;
		
	}
	
	public void OnTriggerEnter(Collider other){
		safeCheck = 1;
		Debug.Log ("You are safe");
	
	}
	public void OnTriggerExit (Collider other){
		safeCheck = 2;
		Debug.Log ("You are in danger");
	}
	
	
	
}
	// Update is called once per frame
	/*
	void Update () {
		var maxDistanceSquared = maxDistance * maxDistance;
var rayDirection : Vector3 = playerObject.transform.localPosition - transform.localPosition;
var enemyDirection : Vector3 = transform.TransformDirection(Vector3.forward);
var angleDot = Vector3.Dot(rayDirection, enemyDirection);
var playerInFrontOfEnemy = angleDot > 0.0;
var playerCloseToEnemy =  rayDirection.sqrMagnitude < maxDistanceSquared;
 
if ( playerInFrontOfEnemy && playerCloseToEnemy)
{ 
    //by using a Raycast you make sure an enemy does not see you 
    //if there is a bulduing separating you from his view, for example
    //the enemy only sees you if it has you in open view
    var hit : RaycastHit;
    if (Physics.Raycast (transform.position,rayDirection, hit, maxDistance) 
    		&& hit.collider.gameObject==playerObject) //player object here will be your Player GameObject
    {
    	//enemy sees you - perform some action
    } 
    else 
    {
    	//enemy doesn't see you
    }		
}
*/
	
	
	

