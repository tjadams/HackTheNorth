using UnityEngine;
using System.Collections;

public class DamageTextController : MonoBehaviour {

	int ticksToGone = 0;
	// Use this for initialization
	void Start () {
		renderer.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (ticksToGone != 0)
						ticksToGone--;
		if (ticksToGone <= 0)
						renderer.enabled = false;
	}

	public void DisplayDmg(int dmg, float orientation){
		Debug.Log ("displaying damage of " + dmg);
		TextMesh dmgDisp = gameObject.GetComponent<TextMesh>();
		dmgDisp.text = "" + dmg;
		Transform dispRotate = gameObject.GetComponent<Transform> ();
		dispRotate.rotation = Quaternion.Euler(0, orientation, 0);
		renderer.enabled = true;
		ticksToGone = 60;
	}
}