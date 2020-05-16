using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Script pour gérer la scene de début et ses interactions
/// 
/// Author : Olivier Fortier
/// 
/// Date : 2020-05-16
/// </summary>
public class ScriptSceneDebut : MonoBehaviour
{
    //référence à l'objet d'input de texte
    public GameObject monInput;

    //référence à l'objet qui contient le message à afficher quand le joueur ne rentre pas de nom
    public GameObject messageErreur;

    //variable statique pour garder en mémoire le nom du joueur sur plusieurs scènes
    public static string nomDuJoueur;

    //référence au bouton qui permet au joueur de commencer la partie lorsqu'il clique dessus
    public GameObject boutonJouer;

    //référence au message d'information et de contexte qui est donnée au joueur une fois qu'il a entré son nom
    public GameObject infoIntro;

    //méthode qui permet de valider si le joueur à bel et bien entré un nom valide
    //appelée par les événements du onEndEdit
    public void ValiderNom()
    {

        // on va chercher la valeur entrée dans le text du input
        string nomEntre = monInput.GetComponent<InputField>().text;

        //si il est vide
        if (nomEntre == "" || nomEntre == null)
        {
            //on affiche le message d'erreur
            messageErreur.SetActive(true);
            //on désactive le message d'erreur apres un certain temps (3s)
            StartCoroutine(DesactiverErreur());
        }
        //sinon
        else
        {
            //on ajoute à la variable statique la valeur saisie
            nomDuJoueur = monInput.GetComponent<InputField>().text;

            //on désactive le input
            monInput.SetActive(false);

            //on active le message d'info
            infoIntro.SetActive(true);

            //on active le bouton "jouer"
            boutonJouer.SetActive(true);
        }

    }

    //méthode pour attendre 3s avant de désactiver le message d'erreur de nom
    public IEnumerator DesactiverErreur()
    {

        //on attents 3 secondes
        yield return new WaitForSeconds(3f);

        //on désactive le message d'erreur
        messageErreur.SetActive(false);


    }

    //méthode pour commencer le jeu
    //apellée par le bouton "jouer"
    public void CommencerJeu()
    {
        //charge le scene de jeu
        SceneManager.LoadScene("SceneJeu");
    }
}
