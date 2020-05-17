using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Script unique pour le déroulement du combat de l'arène.
/// 
/// Author: Olivier Fortier
/// Date: 2020-05-16
/// </summary>
public class ScriptCombatArene : MonoBehaviour
{

    //référence au groupe d'instanciation des combatans de l'arène
    public GameObject refCombatantsArene;

    //référence au groupe d'instanciation des potions de l'arène
    public GameObject refPotionsArene;

    //référence au prefab de sceptre récompensé au joueur à la fin du combat par le chef de l'arene
    public GameObject prefabSceptreGagner;

    //référence au prefab du personnage du chef de la compagnie
    public GameObject prefabChefSageCompagnie;

    //référence à la porte de l'arene à ouvrir après la fin du combat
    public GameObject porteArene;

    //contiens si le combat est terminé ou pas
    private bool areneTerminer = false;

    //référence à l'événement déclenché à la fin du combat
    public GameObject evenementFinArene;

    //référence a l'objet qui contient l'événement de la fin de l'acte
    public GameObject evenementFinActe;

    //référence à l'objet musique qui fais jouer la musique en 2D
    public GameObject objetMusique;

    //référence a la musique qu'on fera jouer pendant le combat d'arene
    public AudioClip musiqueArene;

    //référence à la musique normale pour le remettre quand l'arene est finie
    public AudioClip musiqueNormale;

    private void Awake()
    {
        //on désactive l'événement de fin du combat, on va l'activer plus tard
        evenementFinArene.SetActive(false);
    }

    private void Start()
    {

        //boucle pour désactiver le renderer de chaque point d'instance pour pas que le joueur voit des "plane"
        foreach (Transform enfant in transform)
        {
            //on désactive le renderer
            enfant.GetComponent<Renderer>().enabled = false;
        }

    }

    private void OnCollisionEnter(Collision autreObjet)
    {
        //si le joueur entre en collision avec le collider , déclenche le début du combat de l'arène
        if (autreObjet.gameObject.name == "joueur")
        {
            //active les scripts pour instancier les potions et les combatans de l'arene
            refCombatantsArene.GetComponent<InstancierAuxPoints>().enabled = true;
            refPotionsArene.GetComponent<InstancierAuxPoints>().enabled = true;

            objetMusique.GetComponent<AudioSource>().clip = musiqueArene;

            objetMusique.GetComponent<AudioSource>().Play();

        }
    }

    private void Update()
    {

        //si on arrive à 15 ennemis apparus au total
        if (refCombatantsArene.GetComponent<InstancierAuxPoints>().nombreObjetsInstanciesTotal >= 15)
        {
            //terminer le combat de l'arène si ce n'est pas déja fait
            if (!areneTerminer)
                FinCombatArene();


        }
    }

    //méthode pour déclencher la fin du combat de l'arène
    void FinCombatArene()
    {
        //on désactive le collider pour ne pas activer l'événement 2 fois de suite
        var coll = GetComponent<BoxCollider>();
        coll.enabled = false;

        //on active l'objet d'événement de fin d'arène
        evenementFinArene.SetActive(true);

        //on ouvre la porte de l'arene pour que le joueur puisse sortir
        porteArene.transform.Rotate(new Vector3(0, 180f, 0), Space.Self);

        //on désactive les instanciations d'ennemis et de potions dans l'arène
        refCombatantsArene.GetComponent<InstancierAuxPoints>().enabled = false;
        refPotionsArene.GetComponent<InstancierAuxPoints>().enabled = false;

        //on instancie le chef de la compagnie
        PlacerChefCompagnie();

        //on instancie le sceptre ancien
        PlacerSceptre();

        //l'arene est terminée
        areneTerminer = true;

        //remettre la musique a normal
        objetMusique.GetComponent<AudioSource>().clip = musiqueNormale;

        objetMusique.GetComponent<AudioSource>().Play();

        //on active l'événement de fin de l'acte, déclenché quand le joueur sort de l'arene
        evenementFinActe.SetActive(true);
    }

    //méthode pour instancier le chef de la compagnie
    void PlacerChefCompagnie()
    {
        //on va chercher le premier point d'instance, c'est la qu'on va positionner le chef
        var posChef = transform.GetChild(0).transform.position;

        //on instancie le chef
        var chefSageCompagnie = Instantiate(prefabChefSageCompagnie, posChef, Quaternion.identity);

        //on met son nom au meme nom que le prefab lui-meme
        chefSageCompagnie.gameObject.name = prefabChefSageCompagnie.gameObject.name;

        //pour contenir la position la plus pres
        NavMeshHit posLaPlusProche;

        //si il y a quelque chose à la position qu'on veut instancier le perso , alors on va le positionner à un endroit proche
        if (NavMesh.SamplePosition(chefSageCompagnie.transform.position, out posLaPlusProche, 10f, NavMesh.AllAreas))
        {
            //on met a jour sa position pour la position la plus proche disponible
            chefSageCompagnie.transform.position = posLaPlusProche.position;
        }
    }

    //méthode pour instancier le sceptre ancien
    void PlacerSceptre()
    {
        //on va chercher le 2e point d'instance, c'est la qu'on va positionner le sceptre
        var posSceptre = transform.GetChild(1).transform.position;

        //on instancie le sceptre
        var sceptre = Instantiate(prefabSceptreGagner, posSceptre, Quaternion.identity);

        //on change son nom pour qu'il soit le meme que le prefab
        sceptre.gameObject.name = prefabSceptreGagner.gameObject.name;

        //pour contenir la position la plus proche
        NavMeshHit posLaPlusProche;

        //si il y a quelque chose à la position qu'on veut instancier le sceptre , alors on va le positionner à un endroit proche
        if (NavMesh.SamplePosition(sceptre.transform.position, out posLaPlusProche, 10f, NavMesh.AllAreas))
        {
            //on met a jour sa position pour la position la plus proche disponible
            sceptre.transform.position = posLaPlusProche.position;
        }
    }



}
