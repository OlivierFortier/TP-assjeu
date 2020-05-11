using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Classe pour gérer le survol des entités et objets avec la souris et les mettre en surbrillance et afficher le nom
///
/// Author : Olivier Fortier
/// </summary>
public class survolSouris : MonoBehaviour
{
    #region propriétés

    [Header("ajuster position texte")]
    //pour ajuster la position de la barre de vie dans l'inspecteur si besoin
    public Vector3 offsetPositionTexte = new Vector3(0, 1.2f, 0);

    [Header("prefab texte")]
    //le prefab texte qui affichera l'information de l'objet/personnage
    public GameObject infoTexte;

    [Header("prefab barre vie")]
    //le prefab de barre de vie pour la vie des personnages/ennemis, etc si besoin
    public GameObject barreVie;

    [Header("Configuration du renderer et de la couleur")]
    //l'objet sur lequel on veut survoler et affecter le renderer
    public GameObject objetASurvoler;

    //est-ce qu'on met un contour lumineux
    public bool mettreContour = true;

    [HideInInspector] public bool contourDejaMis = false;

    public bool couleurRouge = true; //0 

    public bool couleurBleu = false; //2

    public bool couleurVert = false; //1

     public List<cakeslice.Outline> listeComposanteContour;

    [HideInInspector] public int couleurContour = 0;

    [HideInInspector]
    //contiendra l'instance du prefab de texte
    public GameObject instanceTexte;

    [HideInInspector]
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

        if (infoTexte)
        {
            //instancier un texte
            instanceTexte = Instantiate(infoTexte) as GameObject;

            //mettre l'instance de texte en enfant du canvas
            instanceTexte.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }
        #endregion



    }

    //lorsqu'on sort du survol de l'objet
    private void OnMouseExit()
    {
        EnleverInfo();

    }

    public void EnleverInfo()
    {
        //mettre la variable à false
        surSouris = false;

        //détruire l'instance de texte
        if (instanceTexte) Destroy(instanceTexte);

        if (barreVie && instanceBarreVie) Destroy(instanceBarreVie.gameObject);
    }

    public void choisirCouleurContour()
    {

        if (couleurRouge) couleurContour = 0;
        else if (couleurVert) couleurContour = 1;
        else if (couleurBleu) couleurContour = 2;

    }

    private void Start()
    {
        //par défaut, on survole rien donc la variable est fausse
        surSouris = false;

        if (infoTexte)
        {
            //initialiser le texte à vide
            infoTexte.GetComponent<Text>().text = "";
        }

        choisirCouleurContour();


    }

    private void Update()
    {


        GestionContour();

        foreach (cakeslice.Outline unContour in listeComposanteContour)
        {
            unContour.enabled = surSouris;

        }

        //au survol de la souris, afficher les informations textuelles sur l'objet ou le personnage
        if (surSouris)
        {
            if (infoTexte || barreVie)
            {
                //on obtient la position de l'entité/objet et on la convertit en position dans l'écran
                Vector3 posObjet = Camera.main.WorldToScreenPoint(objetASurvoler.transform.position);
                //la position désirée du texte à afficher à l'écran
                Vector3 posTexte = new Vector3(posObjet.x + offsetPositionTexte.x, posObjet.y * offsetPositionTexte.y, Input.mousePosition.z);


                if (barreVie && instanceBarreVie)
                {

                    //ajuster la barre de vie selon la vie de l'ennemi
                    instanceBarreVie.transform.Find("vie-pleine").gameObject.GetComponent<Image>().fillAmount = ((gameObject.GetComponent<ScriptEnnemi>().vieActuelle * 1) / gameObject.GetComponent<ScriptEnnemi>().vieMaximum);
                }

                if (infoTexte && instanceTexte)
                {
                    //changer la position du texte à l'écran pour celle désirée selon si c'est un ennemi ou autre
                    if (!gameObject.CompareTag("ennemi"))
                    {
                        instanceTexte.GetComponent<Text>().rectTransform.position = posTexte;
                    }
                    else
                    {
                        instanceTexte.GetComponent<Text>().rectTransform.position = instanceBarreVie.transform.position;
                    }

                    //changer le contenu du texte pour l'information de l'objet actuelle
                    instanceTexte.GetComponent<Text>().text = gameObject.name;
                }
            }
        }
    }

    public void GestionContour()
    {
        if (!contourDejaMis)
        {
            if (mettreContour)
            {//activer ou désactiver le script du asset store qui produit un contour sur l'objet sélectionné
                if (gameObject.TryGetComponent(out Renderer render) && !gameObject.TryGetComponent(out ParticleSystem part))
                {
                    var contoure = gameObject.AddComponent<cakeslice.Outline>();

                    contoure.color = couleurContour;

                    listeComposanteContour.Add(contoure);

                    //contour.enabled = surSouris;
                }
                else
                {
                    foreach (Transform enfant in transform)
                    {
                        if (enfant.TryGetComponent(out Renderer renderEnfant) && !enfant.TryGetComponent(out ParticleSystem partee))
                        {
                            var contour = enfant.gameObject.AddComponent<cakeslice.Outline>();

                            contour.color = couleurContour;

                            listeComposanteContour.Add(contour);

                            // contour.enabled = surSouris;
                        }

                        else
                        {
                            foreach (Transform enfantEnfant in enfant)
                            {
                                if (enfantEnfant.TryGetComponent(out Renderer RenderEnfantEnfant) && !enfantEnfant.TryGetComponent(out ParticleSystem parte))
                                {
                                    var contouree = enfantEnfant.gameObject.AddComponent<cakeslice.Outline>();

                                    contouree.color = couleurContour;

                                    listeComposanteContour.Add(contouree);

                                    //contour.enabled = surSouris;
                                }
                                else
                                {

                                    foreach (Transform enfantEnfantEnfant in enfantEnfant)
                                    {
                                        if (enfantEnfantEnfant.TryGetComponent(out Renderer renderEnfantEnfantEnfant) && !enfantEnfantEnfant.TryGetComponent(out ParticleSystem parteee))
                                        {
                                            var contoureee = enfantEnfantEnfant.gameObject.AddComponent<cakeslice.Outline>();

                                            contoureee.color = couleurContour;

                                            listeComposanteContour.Add(contoureee);

                                            //contour.enabled = surSouris;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
            contourDejaMis = true;
        }
    }
}