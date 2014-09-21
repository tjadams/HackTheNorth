using UnityEngine;
using System.Collections;
using System;

public class ArrowController : MonoBehaviour {

	public bool attached = true;
	public bool friendlyFire = false;
	bool reload = false;
	int wait = 30;
	int watchdogReset = 1000;
	float vel = 0;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Rigidbody> ().useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (attached)
				arrowToBead ();
	}

	void FixedUpdate(){
		if (!attached)
			watchdogReset--;
		if (wait > 0 && reload)
			wait--;
		if (wait == 0 || watchdogReset == 0) {
			attached = true;
			reload = false;
			wait = 30;
			watchdogReset = 1000;
			gameObject.GetComponent<Rigidbody> ().useGravity = false;
			Rigidbody initialConditions = gameObject.GetComponent<Rigidbody>();
			initialConditions.velocity = Vector3.zero;
			initialConditions.angularVelocity = Vector3.zero;
		}
	}

	public bool Attached{
		get { return attached;}
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log ("The arrow hit something!");
		string hitted = collision.collider.name;
		//only detects if the collider is a character, else do nothing
		bool isParent;
		try{
			isParent = gameObject.GetComponent<Transform> ().parent.name.Equals (hitted);
		}
		catch(Exception e){
			isParent = false;
		}
		if (hitted.Contains("CharBody") && (!isParent || friendlyFire)) {
			//we can integrate something that can calculate the damage from how hard the swing is here
			CharacterProperties hitChar = GameObject.Find(hitted).GetComponent<CharacterProperties>();
			float yAngle = gameObject.GetComponent<Transform>().eulerAngles.y;
			int dmg = 10;
			dmg += (int)(vel/5);
			hitChar.HpLoss(dmg, yAngle);
		}
		reload = true;
	}

	void arrowToBead(){
		Transform arrowTail = gameObject.GetComponent<Transform>();
		if (attached){
			Vector3 beadOri = GameObject.Find ("Bow Bead").GetComponent<Transform> ().eulerAngles;
			beadOri.z = 90;
			arrowTail.eulerAngles = beadOri;
			Vector3 beadPos = GameObject.Find ("Bow Bead").GetComponent<Transform> ().position;
			arrowTail.position = beadPos;
		}
	}

	//the velocity of the bow is obtained from the bow which is controlled by the myo
	//When this is called, it means the arrow is officially released from the bow
	public void setProjectile(float velocity){
		attached = false;
		vel = velocity;
		//for this, we are using the Z angle with x and y both set to 0
		Rigidbody initialConditions = gameObject.GetComponent<Rigidbody>();
		initialConditions.useGravity = true;
		initialConditions.AddForce(gameObject.GetComponent<Transform>().up*(-1*velocity/2));
	}
}
