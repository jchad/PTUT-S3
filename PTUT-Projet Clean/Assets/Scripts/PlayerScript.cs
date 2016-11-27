using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public int nbTirs = 0;
    public float speed = 5.0f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        bool shoot = Input.GetButtonDown("Fire1");
        shoot |= Input.GetButtonDown("Fire2");
        float horizon = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
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
                weapon.Attack(false, shootDirection);
            }
            else if (GetComponent<ShotgunScript>().enabled)
            {
                if (nbTirs != 0)
                {
                    shot.Attack(false, shootDirection);
                    nbTirs -= 1;
                }
                else
                {
                    shot.enabled = false;
                    weapon.enabled = true;
                    weapon.Attack(false, shootDirection);
                }
            }
        
                // false : le joueur n'est pas un ennemi
                
            
        }

        if(horizon != 0 || vert != 0)
        {
            float x = horizon * speed * Time.deltaTime;
            float y = vert * speed * Time.deltaTime;
            transform.Translate(x, y, 0);
        }
    }

    Vector3 CalculDirection (Vector3 direction)
    {
        float longueur = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
        float x = direction.x / longueur;
        float y = direction.y / longueur;
        return new Vector3(x,y,0);
    }
}
