using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerV2Script : NetworkBehaviour
{
	private Vector3 startPos;

	private float speedHor;

	private bool shoot;

	private Rigidbody2D body;

	private Vector3 directionSouris;

	private Vector2 directionSouris2d;

	private bool isRightOriented;

	private List<GameObject> listeTir;

	public int healthMax = 4;

	[SyncVar]
	private int currentHealth;

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
		if (!isLocalPlayer) {
			this.gameObject.name = "ennemy";
		} else {
			this.gameObject.name = "player";
		}
		body = GetComponent<Rigidbody2D> ();
		arme = GetComponent<WeaponV2Script>();
		currentHealth = healthMax;
		startPos = this.transform.position;
		isRightOriented = true;
		Debug.Log (Network.player.ipAddress);
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

		orientation ();

		if (shoot && arme.isCooldownCooled ()) {
			
			InputTir (directionSouris2d);
		}

		mouvements ();
    }

	private void getInput() {
		shoot = Input.GetButton("Fire1") | Input.GetButtonDown("Fire2");
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
		
	public void InputTir(Vector2 v) {
		directionSouris =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
		listeTir = arme.attaque (v);
		Cmdspawn (listeTir);
	}

	[Command]
	public void Cmdspawn(List<GameObject> tir){
		foreach (GameObject elemTir in listeTir) {
			NetworkServer.Spawn (elemTir);
		}
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.red;
	}
		
	public void takeDommage(int dommage){

		if (!isServer) {
			return;
		}
		currentHealth -= dommage;
		if (currentHealth <= 0) {
			RpcRespawn ();
		}
	}

	private void orientation() {
		directionSouris =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
		directionSouris2d = new Vector2 (directionSouris.x - transform.position.x, directionSouris.y - transform.position.y);
		if ((Mathf.Abs (directionSouris2d.x) == directionSouris2d.x) != isRightOriented) {
			transform.Rotate (0, 180, 0);
			isRightOriented = !isRightOriented;
		}
	}

	[ClientRpc]
	public void RpcRespawn(){
		transform.position = startPos;
		currentHealth = healthMax;
	} 
}
