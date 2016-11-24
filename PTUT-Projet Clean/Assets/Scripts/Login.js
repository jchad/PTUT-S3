private var formNick = ""; //this is the field where the player will put the name to login
private var formPassword = ""; //this is his password
var formText = ""; //this field is where the messages sent by PHP script will be in

static var urlip   = "http://checkip.dyndns.org/";
static var pubIP = "";


var URL = "localhost/PTUT/login.php"; //change for your URL
var hash = "hashcode"; //change your secret code, and remember to change into the PHP file too

private var textrect = Rect (10, 90, 200, 30); //just make a GUI object rectangle

function OnGUI() {
    GUI.Label( Rect (10, 10, 80, 20), "Your nick:" ); //text with your nick
    GUI.Label( Rect (10, 30, 80, 20), "Your pass:" );

    formNick = GUI.TextField ( Rect (90, 10, 100, 20), formNick ); //here you will insert the new value to variable formNick
    formPassword = GUI.TextField ( Rect (90, 30, 100, 20), formPassword ); //same as above, but for password

    if ( GUI.Button ( Rect (10, 60, 100, 20) , "Connexion" ) ){ //just a button
        Login();
    }
    GUI.TextArea( textrect, formText );

    if(GUI.Button(Rect(115, 60, 100, 20), "Quitter")){ 
		Application.LoadLevel("Accueil");
    }
}

function Login() {
    var form = new WWWForm(); //here you create a new form connection
    form.AddField( "myform_hash", hash ); //add your hash code to the field myform_hash, check that this variable name is the same as in PHP file
    form.AddField( "myform_nick", formNick );
    form.AddField( "myform_pass", formPassword );
    var w = WWW(URL, form); //here we create a var called 'w' and we sync with our URL and the form
    yield w; //we wait for the form to check the PHP file, so our game dont just hang
    if (w.error != null) {
        print(w.error); //if there is an error, tell us
    } else {
    	if(w.text=="Connexion réussie"){
    		pubIP=CheckIP();
    		Application.LoadLevel("PremiereScene");
    	}
        formText = w.text; //here we return the data our PHP told us
        print("Test ok");
        w.Dispose(); //clear our form in game
    }

    formNick = ""; //just clean our variables
    formPassword = "";
}

static function CheckIP ()
{
    var www : WWW = new WWW (urlip);
    yield www;
   pubIP = www.tex;
   //pubIP = pubIP.Substring(pubIP.IndexOf(“:”)+1);
   //pubIP = pubIP.Substring(0,pubIP.IndexOf(“<"));
    Debug.Log(pubIP);
    return pubIP;
}