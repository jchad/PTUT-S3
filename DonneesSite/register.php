<?php
require("Model/Model.php");
require("Model/UserManager.php");
$um=new UserManager();
// =============================================================================
$unityHash = $um->anti_injection($_POST["myform_hash"]);
$phpHash = "hashcode"; // same code in here as in your Unity game

$nick = $um->anti_injection($_POST["myform_nick"]);
$pass = $um->anti_injection($_POST["myform_pass"]);
$passbis = $um->anti_injection($_POST["myform_passbis"]);
$mail = $um->anti_injection($_POST["myform_mail"]);

/*vérifier taille chaine + utilsateur non existant*/
if(!$nick || !$pass || !$passbis || !$mail){
  echo "At least one field is empty.";
}else{
  if ($unityHash != $phpHash){
    echo "HASH code is diferent from your game, you infidel.";
  }else{
    if($pass!=$passbis){
      echo "Les deux mots de passe ne sont pas indentiques.";
    }else{
      $salt=mb_strimwidth(md5(rand()),0,4);
      $pass=crypt((trim($pass)),$salt);
      if (filter_var($mail, FILTER_VALIDATE_EMAIL)){
        $um->createUser($nick, $pass, $salt, $mail);
        echo "1";
      }else{
        echo "Adresse mail non valide";
      }
    }
  }
}

/*
you can also use this:
$nick = $_POST["myform_nick"];
$pass = $_POST["myform_pass"];
*/

?>
