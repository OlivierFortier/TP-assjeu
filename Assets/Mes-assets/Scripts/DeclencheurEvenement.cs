using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeclencheurEvenement : MonoBehaviour
{

    public bool detruireApres = true;
    
    //lors de la collision avec le joueur
    private void OnCollisionEnter(Collision other) {

        if(other.gameObject.name == "joueur") {

            //si on a un composant d'avertissement, déclencher l'avertissement
            if(gameObject.TryGetComponent(out DeclencherAvertissement avertisseur)) {

                avertisseur.avertir();
                
                if(detruireApres){
                    //supprimer le déclencheur d'événement
                Destroy(gameObject);
                }
                

            }

        }
    }
}
