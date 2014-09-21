using UnityEngine;
using System.Collections;
using Pose = Thalmic.Myo.Pose;

public class BowController : MonoBehaviour {

	// Use this for initialization
	bool pulling = false;
	float pullStrength = 0;
	float stringLengthInit = 0;
	float stringLength = 0;
	bool resetNeeded = false;
	public GameObject myo = null;
	void Start () {
		Transform bowString = GameObject.Find ("Bow Top String").GetComponent<Transform>();
		stringLengthInit = bowString.localScale.y;
		stringLength = stringLengthInit;
	}

	bool myoUnlock = false;
	int lockSwitchCD = 200;
	bool switchCD = false;

	// Update is called once per frame
	void FixedUpdate () {
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

		if (switchCD) {
			lockSwitchCD--;
		}
		if (lockSwitchCD < 0) {
			switchCD = false;
			lockSwitchCD = 200;
		}
		if (thalmicMyo.pose == Pose.FingersSpread && !myoUnlock && !switchCD) {
			myoUnlock = true;
			switchCD = true;
		}
		if (thalmicMyo.pose == Pose.FingersSpread && myoUnlock && !switchCD) {
			myoUnlock = false;
			switchCD = true;
		}

		
		if (resetNeeded)
			reset ();
		//this method will receive new information from the myo to see how far are we going to pull the bow
		//code for update here
		if (myoUnlock && thalmicMyo.pose == Pose.Fist) {//some information from the myo that a fist position is there)
			pulling = true;
			Debug.Log("The bow is pulling");
		}
		if (myoUnlock && thalmicMyo.pose != Pose.Fist) {
				pulling = false;
				resetNeeded = true;
		}
		//this implies that we are now drawing the bow
		if (pulling && (pullStrength < 2*stringLengthInit)) {
			pullStrength += 0.2f;
			Debug.Log("pull at " + pullStrength + " with a string length of " + stringLength);
			//"extending" the bowstring
			stringLength = Mathf.Sqrt(((stringLengthInit*stringLengthInit)+(pullStrength*pullStrength)));
			Transform bowString = GameObject.Find ("Bow Top String").GetComponent<Transform>();
			Vector3 size = new Vector3(0.5f, stringLength, 0.5f);
			bowString.localScale = size;
			bowString = GameObject.Find ("Bow Bottom String").GetComponent<Transform>();
			size = new Vector3(0.5f, stringLength, 0.5f);
			bowString.localScale = size;
			Transform bowBead = GameObject.Find ("Bow Bead").GetComponent<Transform>();
			Vector3 pos = bowBead.localPosition;
			pos.z -= (0.2f/30);
			bowBead.localPosition = pos;
			//rotate the bowstrings
			float stringAngle = Mathf.Atan((pullStrength/stringLengthInit));
			stringAngle = stringAngle*(180/Mathf.PI);
			Transform bowStringBead = GameObject.Find("Bow Top Bead").GetComponent<Transform>();
			Vector3 rotate = new Vector3(0, 0, -1*stringAngle);
			bowStringBead.localEulerAngles = rotate;
			bowStringBead = GameObject.Find("Bow Bottom Bead").GetComponent<Transform>();
			rotate = new Vector3(0, 0, stringAngle);
			bowStringBead.localEulerAngles = rotate;
		}
		if (!pulling && pullStrength > 0.5) {
			//here we actually release the bow
			Debug.Log ("Releasing the arrow");
			ArrowController arrowObject = GameObject.Find("Arrow Tail").GetComponent<ArrowController>();
			arrowObject.setProjectile(pullStrength*10);
			resetNeeded = true;
		}
	}

	/*void arrowToBead(){
		ArrowController arrowController = GameObject.Find ("Arrow Tail").GetComponent<ArrowController> ();
		Transform arrowTail = GameObject.Find ("Arrow Tail").GetComponent<Transform>();
		if (arrowController.attached){
			Vector3 beadOri = GameObject.Find ("Bow Bead").GetComponent<Transform> ().eulerAngles;
			beadOri.z = 90;
			arrowTail.eulerAngles = beadOri;
			Vector3 beadPos = GameObject.Find ("Bow Bead").GetComponent<Transform> ().localPosition;
			arrowTail.localPosition = beadPos;
		}
	}*/

	void reset(){
		Transform bowBead = GameObject.Find ("Bow Bead").GetComponent<Transform>();
		Vector3 pos = bowBead.localPosition;
		pos.z += pullStrength/30;
		bowBead.localPosition = pos;
		stringLength = stringLengthInit;
		pullStrength = 0;
		resetNeeded = false;
	}
}
