using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Platform : NetworkBehaviour {
	public int maxlife=3;
	private int life;
	void Start(){
		life = maxlife;
	}
	// Use this for initialization
	public void gethit(int d){
		life = life - 1;
		if (life <= 0) {
			Destroy (this.gameObject);
		}
		}
	}