using UnityEngine;
using System.Collections;

public class viseur : MonoBehaviour {

    Vector2 pos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}
