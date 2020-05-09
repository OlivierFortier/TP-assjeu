using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personnage : MonoBehaviour
{

    //référence au personnage du joueur
    public GameObject refJoueur;
    
    void Update()
    {


        
        //regarder le joueur
        transform.LookAt(new Vector3(refJoueur.transform.position.x, transform.position.y, refJoueur.transform.position.z));
        
    }
}
