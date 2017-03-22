using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking; 

public class ShotScript : NetworkBehaviour {
	public int damage = 1;
	public bool isEnemyShot = false;
    public NetworkHash128 joueur;
	public GameObject blastPrefab;
	// Use this for initialization
	void Start () {
		
		Destroy (gameObject, 1);

	}


	void OnCollisionEnter2D(Collision2D collision){
		GameObject hit = collision.gameObject;
		Debug.Log (collision.gameObject.name);
		if (string.Compare (hit.name, "platformPrefab 1(Clone)")==0) {
			GameObject boom = Instantiate (blastPrefab) as GameObject;
			boom.transform.localPosition = transform.localPosition;
			boom.GetComponent<blast> ().radius = 0.3f;
            boom.GetComponent<blast>().parent = "tir";
            Debug.Log ("Boom ?");
			NetworkServer.Spawn (boom);
		}
            if (((string.Equals(collision.gameObject.name, "Player")) || string.Equals(collision.gameObject.name, "ennemy")))
            {
                if (!hit.GetComponent<PlayerV2Script>().isShield())
                {
                    hit.GetComponent<PlayerV2Script>().RpcTakedommage(1);
                }
            }
        if (!string.Equals(collision.gameObject.name, "tirN"))
        {
           
            Network.Destroy(gameObject);
        }

		}

    [Command]
    void CmdBlast()
    {
        
    }
		
}
