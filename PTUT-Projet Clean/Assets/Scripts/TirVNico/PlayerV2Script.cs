using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerV2Script : NetworkBehaviour
{
	private float speedHor;

	private bool shoot;

	private Rigidbody2D body;

	private Vector3 directionSouris;

	private List<GameObject> listeTir;

	[SerializeField]
    private float jumpForce = 5500.0f;

	[SerializeField]
	private float maxiSpeed = 5.0f;

	[SerializeField]
	private Transform groundPoint;

	[SerializeField]
	private float groundRadius;

	[SerializeField]
	private LayerMask sol;

	private bool isGrounded;

	private bool jump;

	private WeaponV2Script arme;

    // Le début des méthodes
    void Start()
	{
		body = GetComponent<Rigidbody2D> ();
		arme = GetComponent<WeaponV2Script>();
    }

    // Update is called once per frame
    void FixedUpdate()
	{
		if (!isLocalPlayer) {
			return;
		}
		isGrounded = IsGrounded ();
		getInput ();
        // Astuce pour ceux sous Mac car Ctrl + flèches est utilisé par le système

		if (shoot && arme.isCooldownCooled ()) {
			directionSouris =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
			CmdInputTir (new Vector2 (directionSouris.x - transform.position.x, directionSouris.y - transform.position.y));
		}

		mouvements ();
    }

	private void getInput() {
		shoot = Input.GetButtonDown("Fire1") | Input.GetButtonDown("Fire2");
		speedHor = Input.GetAxis("Horizontal");
		if (Input.GetButton ("Jump")) {
			jump = true;
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

	private void mouvements() {
		if (isGrounded && jump) {
			isGrounded = false;
			body.AddForce (new Vector2 (0, jumpForce));
		}
		body.velocity = new Vector2 (speedHor * maxiSpeed, body.velocity.y);
	}
		
	[Command]
	public void CmdInputTir(Vector2 v) {
		directionSouris =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
		listeTir = arme.attaque (v);
		foreach (GameObject elemTir in listeTir) {
			NetworkServer.Spawn (elemTir);
		}
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.red;
	}
}
