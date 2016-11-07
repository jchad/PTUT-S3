
function OnGUI (){

	if(GUI.Button(Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 200, 50), "Jouer")){ 

	Application.LoadLevel("Level 1"); 

	}

	if(GUI.Button(Rect(Screen.width / 2 - 100, Screen.height / 2 - 75, 200, 50), "Options")){


	}

	if(GUI.Button(Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 50), "Quitter")){

	  Application.Quit();

	}

}