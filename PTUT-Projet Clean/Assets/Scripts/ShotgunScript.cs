using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ShotgunScript : NetworkBehaviour {

 public Transform shotPrefab;

    /// <summary>
    /// Temps de rechargement entre deux tirs
    /// </summary>
    public float shootingRate = 0.25f;

   
    /// </summary>
    //--------------------------------
    // 2 - Rechargement
    //--------------------------------

    private float shootCooldown;

    void Start()
    {
        shootCooldown = 0f;
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    //--------------------------------
    // 3 - Tirer depuis un autre script
    //--------------------------------

    /// <summary>
    /// Création d'un projectile si possible
    /// </summary>
	[Command]
    public void CmdAttack(bool isEnemy, Vector2 curseur)
    {
        if (CanAttack)
        {

            shootCooldown = shootingRate;

            //Creation de 6 objets
            for (int i = 0; i < 6; i++)
            {
                var shotTransform = Instantiate(shotPrefab) as Transform;
                float x = Random.Range(-1.0f, 1.0f);
                float y = Random.Range(-1.0f, 1.0f);
                
                curseur = new Vector2(curseur.x + x, curseur.y + y);

                // Position d'apparition du tir 
                shotTransform.position = transform.position;

                // Propriétés du script
                ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
                if (shot != null)
                {
                    shot.isEnemyShot = isEnemy;
                }

                // On saisit la direction pour le mouvement
                MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
                if (move != null)
                {
                    //move.direction = curseur; //direction du tir = position de la souris
                }
            }
        }
    }

    /// <summary>
    /// L'arme est chargée ?
    /// </summary>
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}
