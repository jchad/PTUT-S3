#pragma strict

function OnGUI() {
	GUI.Label( Rect (350, 50, 80, 20), "Numéro de joueur:" ); //text with your nick
    GUI.Label( Rect (350, 70, 80, 20), "Pseudo:" ); //text with your nick
    GUI.Label( Rect (350, 90, 80, 20), "Date d'inscription:" );
    GUI.Label( Rect (350, 110, 80, 20), "Temps de Jeu:" ); //text with your nick
    GUI.Label( Rect (350, 130, 80, 20), "Niveau:");

    GUI.Label ( Rect (500, 50, 100, 20), Login.infouser["noJoueur"].Value ); 
   	GUI.Label ( Rect (500, 70, 100, 20), Login.infouser["pseudo"].Value ); 
    GUI.Label ( Rect (500, 90, 100, 20), Login.infouser["dateInscription"].Value);
    GUI.Label ( Rect (500, 110, 100, 20), Login.infouser["tempsdeJeu"].Value ); 
    GUI.Label ( Rect (500, 130, 100, 20), Login.infouser["niveau"].Value); 

}