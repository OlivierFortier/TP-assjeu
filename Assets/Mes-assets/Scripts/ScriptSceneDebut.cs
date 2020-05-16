using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScriptSceneDebut : MonoBehaviour
{
    public GameObject monInput;

    public GameObject messageErreur;

    public static string nomDuJoueur;

    public GameObject boutonJouer;

    public void ValiderNom() {

        string nomEntre = monInput.GetComponent<InputField>().text;

        if(nomEntre == "" || nomEntre == null) {
            messageErreur.SetActive(true);
            StartCoroutine(DesactiverErreur());
        }
        else {

            nomDuJoueur = monInput.GetComponent<InputField>().text;

            monInput.SetActive(false);

           boutonJouer.SetActive(true);
        }

    }

    public IEnumerator DesactiverErreur() {

        yield return new WaitForSeconds(3f);

        messageErreur.SetActive(false);
        

    }
    
    public void CommencerJeu() {

        SceneManager.LoadScene("SceneJeu");
    }
}
