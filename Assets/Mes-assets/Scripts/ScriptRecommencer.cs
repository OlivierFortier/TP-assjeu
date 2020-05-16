using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Permet de recharger la scene de jeu tout simplement
/// 
/// Author : Olivier Fortier
/// Date : 2020-05-16
/// </summary>
public class ScriptRecommencer : MonoBehaviour
{
    
    //méthode qu'on apelle a partir d'un bouton UI
    //permet de charger la scene de jeu
    public void Recommencer() {
        SceneManager.LoadScene("SceneJeu");
    }
}
