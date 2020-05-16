using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script pour gérer le déroulement de la fin du premier acte/jeu
/// 
/// Author : Olivier Fortier
/// Date : 2020-05-16
/// </summary>
public class FinPremierActe : MonoBehaviour
{

    private void Awake() {
        //mettre l'objet inactif par défaut pour ne pas accidentellement terminer le jeu quand le joueur marche sur le déclencheur d'événements
        gameObject.SetActive(false);
    }
    
    private void OnCollisionEnter(Collision other) {
        //si le joueur entre dans la collision
        if(other.gameObject.name == "joueur"){

            //déclenche le dialogue de fin du jeu
            GetComponent<DeclencherAvertissement>().Avertir();

            //on charge la scene de fin dans 5 secondes
            StartCoroutine(FinActe());

        }
    }

    //méthode pour terminer l'acte 1 / le jeu
    IEnumerator FinActe() {

        //désactiver le collider pour ne pas activer ce code deux fois de suite
        GetComponent<BoxCollider>().enabled = false;

        //apres 5 secondes, terminer le jeu
        yield return new WaitForSeconds(5f);

        //charger la scene de fin
        SceneManager.LoadScene("SceneFin");

    }

}
