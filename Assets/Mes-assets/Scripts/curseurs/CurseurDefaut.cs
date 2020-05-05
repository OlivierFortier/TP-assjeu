using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe pour définir les apparences du curseur par défaut
/// 
/// Auteur : Olivier Fortier
/// Date : 2020-04-07
/// </summary>
public class CurseurDefaut : MonoBehaviour
{
    #region propriétés

    [Header("texture pour le curseur")]
    public Texture2D textureCurseur;

    [Header("mode du curseur")]
    public CursorMode modeCurseur = CursorMode.ForceSoftware;

    [Header("position du curseur")]
    public Vector2 hotSpot = Vector2.zero;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(textureCurseur, hotSpot, modeCurseur);
    }

}
