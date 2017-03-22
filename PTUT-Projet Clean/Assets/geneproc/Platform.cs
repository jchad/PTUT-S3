using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
[NetworkSettings(channel=1,sendInterval=0.2f)]
public class Platform : NetworkBehaviour {
	public int maxlife=3;
	private Vector3 realScale;
	private int life;
	void Start(){
		
		life = maxlife;
		realScale = GameObject.FindGameObjectWithTag ("Start").GetComponent<VoxelGrid>().realScale;
		gameObject.transform.localScale = Vector3.Scale(gameObject.transform.localScale,realScale);
	}
	// Use this for initialization
	public void gethit(int d){
		life = life - 1;
		if (life <= 0) {
			
			Destroy (this.gameObject);
		}
		}
	}