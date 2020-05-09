using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gère le déclenchement de différents événements selon la situation
/// </summary>
public class DeclencheurEvenement : MonoBehaviour
{
    //configuration des types d'événements enregistrés (click, collision, etc)
    [Header("Type d'événement enregistré")]
    //est-ce que c'est un événement déclenché par collision ?
    public bool evenementCollision = true;

    //est-ce que c'est un événement déclenché par clic ?
    public bool evenementClic = false;

    //configuration du type d'événement déclenché suite à l'événement enregistré (avertissement, ramasser, ouvrir, etc)
    [Header("Type d'événement déclenché")]

    //est-ce qu'on veut faire apparaitre du texte ?
    public bool apparaitreTexte = true;

    //est-ce qu'on veut ramasser un objet
    public bool ramasserObjet = false;

    //est-ce qu'on veut ouvrir une porte ?
    public bool ouvrirPorte = false;

    //est-ce qu'on veut utiliser une potion
    public bool utiliserPotion = false;

    //est-ce qu'on détruit l'objet après l'événement ?
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
                //si on veut faire apparaite un message
                if (apparaitreTexte)
                {
                    //déclenche l'avertisseur
                    DeclencheAvertisseur();
                }
                //si on veut faire ramasser un objet
                else if (ramasserObjet)
                {

                    DeclencherRamasserObjet();

                }
                //si on veut faire ouvrir une porte
                else if (ouvrirPorte)
                {
                    DeclencherOuvrirPorte();
                }
                //si on veut utiliser une potion
                else if (utiliserPotion)
                {
                    DeclencherUtiliserPotion();
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
            //obtenir une référence au joueur
            var refJoueur = GameObject.Find("joueur");

            //obtenir une référence au script du joueur
            var refScriptJoueur = refJoueur.GetComponent<ControlePerso>();

            //si le joueur sélectionne l'objet actuel (vérification additionnelle)
            if (refScriptJoueur.SelectCible() == gameObject)
            {
                //si on veut faire apparaite un message
                if (apparaitreTexte)
                {
                    //déclenche l'avertisseur
                    DeclencheAvertisseur();
                }
                //si on veut faire ramasser un objet
                else if (ramasserObjet)
                {

                    DeclencherRamasserObjet();

                }
                //si on veut faire ouvrir une porte
                else if (ouvrirPorte)
                {

                    DeclencherOuvrirPorte();
                }
                //si on veut utiliser une potion
                else if (utiliserPotion)
                {
                    DeclencherUtiliserPotion();
                }

            }

        }
    }

    //méthode pour déclencher un message
    public void DeclencheAvertisseur()
    {
        //si on a un composant d'avertissement, déclencher l'avertissement
        if (gameObject.TryGetComponent(out DeclencherAvertissement avertisseur))
        {
            avertisseur.Avertir();

            //détruire l'objet apres si besoin
            if (detruireApres)
            {
                //si on a un composant de survol, enlever l'information qu'elle affiche avant de détruire
                if (gameObject.TryGetComponent(out survolSouris survol)) survol.EnleverInfo();

                //supprimer le déclencheur d'événement
                Destroy(gameObject);
            }

        }
    }

    //méthode pour déclencher un ramassage d'objet
    public void DeclencherRamasserObjet()
    {
        //vérification qu'on a bien un composant objet
        if (gameObject.TryGetComponent(out ObjetInventaire objet))
        {
            //ramasser l'objet
            objet.RamasserObjet();

            //si en plus on a aussi un avertisseur attaché , le déclencher
            if (gameObject.TryGetComponent(out DeclencherAvertissement avertissement))
            {
                //déclencher l'avertissement
                avertissement.Avertir();
            }

            //détruire l'objet apres si besoin
            if (detruireApres)
            {
                //si on a un composant de survol, enlever l'information qu'elle affiche avant de détruire
                if (gameObject.TryGetComponent(out survolSouris survol)) survol.EnleverInfo();
                //supprimer le déclencheur d'événement
                Destroy(gameObject);
            }

        }
    }

    //méthode pour déclencher une ouverture de porte
    public void DeclencherOuvrirPorte()
    {

        // vérifier qu'on a un composant d'ouverture de porte
        if (gameObject.TryGetComponent(out OuvrirPorte porte))
        {
            //ouvrir la porte
            porte.OuvrirLaPorte();

            //détruire l'objet apres si besoin
            if (detruireApres)
            {
                //si on a un composant de survol, enlever l'information qu'elle affiche avant de détruire
                if (gameObject.TryGetComponent(out survolSouris survol)) survol.EnleverInfo();
                //supprimer le déclencheur d'événement
                Destroy(gameObject);
            }

        }
    }

    //méthode pour déclencher une utilisation de potion
    public void DeclencherUtiliserPotion()
    {
        //vérifier qu'on a un composant de potion
        if (gameObject.TryGetComponent(out LesPotions potion))
        {
            //utiliser la potion
            potion.UtiliserPotion();

            //détruire l'objet apres si besoin
            if (detruireApres)
            {
                //si on a un composant de survol, enlever l'information qu'elle affiche avant de détruire
                if (gameObject.TryGetComponent(out survolSouris survol)) survol.EnleverInfo();
                //supprimer le déclencheur d'événement
                Destroy(gameObject);
            }

        }
    }
}
