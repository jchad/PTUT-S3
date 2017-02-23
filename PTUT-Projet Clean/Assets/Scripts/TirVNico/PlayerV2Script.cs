using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking; 

public class PlayerV2Script : NetworkBehaviour
{ 
	private NetworkStartPosition[] spawnPoints;
    [SyncVar]
	private Vector3 startPos;

	private float speedHor;

	private bool shoot;

	private Rigidbody2D body;

	private Vector3 directionSouris;

	private Vector2 directionSouris2d;

	public bool isRightOriented;

	private List<GameObject> listeTir;

	public int healthMax = 4;

	[SyncVar]
	public int currentHealth;

	[SerializeField]
    private float jumpForce = 5500.0f;

	[SerializeField]
	private float maxiSpeed = 5.0f;

	[SerializeField]
	private Transform groundPoint;
	[SerializeField]
	private Transform leftPoint;
	[SerializeField]
	private Transform rightPoint;

	[SerializeField]
	private float groundRadius;

	[SerializeField]
	private LayerMask sol;

	private bool isGrounded;

	private bool jump;

	private WeaponV2Script arme;

	private GameObject cam = null;

	private bool bonusDoubleJump = true;
	private bool isDoubleJump = false;

    private bool bonusJetPack = true;
    [SyncVar]
    private bool jetPack = false;

    private bool bonusShield = true;
    [SyncVar]
    private bool shield = false;
    // Le début des méthodes
    void Start()
	{
		
		spawnPoints = NetworkManager.FindObjectsOfType<NetworkStartPosition>();
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
		Network.DestroyPlayerObjects(player);
        Network.RemoveRPCs(player);    
    }

    void FixedUpdate()
	{
		if (!isLocalPlayer) {
			return;
		}
		if (currentHealth <= 0) {
			currentHealth = healthMax;
			RpcRespawn ();
		}
        jetPack = false;
        if (shield)
        {
            shield = false;
            if (isLocalPlayer)
            {
                GetComponent<SpriteRenderer>().material.color = Color.red; ;
            }
            else
            {
                GetComponent<SpriteRenderer>().material.color = Color.yellow;
            }
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
		shoot = Input.GetButton("Fire1");
		speedHor = Input.GetAxis("Horizontal");
		if (Input.GetButtonDown ("Jump")) {
			jump = true;
		}
        if (bonusJetPack) {
            if (Input.GetButton("Fire2"))
            {
                jetPack = true;
            }
        }
        if (bonusShield) {
            if (Input.GetButton("Fire3"))
            {
                shield = true;
                GetComponent<SpriteRenderer>().material.color = Color.blue;
            
            }
        }
        
	}

	private bool IsGrounded() {
		if (body.velocity.y <= 0) {
			List<Collider2D> colliders = new List<Collider2D>(Physics2D.OverlapCircleAll (groundPoint.position, groundRadius, sol));
			colliders.AddRange(Physics2D.OverlapCircleAll (leftPoint.position, groundRadius, sol));
			colliders.AddRange(Physics2D.OverlapCircleAll (rightPoint.position, groundRadius, sol));
			for (int i = 0; i < colliders.Count; i++) {
				if (colliders [i].gameObject != gameObject) {
					jump = false;
                    isDoubleJump = false;
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
            jump = false;
		}

        else if (!isGrounded && bonusDoubleJump && !isDoubleJump && jump)
        {
            isDoubleJump = true;
			body.velocity = new Vector3 (0, 0, 0);
            body.AddForce(new Vector2(0, jumpForce));
        }

        if (jetPack)
        {
            if (body.velocity.y < 20) {
                body.AddForce(new Vector2(0, 600));
            }
        
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
		GetComponent<SpriteRenderer>().material.color = Color.red;
	}
    public void RpcTakedommage(int dommage){
		if (!isServer) {
			return;
		}
		currentHealth -= dommage;
        //Debug.Log(currentHealth);
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
			CmdTourne(this.gameObject);
			isRightOriented = !isRightOriented;
		}
	}
	[Command] private void CmdTourne (GameObject o){
		o.transform.Rotate (0, 180, 0);
		RpcTourne (o);
	}
	[ClientRpc] private void RpcTourne(GameObject o){
		if (!(isServer)){
			o.transform.Rotate (0, 180, 0);
		}
	}
	[ClientRpc]
    public void RpcRespawn()
    {
		spawnPoints = NetworkManager.FindObjectsOfType<NetworkStartPosition>();
		// Set the spawn point to origin as a default value
		Vector3 spawnPoint = Vector3.zero;
	
		// If there is a spawn point array and the array is not empty, pick one at random
		if (spawnPoints != null && spawnPoints.Length > 0)
		{
			spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
		}
		// Set the player’s position to the chosen spawn point
		transform.position = spawnPoint;
        //doubleJump = false;

    }

    public bool isShield()
    {
        return shield;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (string.Equals(collision.gameObject.name, "DeathZone")){
			RpcTakedommage (500);
        }
        
        else if (string.Equals(collision.gameObject.name, "Shotgun"))
        {
			
			arme.CmdSetArme ("shotgun");
			arme.CmdSetNbTir (5);
            Network.Destroy(collision.gameObject);
        }
       

    }
}
