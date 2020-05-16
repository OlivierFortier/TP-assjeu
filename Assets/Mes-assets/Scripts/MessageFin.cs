using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script pour mettre à jour le texte de fin avec le nom du joueur
/// </summary>
public class MessageFin : MonoBehaviour
{
    
    private void Update() {
        
        //on va chercher le composant texte sur l'objet actuel
        Text monTexte = GetComponent<Text>();

        //on met a jour le texte avec le nom du joueur qui est dans une variable statique
        monTexte.text = $"Merci d'avoir joué, {ScriptSceneDebut.nomDuJoueur}.";

    }

}
