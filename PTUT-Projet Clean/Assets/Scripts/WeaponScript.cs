using UnityEngine;
using UnityEngine.Networking;
/// <summary>
/// Crée des projectiles
/// </summary>
public class WeaponScript : NetworkBehaviour
{

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
	///

	[Command]
    public void CmdAttack(bool isEnemy, Vector2 curseur)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            // Création d'un objet copie du prefab
            var shotTransform = (GameObject)Instantiate(shotPrefab, transform.position, transform.rotation);


     //       shotTransform.position = transform.position;


           
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
                move.direction = curseur;
				NetworkServer.Spawn (shotTransform);
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
