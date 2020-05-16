using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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



        }
    }

    //méthode pour terminer l'acte 1 / le jeu
    IEnumerator FinActe() {

        //apres 5 secondes, terminer le jeu
        yield return new WaitForSeconds(5f);

        //configurer la fin du jeu

        //charger la scene de fin
        SceneManager.LoadScene("SceneFin");

    }

}
