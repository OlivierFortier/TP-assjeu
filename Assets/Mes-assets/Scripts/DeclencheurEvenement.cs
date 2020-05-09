using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeclencheurEvenement : MonoBehaviour
{

    [Header("Type d'événement enregistré")]
    //est-ce que c'est un événement déclenché par collision ?
    public bool evenementCollision = true;

    //est-ce que c'est un événement déclenché par clic ?
    public bool evenementClic = false;

    [Header("Type d'événement déclenché")]

    public bool apparaitreTexte = true;

    public bool ramasserObjet = false;

    public bool ouvrirPorte = false;

    [Header("Est-ce qu'on détruit l'événement apres ?")]
    public bool detruireApres = true;


    //lors de la collision avec le joueur
    private void OnCollisionEnter(Collision other)
    {
        //si c'est un événement qu'on déclenche en collision
        if (evenementCollision)
        {
            //validation que la collision se passe avec le joueur
            if (other.gameObject.name == "joueur")
            {
                if (apparaitreTexte)
                {
                    //déclenche l'avertisseur
                    DeclencheAvertisseur();
                }

                else if (ramasserObjet) {
                    //A FAIRE PLUS TARD
                    return;
                }

                else if(ouvrirPorte) {
                    //A FAIRE PLUS TARD
                    return;
                }

            }
        }
    }

    //méthode pour la gestion d'un événement de clic
    public void EvenementCliquer()
    {
        //vérifier si c'est un événement de clic attaché
        if (evenementClic)
        {
            var refJoueur = GameObject.Find("joueur");

            var refScriptJoueur = refJoueur.GetComponent<ControlePerso>();

            //si le joueur sélectionne l'objet actuel (vérification additionnelle)
            if (refScriptJoueur.SelectCible() == gameObject)
            {
                 if (apparaitreTexte)
                {
                    //déclenche l'avertisseur
                    DeclencheAvertisseur();
                }

                else if (ramasserObjet) {
                    //A FAIRE PLUS TARD
                    return;
                }

                else if(ouvrirPorte) {
                    //A FAIRE PLUS TARD
                    return;
                }

            }

        }
    }

    public void DeclencheAvertisseur()
    {
        //si on a un composant d'avertissement, déclencher l'avertissement
        if (gameObject.TryGetComponent(out DeclencherAvertissement avertisseur))
        {
            avertisseur.avertir();

            if (detruireApres)
            {
                //supprimer le déclencheur d'événement
                Destroy(gameObject);
            }

        }
    }
}
