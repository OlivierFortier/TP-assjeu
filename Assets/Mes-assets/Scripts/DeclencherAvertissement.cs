using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeclencherAvertissement : MonoBehaviour
{

    [Header("Référence au prefab d'avertissement")]
    public GameObject refAvertissement;

    [Header("Entrez un message d'avertissement")]
    [TextArea(10,10)]
    public string messageAvertissement = "AVERTISSEMENT";

    [Header("temps d'affichage de l'avertissement")]
    public float delaiAvertissement = 2f;
    
    public void avertir() {
        var instanceAvertissement = Instantiate(refAvertissement) as GameObject;

            //parenter l'avertissement au canvas
            instanceAvertissement.transform.SetParent(GameObject.Find("Canvas").transform);

            //positionner l'avertissement dans le canvas 
            instanceAvertissement.transform.position = GameObject.Find("vie-joueur").transform.position + new Vector3(0, GameObject.Find("vie-joueur").transform.localScale.y - instanceAvertissement.transform.localPosition.y, 0);

            //ajouter le texte du message d'avertissement défini dans l'inspecteur
            instanceAvertissement.GetComponentInChildren<Text>().text = messageAvertissement;

            //enlever l'avertissement apres 1.5 seconde
            Destroy(instanceAvertissement, delaiAvertissement);
    }
}
