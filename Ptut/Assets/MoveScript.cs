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
    public Vector2 speed = new Vector2(1,1);

    /// <summary>
    /// Direction
    /// </summary>
    public Vector2 direction;

    private Vector2 movement;

    void Update()
    {
        // 2 - Calcul du mouvement
        movement = new Vector2(
          direction.x * speed.x,
          direction.y * speed.y);
    }

    void FixedUpdate()
    {
        // Application du mouvement
        GetComponent<Rigidbody2D>().velocity = movement;
       // Debug.Log(transform.position);
    }
}
