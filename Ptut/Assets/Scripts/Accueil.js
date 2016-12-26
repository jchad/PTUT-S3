function OnGUI (){

    if(GUI.Button(Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 200, 50), "Se connecter")){ 
		Application.LoadLevel("Login");
    }

    if(GUI.Button(Rect(Screen.width / 2 - 100, Screen.height / 2 - 75, 200, 50), "S'inscrire")){
    	Application.LoadLevel("Register");

    }

    if(GUI.Button(Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 50), "Quitter")){

      Application.Quit();

    }

}
