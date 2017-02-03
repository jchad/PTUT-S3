using UnityEngine;
using System.Collections;

public class viseur : MonoBehaviour {

	[SerializeField]
	private Texture2D curseur;

	[SerializeField]
	private CursorMode cMode = CursorMode.Auto;

	[SerializeField]
	private Vector2 hotSpot = Vector2.zero;

	void Start() {
		Cursor.SetCursor (curseur, hotSpot, cMode);
	}
}
