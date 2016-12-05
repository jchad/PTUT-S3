using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class PlayerScript : NetworkBehaviour
{
	private float speedHor;

	private bool shoot;

    private int nbTirs = 0;

	private Rigidbody2D body;

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

    // Le début des méthodes
    void Start()
	{
		body = GetComponent<Rigidbody2D> ();
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

        if (shoot)
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            ShotgunScript shot = GetComponent<ShotgunScript>();
            Vector3 shootDirection;

            shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection - transform.position;
			shootDirection = CalculDirection(shootDirection);

            if (weapon.enabled)
            {
                weapon.CmdAttack(false, shootDirection);
            }
            else if (GetComponent<ShotgunScript>().enabled)
            {
                if (nbTirs != 0)
                {
                    shot.CmdAttack(false, shootDirection);
                    nbTirs -= 1;
                }
                else
                {
                    shot.enabled = false;
                    weapon.enabled = true;
                    weapon.CmdAttack(false, shootDirection);
                }
            }
            // false : le joueur n'est pas un ennemi
        }

		mouvements ();
    }

    private Vector3 CalculDirection(Vector3 direction)
    {
        float longueur = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
        float x = direction.x / longueur;
        float y = direction.y / longueur;
        return new Vector3(x, y, 0);
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

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.red;
	}
}
