using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ShotScript : NetworkBehaviour {

	public int damage = 1;
	public bool isEnemyShot = false;
    public NetworkHash128 joueur;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 1);
	}

	void OnCollisionEnter2D(Collision2D collision){
		var hit = collision.gameObject;
        if (!NetworkHash128.Equals(joueur, collision.gameObject.GetComponent < NetworkIdentity>().assetId)) {
            
			var health = hit.GetComponent<PlayerV2Script> ();
            if (health != null) {
                Debug.Log("wtf");
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
        Network.Destroy(gameObject);

		}
}
