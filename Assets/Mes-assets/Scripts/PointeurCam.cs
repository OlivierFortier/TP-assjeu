using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointeurCam : MonoBehaviour
{
    
    void Update()
    {
        //mettre à jour a position de la caméra à la position du joueur
        transform.position = GameObject.Find("joueur").transform.position;
        
    }
}
