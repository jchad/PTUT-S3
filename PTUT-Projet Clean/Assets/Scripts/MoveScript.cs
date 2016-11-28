using UnityEngine;

/// <summary>
/// Déplace l'objet
/// </summary>
public class MoveScript : MonoBehaviour
{
    // 1 - Designer variables

    /// <summary>
    /// Vitesse de déplacement
    /// </summary>
    public int speedx = 30;
    public int speedy = 30;

    /// <summary>
    /// Direction
    /// </summary>
    public Vector3 direction;

    private Vector2 movement;

    void Start()
    {
        // 2 - Calcul du mouvement
        movement = new Vector2(
          direction.x * speedx,
          direction.y * speedy);
    }
        void FixedUpdate()
    {
            // Application du mouvement
            GetComponent<Rigidbody>().velocity = movement;
            // Debug.Log(transform.position);
        }
    }