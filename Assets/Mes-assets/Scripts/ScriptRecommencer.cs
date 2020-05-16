using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptRecommencer : MonoBehaviour
{
    
    public void Recommencer() {
        SceneManager.LoadScene("SceneJeu");
    }
}
