<?php
require("Model/Model.php");
require("Model/UserManager.php");
$um=new UserManager();
// =============================================================================
$unityHash = $_GET["hash"];
$phpHash = "hashcode"; // same code in here as in your Unity game
$nick = $_GET["nick"];

if ($unityHash != $phpHash){
  echo "HASH code is diferent from your game, you infidel.";
}else{
  $um->setStatut($nick,0);/*form avec chanp invisible pour passage du pseudo pour déconnexion*/
}
?>
