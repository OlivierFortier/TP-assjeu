﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe pour gérer le survol des entités et objets avec la souris et les mettre en surbrillance et afficher le nom
///
/// Author : Olivier Fortier
/// </summary>
public class survolSouris : MonoBehaviour
{
    #region propriétés

    [Header("ajuster position barre vie")]
    //pour ajuster la position de la barre de vie dans l'inspecteur si besoin
    public Vector3 offsetPositionBarreVie;

    [Header("prefab texte")]
    //le prefab texte qui affichera l'information de l'objet/personnage
    public GameObject infoTexte;

    [Header("prefab barre vie")]
    //le prefab de barre de vie pour la vie des personnages/ennemis, etc si besoin
    public GameObject barreVie;

    [Header("gameobject à affecter le renderer")]
    //l'objet sur lequel on veut survoler et affecter le renderer
    public GameObject objetASurvoler;

    [Header("instance prefab texte")]
    //contiendra l'instance du prefab de texte
    public GameObject instanceTexte;

    [Header("instance prefab barre vie")]
    //contiendra l'instance du prefab de la barre de vie d'un perso/ennemi si besoin
    public GameObject instanceBarreVie;

    //booléen pour déterminer si on survole ou pas l'objet ou le personnage
    private bool surSouris;
    #endregion

    //lorsqu'on entre en survol de l'objet
    private void OnMouseEnter()
    {
        //mettre la variable à true
        surSouris = true;



        #region barre vie
        //si on a affaire a un ennemi, instancier la barre de vie
        if (gameObject.CompareTag("ennemi"))
        {
            instanceBarreVie = Instantiate(barreVie) as GameObject;

            instanceBarreVie.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }


        #endregion

        #region texte

        //instancier un texte
        instanceTexte = Instantiate(infoTexte) as GameObject;

        //mettre l'instance de texte en enfant du canvas
        instanceTexte.transform.SetParent(GameObject.Find("Canvas").transform, false);
        #endregion



    }

    //lorsqu'on sort du survol de l'objet
    private void OnMouseExit()
    {
        //mettre la variable à false
        surSouris = false;

        //détruire l'instance de texte
        if(instanceTexte)Destroy(instanceTexte);
        
        if(barreVie && instanceBarreVie) Destroy(instanceBarreVie.gameObject);
        
    }

    private void Start()
    {
        //par défaut, on survole rien donc la variable est fausse
        surSouris = false;
        //initialiser le texte à vide
        infoTexte.GetComponent<Text>().text = "";

    }

    private void Update()
    {
        //activer ou désactiver le script du asset store qui produit un contour sur l'objet sélectionné
        objetASurvoler.GetComponent<cakeslice.Outline>().enabled = surSouris;

        //au survol de la souris, afficher les informations textuelles sur l'objet ou le personnage
        if (surSouris)
        {
            //on obtient la position de l'entité/objet et on la convertit en position dans l'écran
           Vector3 posObjet = Camera.main.WorldToScreenPoint(objetASurvoler.transform.position);
            //la position désirée du texte à afficher à l'écran
           Vector3 posTexte = new Vector3(posObjet.x + offsetPositionBarreVie.x, posObjet.y * offsetPositionBarreVie.y, Input.mousePosition.z);

            if (barreVie && instanceBarreVie)
            {
                    //TODO : mettre la barre de vie en position statique au milieu en haut de l'écran
                //calculer la position de la barre de vie
                Vector3 posBarreVie = new Vector3(posObjet.x + offsetPositionBarreVie.x, posObjet.y * offsetPositionBarreVie.y, Input.mousePosition.z);

                //appliquer la position à l'instance de la barre de vie
                instanceBarreVie.GetComponent<RectTransform>().position = posBarreVie;

                //ajuster la barre de vie selon la vie de l'ennemi
                instanceBarreVie.transform.Find("vie-pleine").gameObject.GetComponent<Image>().fillAmount = ((gameObject.GetComponent<ScriptEnnemi>().vieActuelle * 1) / gameObject.GetComponent<ScriptEnnemi>().vieMaximum);
            }

            if(infoTexte && instanceTexte){
            //changer la position du texte à l'écran pour celle désirée
            instanceTexte.GetComponent<Text>().rectTransform.position = posTexte;

            //changer le contenu du texte pour l'information de l'objet actuelle
            instanceTexte.GetComponent<Text>().text = gameObject.name;}

        }
    }
}