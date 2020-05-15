using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gère l'ouverture des portes.
/// Est configurable selon la clé et la porte
/// </summary>
public class OuvrirPorte : MonoBehaviour
{
    //référence de la porte a déplacer/ouvrir
    public GameObject porteAouvrir;

    //référence de la clé qui ouvre la porte
    public GameObject cleQuiOuvreLaPorte;

    //reconnaitre l'état de la porte
    public bool porteEstOuverte = false;

    //méthode pour effectuer une ouverture de porte selon la clé que le joueur possède
    public void OuvrirLaPorte()
    {
        //si le joueur déclenche cet événement

        if (!porteEstOuverte)
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
            else
            {

                if (gameObject.TryGetComponent(out DeclencherAvertissement avertisseur))
                {
                    avertisseur.Avertir();
                }
            }
        }


    }

    //méthode pour fermer une porte
    public void FermerLaPorte()
    {
        //si la porte est ouverte
        if (porteEstOuverte)
        {

            //la porte se ferme par rotation
            porteAouvrir.transform.Rotate(new Vector3(0, -180f, 0), Space.Self);

            porteEstOuverte = false;

        }

    }

}
