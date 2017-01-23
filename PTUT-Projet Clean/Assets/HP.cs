using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HP : MonoBehaviour
{
    public Texture2D HPText;
    private float HPLength = 100;
    public int ratio = 60;
    GameObject[] o;
    Resolution r;
    int i;
    int j;

    // Update is called once per frame

    void Start()
    {
        r = Screen.currentResolution;
        o = GameObject.FindGameObjectsWithTag("Player");
        
    }
	void Update(){
		o = GameObject.FindGameObjectsWithTag ("Player");
	}
		
    private void OnGUI()
    {
      
        for (i = 0; i < o.Length; i++)
        {
            for (j = 0; j < o[i].GetComponent<PlayerV2Script>().currentHealth; j++)
            {
                GUI.DrawTexture(new Rect((r.width / ratio) * (j + 1), (r.height / ratio) * (i + 1), (r.width / ratio), (r.height / ratio)), HPText);
            }


        }
    }


    void OnPlayerConnected(NetworkPlayer player)
    {
        o = GameObject.FindGameObjectsWithTag("Player");

    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        o = GameObject.FindGameObjectsWithTag("Player");
    }
}
   
