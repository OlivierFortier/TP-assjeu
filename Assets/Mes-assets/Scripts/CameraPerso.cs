using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Auteur : Olivier Fortier
classe pour gérer la caméra du personnage
*/
public class CameraPerso : MonoBehaviour
{
    [Header("position de la caméra désirée")]
    //position désirée de la caméra qu'on initialise dans l'éditeur unity
    public Vector3 positionCam;

    [Header("Référence au personnage")]
    //référence au personnage pour obtenir sa position et le regarder
    public GameObject refPersonnage;


    //la position de la caméra sera intialisé ici, mais sera mise a jour par la suite en suivant
    //un gameobject qui est à la position du joueur dans le script GestionCamera
    private void Start() {
        
        //mettre la position de la caméra a la position du personnage + la position désirée de l'inspecteur
        transform.position = (refPersonnage.transform.position + positionCam);
        //regarder le personnage
        transform.LookAt(refPersonnage.transform);

    }

}
