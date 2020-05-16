using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageFin : MonoBehaviour
{
    
    private void Update() {
        
        Text monTexte = GetComponent<Text>();

        monTexte.text = $"Merci d'avoir joué, {ScriptSceneDebut.nomDuJoueur}.";

    }

}
