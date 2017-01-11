using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HP : MonoBehaviour {
	public Texture2D HPText;
	private float HPLength=100;
	public int ratio = 60;
	// Update is called once per frame
	private void OnGUI () 
	{
		int j;
		Resolution r = Screen.currentResolution;
		GameObject[] o = GameObject.FindGameObjectsWithTag ("Player");
		Debug.Log (o.Length);
		for (int i = 0; i < o.Length; i++) {
			for(j=0;j<o[i].GetComponent<PlayerV2Script>().currentHealth;j++){
				Debug.Log ("Le Japon, quel beau pays");
				GUI.DrawTexture(new Rect((r.width/ratio)*(j+1),(r.height/ratio)*(i+1), (r.width/ratio), (r.height/ratio)), HPText);
			}
		

		}
	}
}
