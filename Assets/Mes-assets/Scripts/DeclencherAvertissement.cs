using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gère l'affichage d'un message de texte
/// </summary>
public class DeclencherAvertissement : MonoBehaviour
{

    [Header("Référence au prefab d'avertissement")]
    //référence au prefab d'avertissement
    public GameObject refAvertissement;

    [Header("Entrez un message d'avertissement")]
    [TextArea(10, 10)]
    //boite de texte pour entrer un message
    public string messageAvertissement = "AVERTISSEMENT";

    [Header("temps d'affichage de l'avertissement")]
    //temps avant qu'on détruit le message
    public float delaiAvertissement = 2f;

    //méthode pour faire apparaitre un avertissement de texte
    public void Avertir()
    {
        //instancier l'avertissement
        var instanceAvertissement = Instantiate(refAvertissement) as GameObject;

        //parenter l'avertissement au canvas
        instanceAvertissement.transform.SetParent(GameObject.Find("Canvas").transform);

        //positionner l'avertissement dans le canvas 
        instanceAvertissement.transform.position = new Vector3(Screen.width * 0.75f, Screen.height / 2, 0);

        //ajouter le texte du message d'avertissement défini dans l'inspecteur
        instanceAvertissement.GetComponentInChildren<Text>().text = messageAvertissement;

        //enlever l'avertissement apres un délai
        Destroy(instanceAvertissement, delaiAvertissement);
    }
}
