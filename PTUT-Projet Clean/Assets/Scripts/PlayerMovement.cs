using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
	private float speedx;

	[SerializeField]
	private float jumpSpeed = 5.0f;

	private Rigidbody2D body;

	[SerializeField]
	private float maxSpeed = 5f;

	[SerializeField]
	private Transform groundPoint;

	[SerializeField]
	private float groundRadius;

	[SerializeField]
	private LayerMask sol;

	private bool isGrounded;

	private bool jump;

// fin des variables
	void Start()
	{
		body = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate()
	{
		// Application du mouvement
		speedx = Input.GetAxis("Horizontal");

		isGrounded = IsGrounded ();

		if (Input.GetButton ("Jump")) {
			jump = true;
		}

		movements ();
	}

	private void movements() {
		
		body.velocity = new Vector2(speedx * maxSpeed,  body.velocity.y);
		if (isGrounded && jump) {
			isGrounded = false;
			body.AddForce (new Vector2 (0, jumpSpeed));
		}
	}

	private bool IsGrounded() {
		if (body.velocity.y <= 0) {
			Collider2D[] colliders = Physics2D.OverlapCircleAll (groundPoint.position, groundRadius, sol);
			for (int i = 0; i < colliders.Length; i++) {
				if (colliders [i].gameObject != gameObject) {
					jump = false;
					return true;
				}
			}
		}
		return false;
	}
}