using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ScriptCombatArene : MonoBehaviour
{

    public GameObject refCombatantsArene;

    public GameObject refPotionsArene;

    public GameObject prefabSceptreGagner;

    public GameObject prefabChefSageCompagnie;

    public GameObject porteArene;


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

            porteArene.transform.Rotate(new Vector3(0, 180f, 0), Space.Self);

            refCombatantsArene.GetComponent<InstancierAuxPoints>().enabled = false;
            refPotionsArene.GetComponent<InstancierAuxPoints>().enabled = false;

            PlacerChefCompagnie();

            PlacerSceptre();


        }
    }

    void PlacerChefCompagnie()
    {

        var posChef = transform.GetChild(0).transform.position;

        var chefSageCompagnie = Instantiate(prefabChefSageCompagnie, posChef, Quaternion.identity);

        NavMeshHit closestHit;

        if (NavMesh.SamplePosition(chefSageCompagnie.transform.position, out closestHit, 10f, NavMesh.AllAreas))
        {
            chefSageCompagnie.transform.position = closestHit.position;
        }
    }

    void PlacerSceptre()
    {

        var posSceptre = transform.GetChild(1).transform.position;

        var sceptre = Instantiate(prefabSceptreGagner, posSceptre, Quaternion.identity);

        NavMeshHit closestHit;

        if (NavMesh.SamplePosition(sceptre.transform.position, out closestHit, 10f, NavMesh.AllAreas))
        {
            sceptre.transform.position = closestHit.position;
        }
    }



}
