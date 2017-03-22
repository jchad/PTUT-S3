using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class blast : NetworkBehaviour {
	public float radius;
    public string parent;
	private GameObject hit;
	Collider2D[] collision;
	void Start()
    { 
		Destroy (gameObject, (float) 0.1);
		collision = Physics2D.OverlapCircleAll (
			new Vector2(transform.localPosition.x,transform.localPosition.y ),
			radius);

		for (int i = 0; i < collision.Length; i++) {
			hit = collision[i].gameObject;
		    if (string.Compare(hit.name, "platformPrefab 1(Clone)") == 0)
		    {
		        Platform scr = hit.GetComponent<Platform>();
		        if (scr != null)
		        {
		            scr.gethit(1);
		        }
		    }
		    else if (string.Compare(parent, "grenade") == 0)
		    {
		        Debug.Log("poueteuh");
		        if (string.Compare(hit.name, "Player") == 0 || string.Compare(hit.name, "ennemy") == 0)
		        {
		            hit.GetComponent<PlayerV2Script>().RpcTakedommage(2);
		        }
		    }


		}
	}

}
