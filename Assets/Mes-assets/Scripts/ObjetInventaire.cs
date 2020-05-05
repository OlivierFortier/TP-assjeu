using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetInventaire : MonoBehaviour
{

    //référence au tag de l'objet
    [HideInInspector] public string tagObjet;
    

    private void Awake() {
        //initialisation du tag de l'objet
        tagObjet = gameObject.tag;
    }
    
    private void OnCollisionEnter(Collision autreObjet) {
        
        //si le joueur touche l'objet
        if(autreObjet.gameObject.name == "joueur") {

            //obtenir une référence à l'inventaire du joueur
            var refInventaire = autreObjet.gameObject.GetComponentInChildren<ScriptInventaire>();

            //ajouter l'objet dans l'inventaire 
            refInventaire.AjouterObjetInventaire(gameObject, tagObjet);


        }
    }
}
