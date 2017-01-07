using UnityEngine;

public class ApplicationEnd : MonoBehaviour
{
	public string logoutURL="localhost/ptut/logout.php?";
	public string hash="hashcode";

	void Awake()
	{
		//This object will not be destroyed between scenes,
		//It will only be destroyed when the application quits
		DontDestroyOnLoad(this);
	}

	//This is still called in the editor
	void OnDestroy()
	{
		#if UNITY_EDITOR
		Debug.Log("On Destroy called");
		OnApplicationQuit();
		#endif
	}

	void OnApplicationQuit()
	{ 
		/*string url = logoutURL + "nick=" WWW.EscapeURL(nick) + "&hash=" + hash; nick a remplacer par le bon maillon de la liste
		WWW hs_post = new WWW(url);
		yield return hs_post;
		if(hs_post.error!=null){
			print("Il a a eu une erreur pendant la deconnexion : " + hs_post.error);
		}
	*/}
}
