import SimpleJSON;
import JSONParse;
private var formNick = ""; //this is the field where the player will put the name to login
private var formPassword = ""; //this is his password
var infouser : User;
var formText = ""; //this field is where the messages sent by PHP script will be in

var urlip   = "http://checkip.dyndns.org/";
static var pubIP = "";
var xhr;


var URL = "localhost/PTUT/login.php"; //change for your URL
var hash = "hashcode"; //change your secret code, and remember to change into the PHP file too

private var textrect = Rect (10, 90,800, 800); //just make a GUI object rectangle

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

public class User
{
	public var noJoueur : int;
	public var pseudo : String;
	public var dateInscription : Date;
	public var tempsdeJeu : String;
	public var niveau : int;

	public function User(no : int, pse : String, dateI : Date, tempsJ : String, niv : int){
		noJoueur = no;
		pseudo = pse;
		dateInscription = dateI;
		tempsdeJeu = tempsJ;
		niveau = niv;
	}
}


function Login() {
    var form = new WWWForm(); //here you create a new form connection
    form.AddField( "myform_hash", hash ); //add your hash code to the field myform_hash, check that this variable name is the same as in PHP file
    form.AddField( "myform_nick", formNick );
    form.AddField( "myform_pass", formPassword );
    var w = new WWW(URL, form); //here we create a var called 'w' and we sync with our URL and the form
    yield w; //we wait for the form to check the PHP file, so our game dont just hang

    if (w.error != null) {
        print(w.error); //if there is an error, tell us
    } else {
    	var test=w.text;
    	print(test);
    	data = JSON.Parse(test);
    	var N = parseInt(data["Statut"]);
    	print(data["pseudo"].Value);
    	if(N==1){
    		print("ok");
    		print(data["pseudo"].Value);
    		infouser = new User(parseInt(data["noJoueur"]),data["pseudo"].Value,data["dateInscription"].AsDate,data["tempsdeJeu"].Value,parseInt(data["niveau"]));
    		print("ok");
    		CheckIP();
    		Debug.Log(pubIP);
    		//Application.LoadLevel("Main");
    	}else{
    		formText = data["message"]; //here we r	eturn the data our PHP told us
        }
        print("Test ok");
        w.Dispose(); //clear our form in game
    }


	/*request(readData);
	Debug.Log(xhr.reponseText);
	infouser = JSON.Parse(xhr.reponseText);*/



    formNick = ""; //just clean our variables
    formPassword = "";
}

function CheckIP(){
    var www : WWW = new WWW (urlip);
    yield www;
   pubIP = www.tex;
   //pubIP = pubIP.Substring(pubIP.IndexOf(“:”)+1);
   //pubIP = pubIP.Substring(0,pubIP.IndexOf(“<"));
    Debug.Log(pubIP);
}
/*
function getXMLHttpRequest() {
	var xhr = null;
	
	if (Application.XMLHttpRequest || Application.ActiveXObject) {
		if (Application.ActiveXObject) {
			try {
				xhr = new ActiveXObject("Msxml2.XMLHTTP");
			} catch(e) {
				xhr = new ActiveXObject("Microsoft.XMLHTTP");
			}
		} else {
			xhr = new XMLHttpRequest(); 
		}
	} else {
		alert("Votre navigateur ne supporte pas l'objet XMLHTTPRequest...");
		return null;
	}
	
	return xhr;
}
function request(callback) {
	xhr = getXMLHttpRequest();
	
	xhr.onreadystatechange = function() {
		if (xhr.readyState == 4 && (xhr.status == 200 || xhr.status == 0)) {
			callback(xhr.responseText);
		}
	};
	
	xhr.open("POST", url, true);
	xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
	xhr.send("myform_hash="+hash+"&myform_nick="+formNick+"&myform_pass="+formPassword);
}

function readData(sData) {
	// On peut maintenant traiter les données sans encombrer l'objet XHR.
	if (sData == "OK") {
		alert("C'est bon");
	} else {
		alert("Y'a eu un problème");
	}
}
*/