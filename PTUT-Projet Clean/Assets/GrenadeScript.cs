using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour {

    public GameObject blastPrefab;
    // Use this for initialization
    void Start () {
        Destroy(gameObject, 2);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDestroy()
    {
        GameObject boom = Instantiate(blastPrefab) as GameObject;
        boom.GetComponent<blast>().parent = "grenade";
        boom.transform.localPosition = transform.localPosition;
        boom.GetComponent<blast>().radius = 3.0f;
    }
}
