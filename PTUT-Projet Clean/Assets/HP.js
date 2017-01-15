

var actHP : int = 3;
var maxHP : int = 3;

var HPText : Texture2D;
var HPText2 : Texture2D;
var HPText3 : Texture2D;
var HPLenght : float;

function OnGUI () 
{
	if (actHP >= 1){
    	GUI.DrawTexture(Rect(10, 550, HPLenght, 100), HPText);
    }
    if (actHP >= 2){
    	GUI.DrawTexture(Rect(110, 550, HPLenght, 100), HPText2);
    }
    if (actHP == 3){
    	GUI.DrawTexture(Rect(210, 550, HPLenght, 100), HPText3);
    }
}

/*function Update () {
    HPLenght = 100;
    if(Input.GetKeyDown("t"))
    {
        actHP -= 1;
    }
}*/