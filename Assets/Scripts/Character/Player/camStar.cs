using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camStar : MonoBehaviour {

    public Texture2D texture;
    void OnGUI()
    {
        Rect rect =new Rect(Screen.width/2,
            Screen.height/2,
        texture.width,texture.height);
        GUI.DrawTexture(rect, texture);
    }
}
