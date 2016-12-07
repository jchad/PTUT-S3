using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponV2Script : MonoBehaviour {

	private float cooldown;

	private string equiped;

	[SerializeField]
	private GameObject ballePrefab;

	[SerializeField]
	private Transform balleSpawn;

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

	public List<GameObject> attaque (Vector2 aimTo) {
		List<GameObject> lstTir = new List<GameObject>();
		switch (equiped) {
		case "handgun":
			cooldown = 0.5f;
			lstTir.Add(spawnTir (aimTo));
			break;
		}
		return lstTir;
	}

	GameObject spawnTir (Vector2 aimTo) {
		var tir = (GameObject)Instantiate (ballePrefab, balleSpawn.position, balleSpawn.rotation);
		Destroy (tir, 2.0f);
		tir.GetComponent<Rigidbody2D> ().velocity = CalculDirection(aimTo) * 35;
		return tir;
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
}
