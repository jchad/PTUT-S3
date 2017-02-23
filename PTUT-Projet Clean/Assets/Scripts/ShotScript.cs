using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
		Debug.Log (collision.gameObject.name);
		if (string.Compare (hit.name, "platformPrefab 1(Clone)")==0) {
			var scr = hit.GetComponent<Platform> ();
			if (scr != null) {
				scr.gethit (damage);
			}
		}
            if (((string.Equals(collision.gameObject.name, "Player")) || string.Equals(collision.gameObject.name, "ennemy")))
            {
                if (hit.GetComponent<PlayerV2Script>().isShield())
                {
                    hit.GetComponent<PlayerV2Script>().RpcTakedommage(1);
                }
            }
        if (!string.Equals(collision.gameObject.name, "tirN"))
        {
            Network.Destroy(gameObject);
        }

		}
		
}
