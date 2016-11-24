using UnityEngine;

/// <summary>

public class MoveScript : MonoBehaviour
{
    private float speedx;
    private float speedy;
	private Vector2 direction;
	private Rigidbody2D body;
	public float maxSpeed = 5f;

    void Start()
    {
		body = GetComponent<Rigidbody2D> ();
    }

    void Update()
    {
        // Application du mouvement
		speedx = Input.GetAxis("Horizontal");

		body.velocity = new Vector2(speedx * maxSpeed, body.velocity.y);
    }
}
