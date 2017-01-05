using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ShotScript : NetworkBehaviour {

	public int damage = 1;
	public bool isEnemyShot = false;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 1);
	}

	void OnCollisionEnter2D(Collision2D collision){
		var hit = collision.gameObject;
		if (string.Compare (hit.name, "ennemy") == 0) {
			var health = hit.GetComponent<PlayerV2Script> ();
			if (health != null) {
				health.takeDommage (damage);
			}
		}
		if (string.Compare (hit.name, "platformPrefab 1(Clone)")==0) {
			var scr = hit.GetComponent<Platform> ();
			Debug.Log ("Mur");
			if (scr != null) {
				Debug.Log ("Mur");
				scr.gethit (damage);
			
			}
		}
		if (string.Compare (hit.name, "player") != 0) {
			Destroy (gameObject);
		}
		}
}
