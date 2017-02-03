<?php
  class UserManager extends Model{

    // PROTECTION AGAINST SQL INJECTION
    function anti_injection($sql, $formUse = true)
    {
    $sql = preg_replace("/(from|select|insert|delete|where|drop table|show tables|,|'|#|\*|--|\\\\)/i","",$sql);
    $sql = trim($sql);
    $sql = strip_tags($sql);
    if(!$formUse || !get_magic_quotes_gpc())
      $sql = addslashes($sql);
    return $sql;
    }

    //Return the password of a given user
    public function getPass($Login){
      $sql = 'Select mdp from joueur where pseudo = :identifiant';
      $req= $this->executerRequete($sql, array('identifiant' => $Login));
      $results = $req->fetch(PDO::FETCH_ASSOC);
      $req->closeCursor();
      return $results['mdp'];
    }

    public function getSalt($Login){
      $sql = 'Select Salt from joueur where pseudo = :identifiant';
      $req= $this->executerRequete($sql, array('identifiant' => $Login));
      $results = $req->fetch(PDO::FETCH_ASSOC);
      $req->closeCursor();
      return $results['Salt'];
    }

    public function setStatut($Login, $Param){
      $sql = 'Update joueur set Statut= :param where pseudo = :identifiant';
      $req= $this->executerRequete($sql, array('identifiant' => $Login, 'param' => $Param));
      $req->closeCursor();
    }

    public function getInfos($Login){
      $sql = 'Select Statut, noJoueur, pseudo, dateInscription, tempsdeJeu, niveau from joueur where pseudo = :identifiant';
      $req= $this->executerRequete($sql, array('identifiant' => $Login));
      $results = $req->fetch(PDO::FETCH_ASSOC);
      $req->closeCursor();
      return $results;
    }

    public function createUser($Login, $password, $salt, $mail){
      $sql = 'INSERT INTO joueur(pseudo, mdp, salt, adresseMail, dateInscription) VALUES (:p_pseudo , :p_mdp, :p_salt , :p_adressemail , sysdate())';
      $req= $this->executerRequete($sql, array('p_pseudo' => $Login, 'p_mdp' => $password, 'p_salt' => $salt, 'p_adressemail' => $mail));
      $req->closeCursor();
    }
  }
?>
