using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionVie : MonoBehaviour
{
    [Header("Configuration de la potion")]
    //configuer combien de vie on veut que la potion restaure dans l'éditeur
    public float vieRestaure = 10;


    private void OnCollisionEnter(Collision autreObjet) {
        
        //si le joueur touche la potion
        if(autreObjet.gameObject.name == "joueur") {


        //obtiens les références a la vie du perso
        var lePerso = autreObjet.gameObject.GetComponent<ControlePerso>();

            //si le joueur à moins que sa vie maximum, lui restaurer de la vie
          if(lePerso.viePersonnage < lePerso.viePersonnageMax) {
              lePerso.viePersonnage += vieRestaure;

                //si la potion fais que la vie du perso dépasse le maximum, le remettre au maximum
              if(lePerso.viePersonnage > lePerso.viePersonnageMax) {
                  lePerso.viePersonnage = lePerso.viePersonnageMax;
              }
          }
          //faire disparaitre la potion
          Destroy(gameObject);
        }
    }

}
