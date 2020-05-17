using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script pour déclencher la cinématique de survol de la ville
/// </summary>
public class ScriptEntrerVille : MonoBehaviour
{

    //référence a la caméra qui va survoler la ville comme cinématique
    public GameObject cameraSurvolVille;

    private void OnCollisionEnter(Collision autreObjet) {
        //quand on rentre en collision avec le joueur
        if(autreObjet.gameObject.name == "joueur") {
            //on désactive le collider pour ne pas activer cet événement plusieurs fois
            GetComponent<BoxCollider>().enabled = false;
            //on désactive la caméra actuelle
            Camera.main.gameObject.SetActive(false);

            //on active la caméra de cinématique
            cameraSurvolVille.SetActive(true);
        }
    }

    
}
