using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    [SerializeField]
    Texture2D image;

    Rect rect;

    void OnGui()
    {
        GUI.DrawTexture(rect, image, ScaleMode.StretchToFill);
    }

    void Update()
    {
        rect = new Rect(0, 0, Screen.width, Screen.height);
    }
}
