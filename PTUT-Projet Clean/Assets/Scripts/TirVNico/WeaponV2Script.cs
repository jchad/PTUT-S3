using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class WeaponV2Script : NetworkBehaviour {

	[SyncVar]
	private float cooldown;

	[SyncVar]
	private string equiped;

	[SyncVar]
	private int nbtir = 0;

	[SerializeField]
	private GameObject ballePrefab;

	[SerializeField]
	private Transform balleSpawn;

	[SerializeField]
	private Sprite shotgunSprite;

	[SerializeField]
	private Sprite akSprite;

	// Use this for initialization
	void Start () {
		cooldown = 0.0f;
		equiped = "handgun";
	}

	// Update is called once per frame
	void Update () {
		if (cooldown > 0)
			cooldown -= Time.deltaTime;
		else
			cooldown = 0.0f;
	}

	public void attaque (Vector2 aimTo) {
		switch (equiped) {
		case "handgun":
			cooldown = 0.3f;
			CmdSpawnTir (aimTo);
			break;
		case "shotgun":
			cooldown = 0.7f;
			CmdSpawnTir (aimTo);
			break;
		}
	}

	[Command]
	public void CmdSpawnTir(Vector2 aimTo){
		/*int orientation = -1;
		if (gameObject.GetComponent<PlayerV2Script>().isRightOriented){
			orientation = 1
				;}*/
		switch (equiped) {
		case "handgun":
			var tir = (GameObject)Instantiate (ballePrefab, balleSpawn.position, balleSpawn.rotation);
			Destroy (tir, 2.0f);
			tir.GetComponent<Rigidbody2D> ().velocity = CalculDirection (aimTo) * 20;
			tir.GetComponent<ShotScript> ().joueur = gameObject.GetComponent<NetworkIdentity> ().assetId;
			Physics2D.IgnoreCollision (tir.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
			NetworkServer.Spawn (tir);
			break;
		case "shotgun":
			//Creation de 6 objets
			GameObject[] list = new GameObject[6];
			for (int i = 0; i < 6; i++) {
				tir = (GameObject)Instantiate (ballePrefab, balleSpawn.position, balleSpawn.rotation);
				list [i] = tir;
				Destroy (tir, 2.0f);
				Vector2 dir = CalculDirection (aimTo) * 20;
				float x = Random.Range (-3.0f, 3.0f);
				float y = Random.Range (-3.0f, 3.0f);
				dir = new Vector2 (dir.x + x, dir.y + y);
				Debug.Log (i);
				for (int j = 0; j < i; j++) {
					Physics2D.IgnoreCollision (tir.GetComponent<Collider2D> (), list [j].GetComponent<Collider2D> ());
				}
				Physics2D.IgnoreCollision (tir.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
				tir.GetComponent<Rigidbody2D> ().velocity = dir;
				tir.GetComponent<ShotScript> ().joueur = gameObject.GetComponent<NetworkIdentity> ().assetId;
				NetworkServer.Spawn (tir);

			}
			nbtir--;
			if (nbtir == 0) {
				CmdSetArme("handgun");
			}
		
			break;
		}
	}	


	public bool isCooldownCooled() {
		return cooldown==0;
	}

	private Vector2 CalculDirection(Vector2 direction)
	{
		float longueur = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
		float x = direction.x / longueur;
		float y = direction.y / longueur;
		return new Vector2(x, y);
	}

	[Command]
	public void CmdSetArme(string arme){
		equiped = arme;
		Sprite sprite = akSprite;
		switch (arme) {
			case "handgun":
				sprite = akSprite;
				break;
			case "shotgun":
				sprite = shotgunSprite;
				break;
		}
		transform.FindChild ("WepSprite").GetComponent<SpriteRenderer> ().sprite = sprite;
	}

	[Command]
	public void CmdSetNbTir(int nb){
		nbtir = nb;
	}
}
