using UnityEngine;

/// <summary>
/// Gestion des points de vie et des dégâts
/// </summary>
public class HealthScript : MonoBehaviour
{
    /// <summary>
    /// Points de vies
    /// </summary>
    public int hp = 1;

    /// <summary>
    /// Ennemi ou joueur ?
    /// </summary>
    public bool isEnemy = true;

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Est-ce un tir ?
        ShotScript shot = collider.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            // Tir allié
            if (shot.isEnemyShot != isEnemy)
            {
                hp -= shot.damage;

                // Destruction du projectile
                // On détruit toujours le gameObject associé
                // sinon c'est le script qui serait détruit avec ""this""
                Destroy(shot.gameObject);

                if (hp <= 0)
                {
                    // Destruction !
                    Destroy(gameObject);
                }
            }
        }
    }
}