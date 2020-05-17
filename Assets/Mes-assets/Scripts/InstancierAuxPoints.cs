using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Permet d'instancier des objets à des points d'instances, avec un délai aléatoire entre chaque apparition
/// 
/// Author: Olivier Fortier
/// Date: 2020-05-16
/// </summary>
public class InstancierAuxPoints : MonoBehaviour
{
    //liste pour tenir le compte des plans d'instances (enfant du gameobject) qui permettent d'instancier 
    //des objets a ces endroits aléatoirement
    [HideInInspector] public List<GameObject> listePointInstance;

    [Header("Liste des objets à instancier")]
    //liste des prefab d'objets à instancier aléatoirement aux points d'instances
    public List<GameObject> listeObjetsPrefab;

    //liste des objets qui ont étés instanciés
    public List<GameObject> listeObjetsInstances;

    //contiens le nombre d'objets instanciés par ce script au total
    public int nombreObjetsInstanciesTotal = 0;

    //le nombre maximum d'objets permis en même temps
    public int maximumObjetsEnMemeTemps = 5;

    //le nombre maximum d'objets permis au TOTAL dans la durée de vie de ce script
    public int maxObjetsTotal = 15;

    [Header("Configuration du délai min et max entre les instanciations")]
    //délai minimum entre chaque instanciation
    public float delaiMin = 1f;
    //délai maximum entre chaque instanciation
    public float delaiMax = 5f;

    //contiens le temps écoulé entre chaque instanciation
    [HideInInspector] public float tempsEcoule;

    private void Start()
    {
        //pour chaque enfant du gameobject
        foreach (Transform enfant in transform)
        {
            //on ajoute le "point d'instance" à la liste
            listePointInstance.Add(enfant.gameObject);

            //on désactive son renderer pour pas que le joueur voit des "planes"
            enfant.gameObject.GetComponent<Renderer>().enabled = false;
        }

        //on assigne un délai aléatoire
        tempsEcoule = Random.Range(delaiMin, delaiMax);

    }

    private void Update()
    {
        //si le délai n'est pas à 0, appeler la fonction qui fait écouler le temps
        if (tempsEcoule > 0)
            EcoulerTemps();



        //fais instancier un objet si on à pas le max d'objets en meme temps, pas le max d'objets total, et que le délai est écoulé
        if (listeObjetsInstances.Count < maximumObjetsEnMemeTemps && nombreObjetsInstanciesTotal <= maxObjetsTotal && tempsEcoule <= 0)
        {
            //instancier un objet
            ApparaitreObjet();
        }

        //boucle pour chaque objet dans la liste d'objets instanciés
        foreach (var item in listeObjetsInstances)
        {
            //si l'objet instancié devient null (par exemple ennemi est tué, la référence devient null car la gameobject est détruit)
            if (item == null)
                //alors enlever cet item de la liste
                listeObjetsInstances.Remove(item);
        }




    }

    //méthode pour faire écouler le temps du délai
    public void EcoulerTemps()
    {
        //on enleve 1 seconde au délai
        tempsEcoule -= Time.deltaTime;
    }

    //méthode qui gère l'instanciation a un point aléatoire un objet
    public void ApparaitreObjet()
    {
        //obtention d'un endroit aléatoire parmis les "points d'instances" disponibles
        GameObject endroitAleatoire = listePointInstance[Random.Range(0, listePointInstance.Count)];

        //obtention d'un objet prefab aléatoire parmis la liste des prefabs disponibles
        GameObject prefabAleatoire = listeObjetsPrefab[Random.Range(0, listeObjetsPrefab.Count)];

        //instanciation de l'objet choisi aléatoirement
        GameObject instanceObjet = Instantiate(prefabAleatoire, transform);

        //on le place à la position d'un point d'instance
        instanceObjet.transform.position = endroitAleatoire.transform.position;

        //on lui donne la rotation du point d'instance
        instanceObjet.transform.rotation = endroitAleatoire.transform.rotation;

        //si l'objet possède un AI, on lui "reset" son navmeshagent car sinon ca bug et il ne peut pas bouger
        if (instanceObjet.TryGetComponent(out NavMeshAgent agentInstance))
        {
            agentInstance.enabled = false;
            agentInstance.enabled = true;

            //variable pour savoir ou est le point le plus proche disponible
            NavMeshHit pointLePlusProche;

            //on change la position de l'objet si on détecte que la position est déja occupée par un autre AI
            if (NavMesh.SamplePosition(instanceObjet.transform.position, out pointLePlusProche, 10f, NavMesh.AllAreas))
            {
                //changer sa position pour un point proche
                instanceObjet.transform.position = pointLePlusProche.position;
            }
        }

        //on lui donne le nom du prefab pour pas qu'il soit appelé "objet(clone)"
        instanceObjet.gameObject.name = prefabAleatoire.gameObject.name;



        //on ajoute l'objet instancié à la liste d'objets instanciés
        listeObjetsInstances.Add(instanceObjet);

        //on augmente le nombre total d'objets instanciés
        nombreObjetsInstanciesTotal++;

        //on ajoute un nouveau délai aléatoire
        tempsEcoule = Random.Range(delaiMin, delaiMax);

    }

}
