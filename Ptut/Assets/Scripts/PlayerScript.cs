using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public int nbTirs = 0;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        bool shoot = Input.GetButtonDown("Fire1");
        shoot |= Input.GetButtonDown("Fire2");
        // Astuce pour ceux sous Mac car Ctrl + flèches est utilisé par le système
        
        if (shoot)
        {
           
            Vector3 shootDirection;
            shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection - transform.position;
            shootDirection = CalculDirection(shootDirection);
            if (GetComponent<WeaponScript>().enabled)
            {
                WeaponScript weapon = GetComponent<WeaponScript>();
                weapon.Attack(false, shootDirection);
            }
            else if (GetComponent<ShotgunScript>().enabled)
            {
                if (nbTirs != 0)
                {
                    ShotgunScript weapon = GetComponent<ShotgunScript>();
                    weapon.Attack(false, shootDirection);
                    nbTirs -= 1;
                }
                else
                {
                    ShotgunScript shot = GetComponent<ShotgunScript>();
                    shot.enabled = false;
                    WeaponScript weapon = GetComponent<WeaponScript>();
                    weapon.enabled = true;
                    weapon.Attack(false, shootDirection);
                }
            }
        
                // false : le joueur n'est pas un ennemi
                
            
        }
    }

    Vector3 CalculDirection (Vector3 direction)
    {
        float x = direction.x;
        float y = direction.y;
        while ((x > 1 || x < -1) || (y > 1 || y < -1))
        {
            x = x / 10;
            y = y / 10;
            Debug.Log(x);
            Debug.Log(y);  
        }
              
        return new Vector3(x,y,0);
    }
}
