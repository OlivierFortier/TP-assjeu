using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Gère les fonctionnalités propres à un objet ramassable
/// </summary>
public class ObjetInventaire : MonoBehaviour
{

    //référence au tag de l'objet
    [HideInInspector] public string tagObjet;


    private void Awake()
    {
        //initialisation du tag de l'objet
        tagObjet = gameObject.tag;
    }

    //méthode pour ramasser un objet
    public void RamasserObjet()
    {
        //obtenir une référence à l'inventaire du joueur
        var refInventaire = GameObject.Find("joueur").gameObject.GetComponentInChildren<ScriptInventaire>();

        //ajouter l'objet dans l'inventaire 
        refInventaire.AjouterObjetInventaire(gameObject, tagObjet);
    }
}
