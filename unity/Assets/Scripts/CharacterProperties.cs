using UnityEngine;
using System.Collections;

public class CharacterProperties : MonoBehaviour {

	//initial health
	public int health;
	// Use this for initialization
	void Start () {
		if (health == 0)
			health = 100;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//call to take away hp of the character
	public void HpLoss(int hpLoss, float orientation){
		if (hpLoss > 0) {
			Debug.Log ("this character has lost " + hpLoss + " health");
			health -= hpLoss;
			gameObject.GetComponentInChildren<DamageTextController>().DisplayDmg(hpLoss, orientation);
			CheckHp ();
		}
	}

	private void CheckHp(){
		if (health == 0)
			Debug.Log ("This character has now died");
		//other things to do when this thing has died goes here
	}
}
