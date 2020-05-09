using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
//structure qui permet de créer un genre de dictionnaire reconnaissable par unity
// puisque unity ne prends pas en charche les dictionnaires par défaut
public static class ListeTagObjetsPersonnages {

    public static List<string> liste = new List<string> {
        "cle-or",
        "cle-argent",
        "cle-porte-village",
        "cle-or-rien",
        "cle-porte-yeet",
        "cle-porte-femme",
        "personnage",
        "objet"
    };

}