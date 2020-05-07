using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttaquerArme : MonoBehaviour
{
    public GameObject refJoueur;


    public void OnCollisionEnter(Collision cibleTouche) {

        if(cibleTouche.gameObject.CompareTag("ennemi")) {

            cibleTouche.gameObject.GetComponent<ScriptEnnemi>().vieActuelle -= refJoueur.GetComponent<ControlePerso>().dommagesAttaque;
        }
        
    }
}
