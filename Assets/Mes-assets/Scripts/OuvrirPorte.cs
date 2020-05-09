using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OuvrirPorte : MonoBehaviour
{
    //référence de la porte a déplacer/ouvrir
    public GameObject porteAouvrir;

    //référence de la clé qui ouvre la porte
    public GameObject cleQuiOuvreLaPorte;

    //reconnaitre l'état de la porte
    public bool porteEstOuverte = false;

    private void OnCollisionEnter(Collision autreObjet)
    {
        // //si le joueur déclenche cet événement
        // if (autreObjet.gameObject.name == "joueur")
        // {
        //     //obtenir la référence au script de l'inventaire
        //     var inv = autreObjet.gameObject.GetComponentInChildren<ScriptInventaire>();

        //     //si le joueur à la bonne clé dans son inventaire, ouvrir la porte
        //     if (inv.listeInventaire.Contains(cleQuiOuvreLaPorte))
        //     {
        //         //la porte s'ouvre par rotation
        //         porteAouvrir.transform.Rotate(new Vector3(0, 180f, 0), Space.Self);

        //         //oter l'objet de l'inventaire après l'avoir utilisé
        //         inv.EnleverObjetInventaire(cleQuiOuvreLaPorte, cleQuiOuvreLaPorte.GetComponent<ObjetInventaire>().tagObjet);

        //         //supprimer le gameobject de détection d'événement
        //         Destroy(gameObject);

        //     }//sinon afficher un message d'avertissement
        //     else {

        //         if(gameObject.TryGetComponent(out DeclencherAvertissement avertisseur)) {
        //             avertisseur.avertir();
        //         }
        //     }

        // }
    }

    public void OuvrirLaPorte() {
        //si le joueur déclenche cet événement

        if(!porteEstOuverte)
        {
            //obtenir la référence au script de l'inventaire
            var inv = GameObject.Find("joueur").GetComponentInChildren<ScriptInventaire>();

            //si le joueur à la bonne clé dans son inventaire, ouvrir la porte
            if (inv.listeInventaire.Contains(cleQuiOuvreLaPorte))
            {
                //la porte s'ouvre par rotation
                porteAouvrir.transform.Rotate(new Vector3(0, 180f, 0), Space.Self);

                //oter l'objet de l'inventaire après l'avoir utilisé
                inv.EnleverObjetInventaire(cleQuiOuvreLaPorte, cleQuiOuvreLaPorte.GetComponent<ObjetInventaire>().tagObjet);

                //supprimer le gameobject de détection d'événement
               // Destroy(gameObject);
               porteEstOuverte = true;

            }//sinon afficher un message d'avertissement
            else {

                if(gameObject.TryGetComponent(out DeclencherAvertissement avertisseur)) {
                    avertisseur.avertir();
                }
            }}

        
    }

}
