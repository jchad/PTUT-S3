<?php
require("Model/Model.php");
require("Model/UserManager.php");
$um=new UserManager();
// =============================================================================
$unityHash = $um->anti_injection($_POST["myform_hash"]);
$phpHash = "hashcode"; // same code in here as in your Unity game

$nick = $um->anti_injection($_POST["myform_nick"]);
$pass = $um->anti_injection($_POST["myform_pass"]);
$pass = trim($pass);
$salt = $um->getSalt($nick);
/*
you can also use this:
$nick = $_POST["myform_nick"];
$pass = $_POST["myform_pass"];
*/

/*verif que l'utilisateur n'est pas déjà connecté*/
if(!$nick || !$pass){
  $password=$um->getPass("Kinder");
  if ($password != crypt("bonjour",$salt)) {
    echo "la connexion est bonne";
  }
  echo "Login or password cant be empty.";
}else{
  if ($unityHash != $phpHash){
    echo "HASH code is different from your game, you infidel.";
  }else{
    $password=$um->getPass($nick);
    if($password==NULL){
      $erreur='Utilisateur inexistant';
    }else{
      if(crypt($pass,$salt)!=$password){
        echo 'Mot de passe incorect';
      }else{
        $_SESSION['Login']=$nick;
        $um->setStatut($nick,1);
        $infos=$um->getInfos($nick);
        echo ''.$infos['Statut'].';'.$infos['pseudo'].';'.$infos['dateInscription'].'';
      }
    }
  }
}
?>
