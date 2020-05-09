using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personnage : MonoBehaviour
{

    //référence au personnage du joueur
    public GameObject refJoueur;

    //par défaut le personnage ne regarde pas le joueur
    public bool regarderJoueur = false;
    
    void Update()
    {
        
        FaireRegarderJoueur();
        
        
        
    }

    void FaireRegarderJoueur() {
        if(regarderJoueur) {
            //regarder le joueur, seulement en bougant sur l'axe Y
            transform.LookAt(new Vector3(refJoueur.transform.position.x, transform.position.y, refJoueur.transform.position.z));
        }
    }
}
