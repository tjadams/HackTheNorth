using UnityEngine;
using System.Collections;

public class CharacterProperties : MonoBehaviour {

	//initial health
	public int health;
	bool dead = false;
	int deathframes = 90;
	// Use this for initialization
	void Start () {
		if (health == 0)
			health = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (dead && deathframes > 0) {
			GetComponent<Transform>().Rotate(1, 0, 0);
			deathframes--;
		}
	}

	//call to take away hp of the character
	public void HpLoss(int hpLoss, float orientation){
		if (health > 0) {
			Debug.Log ("this character has lost " + hpLoss + " health");
			health -= hpLoss;
			gameObject.GetComponentInChildren<DamageTextController>().DisplayDmg(hpLoss, orientation);
			CheckHp ();
		}
	}

	private void CheckHp(){
		if (health < 0) {
			Debug.Log ("This character has now died");
			dead = true;
		}
		//other things to do when this thing has died goes here
	}
}
