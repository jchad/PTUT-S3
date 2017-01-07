﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerV2Script : NetworkBehaviour
{ 
    [SyncVar]
	private Vector3 startPos;

	private float speedHor;

	private bool shoot;

	private Rigidbody2D body;

	private Vector3 directionSouris;

	private Vector2 directionSouris2d;

    [SyncVar]
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

    private GameObject cam = null;
    // Le début des méthodes
    void Start()
	{
		if (!isLocalPlayer) {
			this.gameObject.name = "ennemy";
		} else {
			this.gameObject.name = "Player";
            cam = GameObject.FindGameObjectWithTag("MainCamera");
		}
		body = GetComponent<Rigidbody2D> ();
		arme = GetComponent<WeaponV2Script>();
		currentHealth = healthMax;
		startPos = transform.position;

        directionSouris = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        directionSouris2d = new Vector2(directionSouris.x - transform.position.x, directionSouris.y - transform.position.y);
		isRightOriented = (Mathf.Abs(directionSouris2d.x) == directionSouris2d.x);
		if (!isRightOriented) {
			transform.Rotate (0, 180, 0);
		}

        Debug.Log (Network.player.ipAddress);
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }

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
			
			InputTir ();
		}

		mouvements ();
    }

	private void getInput() {
		shoot = Input.GetButtonDown("Fire2") | Input.GetButton("Fire1");
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
       cam.transform.position = transform.position + new Vector3(0, 0, -2);
          
    }
		
	public void InputTir() {
		directionSouris =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
		arme.attaque (directionSouris2d);
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.red;
	}

    public void takeDommage(int dommage){
		currentHealth -= dommage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0) {
            currentHealth = healthMax;
            RpcRespawn ();
		}
	}

	private void orientation() {
		isRightOriented = (Mathf.Abs(directionSouris2d.x) == directionSouris2d.x);
		directionSouris =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
		directionSouris2d = new Vector2 (directionSouris.x - transform.position.x, directionSouris.y - transform.position.y);
		if ((Mathf.Abs (directionSouris2d.x) == directionSouris2d.x) != isRightOriented) {
			transform.Rotate (0, 180, 0);
			isRightOriented = !isRightOriented;
		}
	}

    [ClientRpc]
    public void RpcRespawn()
    {
        if (isServer)
        {
            transform.position = startPos;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("test");
        if (string.Equals(collision.gameObject.name, "DeathZone")){
            transform.position = startPos;
        }
    }
    }