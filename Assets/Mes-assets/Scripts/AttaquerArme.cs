using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttaquerArme : MonoBehaviour
{
    public GameObject refJoueur;

    private void Awake() {
        Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), refJoueur.GetComponent<CapsuleCollider>());
    }


    public void OnTriggerEnter(Collider cibleTouche) {

        if(cibleTouche.gameObject.CompareTag("ennemi")) {

            cibleTouche.gameObject.GetComponent<ScriptEnnemi>().vieActuelle -= refJoueur.GetComponent<ControlePerso>().dommagesAttaque;
        }
        
    }
}
