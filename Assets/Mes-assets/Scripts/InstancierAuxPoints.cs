using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InstancierAuxPoints : MonoBehaviour
{
    [HideInInspector] public List<GameObject> listePointInstance;

    [Header("Liste des objets à instancier")]
    public List<GameObject> listeObjetsPrefab;

    public List<GameObject> listeObjetsInstances;

    public int maximumObjets = 5;

    [Header("Configuration du délai min et max entre les instanciations")]
    public float delaiMin = 1f;
    public float delaiMax = 5f;

    [HideInInspector] public float tempsEcoule;

    private void Start()
    {

        foreach (Transform enfant in transform)
        {
            listePointInstance.Add(enfant.gameObject);

            enfant.gameObject.GetComponent<Renderer>().enabled = false;
        }

        tempsEcoule = Random.Range(delaiMin, delaiMax);

    }

    private void Update()
    {

        EcoulerTemps();


        if (listeObjetsInstances.Count < maximumObjets && tempsEcoule <= 0)
        {
            ApparaitreObjet();
        }




    }

    public void EcoulerTemps()
    {
        tempsEcoule -= Time.deltaTime;
    }

    public void ApparaitreObjet()
    {
        GameObject endroitAleatoire = listePointInstance[Random.Range(0, listePointInstance.Count)];

        GameObject prefabAleatoire = listeObjetsPrefab[Random.Range(0, listeObjetsPrefab.Count)];

        GameObject instanceObjet = Instantiate(prefabAleatoire, transform);

        instanceObjet.transform.position = endroitAleatoire.transform.position;

        instanceObjet.transform.rotation = endroitAleatoire.transform.rotation;

        instanceObjet.GetComponent<NavMeshAgent>().enabled = false;

        instanceObjet.GetComponent<NavMeshAgent>().enabled = true;

        instanceObjet.gameObject.name = prefabAleatoire.gameObject.name;

       NavMeshHit closestHit;

        if (NavMesh.SamplePosition(instanceObjet.transform.position, out closestHit, 10f, NavMesh.AllAreas))
        {
            instanceObjet.transform.position = closestHit.position;
        }
        listeObjetsInstances.Add(instanceObjet);

        tempsEcoule = Random.Range(delaiMin, delaiMax);

    }

}
