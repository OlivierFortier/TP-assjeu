using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gère la collision entre l'arme et un ennemi
/// </summary>
public class AttaquerArme : MonoBehaviour
{
    //référence au joueur
    public GameObject refJoueur;


    private void Awake() {
        //ignorer la collision entre le joueur et l'arme
        Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), refJoueur.GetComponent<CapsuleCollider>());
    }


    public void OnTriggerEnter(Collider cibleTouche) {

        //si l'arme entre en collision avec un ennemi
        if(cibleTouche.gameObject.CompareTag("ennemi")) {
            //lui causer des dommages
            cibleTouche.gameObject.GetComponent<ScriptEnnemi>().vieActuelle -= refJoueur.GetComponent<ControlePerso>().dommagesAttaque;
        }
        
    }
}
