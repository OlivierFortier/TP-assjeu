using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script pour mettre à jour la position de l'objet vers lequel la caméra regarde à la position du joueur.
/// Fonctionne de pair avec GestionCameras.cs & CameraPerso.cs
/// </summary>
public class PointeurCam : MonoBehaviour
{
    
    void Update()
    {
        //mettre à jour a position de la caméra à la position du joueur
        transform.position = GameObject.Find("joueur").transform.position;
        
    }
}
