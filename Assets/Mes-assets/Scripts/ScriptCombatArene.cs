using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ScriptCombatArene : MonoBehaviour
{

    public GameObject refCombatantsArene;

    public GameObject refPotionsArene;

    public GameObject prefabCleSortirArene;

    public GameObject prefabSceptreGagner;

    public GameObject prefabChefSageCompagnie;


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "joueur")
        {
            refCombatantsArene.GetComponent<InstancierAuxPoints>().enabled = true;
            refPotionsArene.GetComponent<InstancierAuxPoints>().enabled = true;
        }
    }

    private void Update()
    {

        if (refCombatantsArene.GetComponent<InstancierAuxPoints>().nombreObjetsInstanciesTotal >= 15)
        {

            refCombatantsArene.GetComponent<InstancierAuxPoints>().enabled = false;
            refPotionsArene.GetComponent<InstancierAuxPoints>().enabled = false;

            var sceptre = Instantiate(prefabSceptreGagner, transform.position, Quaternion.identity);

            var chefSageCompagnie = Instantiate(prefabChefSageCompagnie, transform.position, Quaternion.identity);

            NavMeshHit closestHit;

            if (NavMesh.SamplePosition(chefSageCompagnie.transform.position, out closestHit, 10f, NavMesh.AllAreas))
            {
                chefSageCompagnie.transform.position = closestHit.position;
            }

        }
    }



}
