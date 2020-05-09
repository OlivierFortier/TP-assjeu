using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ajuste la taille du fond du texte aux dimensions du texte
/// </summary>
public class AjusterTailleAvertissement : MonoBehaviour
{

    [Header("Référence au texte pour obtenir sa taille")]
    public GameObject refTexte;

    //pour obtenir la composante recTransform de l'image
    private RectTransform rectImage;

    //pour obtenir une référence au RecTransform de l'objet de texte
    private RectTransform rectTexte;

    public void Start()
    {

        //forcer la mise a jour du canvas sinon ca ne fonctionne pas
        Canvas.ForceUpdateCanvases();

        //obtenir le composant recctransform de l'image
        rectImage = GetComponent<RectTransform>();

        //obtenir le composant rectransform du texte
        rectTexte = refTexte.GetComponent<RectTransform>();

        //mettre à jour la taille de l'image à la taille du texte
        rectImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTexte.rect.size.x);

        rectImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectTexte.rect.size.y);


    }

}
