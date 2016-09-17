using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {
    public Vector2 pos = new Vector2(0, 0);
    public Vector2 speed = new Vector2(0.01f,0);
    public float posX = 0;
    public float posY = 0;
    // Use this for initialization
    void Start () {
        pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        posX = pos.x + speed.x;
        posY = pos.y + speed.y;
        pos = pos + new Vector2(posX, posY);
        transform.position = pos;
        Debug.Log(pos);
	}
}
