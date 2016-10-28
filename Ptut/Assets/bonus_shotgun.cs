using UnityEngine;
using System.Collections;

public class bonus_shotgun : MonoBehaviour {
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("coucou");
        WeaponScript weapon = collider.gameObject.GetComponent<WeaponScript>();
        ShotgunScript shotgun = collider.gameObject.GetComponent<ShotgunScript>();
        if (weapon != null && shotgun != null)
        {
            weapon.enabled = false;
            shotgun.enabled = true;
            PlayerScript player = collider.gameObject.GetComponent<PlayerScript>();
            player.nbTirs = 5;
        }
    }
}
