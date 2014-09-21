using UnityEngine;
using System.Collections;

public class CubeScriptTest : MonoBehaviour {

	public int health;
	// Use this for initialization
	void Start () {
		if (health == 0) {
			health = 100;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log ("It hit something!");
		health--;
		checkHealth ();
	}
	void checkHealth(){
		if (health == 0)
						Debug.Log ("the object has died"); 
		else
			Debug.Log("the object has " + health + " health left");
	}
}
