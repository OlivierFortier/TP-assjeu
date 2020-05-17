using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gère le fonctionnement des différentes potions.
/// Configurable dans l'inspecteur
/// </summary>
public class LesPotions : MonoBehaviour
{
    [Header("Type de potion")]
    //est-ce une potion de vie ?
    public bool potionDeVie = true;
    //est-ce une potion de vitesse ?
    public bool potionDeVitesse = false;
    //est-ce une potion de force ?
    public bool potionDeForce = false;

    //référence au son de potion quand le joueur en prends une
    public AudioClip sonPotion;

    [Header("Config potion vie")]
    //configuer combien de vie on veut que la potion restaure dans l'éditeur
    public float vieRestaure = 25;

    [Header("Config potion vitesse")]
    //configuer combien de vitesse on veut que la potion augmente
    public float vitesseAugmente = 10;

    [Header("Config potion force")]
    //configuer combien de force on veut que la potion augmente
    public float forceAugmente = 10;

    //méthode pour utiliser la potion selon sa configuration
    public void UtiliserPotion()
    {
        //obtiens les références a la vie du perso
        var lePerso = GameObject.Find("joueur").GetComponent<ControlePerso>();

        //jouer le son de potion
        GetComponent<AudioSource>().PlayOneShot(sonPotion);

        //si c'est une potion de vie
        if (potionDeVie)
        {
            //si le joueur à moins que sa vie maximum, lui restaurer de la vie
            if (lePerso.viePersonnage < lePerso.viePersonnageMax)
            {
                lePerso.viePersonnage += vieRestaure;

                //si la potion fais que la vie du perso dépasse le maximum, le remettre au maximum
                if (lePerso.viePersonnage > lePerso.viePersonnageMax)
                {
                    lePerso.viePersonnage = lePerso.viePersonnageMax;
                }
            }
            //faire disparaitre la potion
            //Destroy(gameObject);
        }
        //si c'est une potion de vitesse
        else if (potionDeVitesse)
        {
            //A FAIRE
        }
        //si c'est une potion de force
        else if (potionDeForce)
        {
            //A FAIRE
        }

    }

}
