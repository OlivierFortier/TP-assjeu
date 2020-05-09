using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptInventaire : MonoBehaviour
{
    [Header("UI pour avertissement plein")]
    //référence à l'élément UI qui averti le joueur qu'il ne peut pas ramasser l'objet car son inventaire est plein
    public GameObject avertissementInvPlein;

    [TextArea(10,10)]
    public string messageAvertissement = "avertissement";

    [Header("Inventaire")]
    //représentation de l'inventaire (limité à 2 places)
    public List<GameObject> listeInventaire;


    public void AjouterObjetInventaire(GameObject objet, string tag = "Untagged")
    {
        //si il y a de la place dans l'inventaire (2 places)
        if (listeInventaire.Count < 2)
        {
            //ajouter l'objet dans l'inventaire
            listeInventaire.Add(objet);

            //parenter l'objet à l'inventaire
            objet.transform.SetParent(GameObject.Find("inventaire").transform);

            //ajouter l'icone de l'objet à l'inventaire
            AjouterImageInventaire(objet, tag);

            //faire disparaitre l'objet (mais il reste dans l'inventaire)
            objet.SetActive(false);
        }
        else
        {   
            //sinon, déclencher un avertissement comme quoi l'inventaire est plein
            if(gameObject.TryGetComponent(out DeclencherAvertissement avertisseur)) {
                    avertisseur.avertir();
                }
        }
    }

    //méthode pour insérer l'icone dans l'inventaire du joueur lorsqu'il ramasse un objet
    public void AjouterImageInventaire(GameObject objet, string tag = "Untagged")
    {
        //aller chercher l'icone associée a l'objet
        var iconePrefab = objet.GetComponent<AssocierIcone>().iconeAssocie;

        //instancier l'icone et la mettre dans le canvas
        var uneIcone = Instantiate(iconePrefab) as GameObject;

        uneIcone.transform.SetParent(GameObject.Find("Canvas").transform);

        //mettre le tag a celui défini dans l'inspecteur
        uneIcone.tag = tag;

        //si c'est le premier item ramassé, le mettre dans la premiere place de l'inventaire
        if (listeInventaire.Count == 1)
        {
            uneIcone.transform.position = GameObject.Find("conteneur-objet-gauche").transform.position;
        } //sinon, le mettre dans la 2e place
        else if (listeInventaire.Count == 2)
        {
            uneIcone.transform.position = GameObject.Find("conteneur-objet-droit").transform.position;
        }
    }

    //méthode pour enlever un objet de l'inventaire
    //par exemple lorsqu'on a fini d'utiliser un objet ou qu'on le consomme
    public void EnleverObjetInventaire(GameObject objet, string tag = "Untagged")
    {
        //enlever l'objet de l'inventaire
        listeInventaire.Remove(objet);

        //enlever l'icone de l'objet de l'inventaire
        EnleverImageInventaire(objet, tag);
    }

    //méthode pour enlever une icone de l'inventaire
    public void EnleverImageInventaire(GameObject objet, string tag = "Untagged")
    {
        //supprimer l'objet de UI de l'icone
        Destroy(GameObject.FindGameObjectWithTag(tag));

        //réorganiser l'inventaire si besoin
        if (listeInventaire.Count > 0)
        {
            ReorganiserInventaire(listeInventaire[0].tag);
        }

    }

    //méthode pour réorganiser l'ordre de l'inventaire
    public void ReorganiserInventaire(string tagIcone)
    {
        //aller chercher l'icone à replacer
        GameObject iconeAplacer = GameObject.FindGameObjectWithTag(tagIcone);
        //replace dans l'UI la position de l'icone de l'objet
        iconeAplacer.transform.position = GameObject.Find("conteneur-objet-gauche").transform.position;

    }
}
