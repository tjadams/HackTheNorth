using UnityEngine;
using System.Collections;

public class SwordAttackScript : MonoBehaviour {

	private bool inCollision = false;
	public bool friendlyFire = false;

	// Use this for initialization
	void Start () {
		renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log ("It hit something!");
		string hitted = collision.collider.name;
		//only detects if the collider is a character, else do nothing
		bool isParent = gameObject.GetComponent<Transform> ().parent.name.Equals (hitted);
		if (hitted.Contains("CharBody") && !inCollision && (!isParent || friendlyFire)) {
			//we can integrate something that can calculate the damage from how hard the swing is here
			CharacterProperties hitChar = GameObject.Find(hitted).GetComponent<CharacterProperties>();
			float yAngle = gameObject.GetComponentInParent<Transform>().GetComponentInParent<Transform>().eulerAngles.y;
			hitChar.HpLoss(20, yAngle);
			inCollision = true;
		}
	}

	void OnCollisionExit(Collision collision){
		Debug.Log ("The sword left the body");
		string hitted = collision.collider.name;
		//only detects if the collider is a character, else do nothing
		if (inCollision) {
			inCollision = false;
		}
	}
}
