using System;
using System.Collections.Generic;

//liste statique qui contiens les tag des objets et personnages pour certaines interactions
[Serializable]
public static class ListeTagObjetsPersonnages {

    public static List<string> liste = new List<string> {
        "cle-or",
        "cle-argent",
        "cle-porte-village",
        "cle-or-rien",
        "cle-porte-yeet",
        "cle-porte-femme",
        "personnage",
        "objet",
        "porte"
    };

}