﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script apelle quand la cinématique de la ville est terminée pour retourner à la caméra normale
/// </summary>
public class ScriptTerminerCinematique : MonoBehaviour
{
    
    //référence au gestionnaire de caméra
    public GameObject gestionnaireCam;

    //méthode pour retourner à la caméra normale apres la cinématique du tour de la ville
    public void RetournerCamNormale() {

        //on se sert de la méthode du gestionnaire de cmaéra pour retourner a la derniere caméra active
        gestionnaireCam.GetComponent<GestionCameras>().ChangerCamera(gestionnaireCam.GetComponent<GestionCameras>().cameraActuelle);

    }
}
