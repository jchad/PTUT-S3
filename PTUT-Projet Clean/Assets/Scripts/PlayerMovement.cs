using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
	private float speedx;
	private float speedy;

	private float jumpSpeed = 5.0f;
	private float veloY;
	public float gravity = 20.0f;
	public bool isGrounded = true;

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


		if (Input.GetButton ("Jump")) {
			veloY = jumpSpeed;
		} else {
			veloY -= gravity * Time.deltaTime;
		}

		body.velocity = new Vector2(speedx * maxSpeed,  veloY);
	}


}