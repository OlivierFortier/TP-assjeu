using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesPotions : MonoBehaviour
{
    [Header("Type de potion")]

    public bool potionDeVie = true;

    public bool potionDeVitesse = false;

    public bool potionDeForce = false;

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
