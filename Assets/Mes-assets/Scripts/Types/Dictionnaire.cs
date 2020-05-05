using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
//structure qui permet de créer un genre de dictionnaire reconnaissable par unity
// puisque unity ne prends pas en charche les dictionnaires par défaut
public struct Dictionnaire
{
    //clé/nom de l'objet
    public string nom;
    //objet à stocker
    public GameObject objet;
}