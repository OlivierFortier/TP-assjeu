using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gère la manipulation du mouvement des caméras et l'activation des différentes caméras
/// </summary>
public class GestionCameras : MonoBehaviour
{
    [Header("Référence au transform du parent des caméras")]
    public GameObject refTransformCamera;

    [Header("Configuration des caméras")]
    //les caméras
    public GameObject[] lesCameras;

    //la caméra actuelle -> index du tableau des caméras
    public int cameraActuelle = 0;

    private void Update() {

        //TODO : raycast entre la caméra et le joueur
        
        //cycler à travers les caméras en appuyant sur "V"
        if(Input.GetKeyDown(KeyCode.V)) {

            //augmenter l'index de la caméra actuelle ou le remettre au début si ca dépasse le max
            cameraActuelle++;
            if(cameraActuelle+1>lesCameras.Length) {
                cameraActuelle = 0;
            }

            //faire changer la caméra
            ChangerCamera(cameraActuelle);

        }

        //initialiser les controles de la caméra
        var controlesCamHor = Input.GetAxis("Horizontal");
        var controlesCamVer = Input.GetAxis("Vertical");

        //tourner la caméra à droite si on appuie sur "D" ou flèche de droite
        if(controlesCamHor > 0) {
            refTransformCamera.transform.Rotate(new Vector3(0, 1.0f, 0));
            
        }
        //tourner la caméra à gauche si on appuie sur "D" ou flèche de gauche
        else if(controlesCamHor < 0) {
            refTransformCamera.transform.Rotate(new Vector3(0, -1.0f, 0));
            
        }


    }

    //méthode pour cycler à travers les caméras selon l'index de la caméra actuelle
    public void ChangerCamera(int numCam) {
        //désactivé la caméra actuelle
        Camera.main.gameObject.SetActive(false);
        //activer la prochaine caméra
        lesCameras[numCam].SetActive(true);
    }
}
