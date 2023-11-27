using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class capture : MonoBehaviour
{
    [SerializeField] GameObject UI_Panel;

    public void Capture_image()
    {
        UI_Panel.SetActive(false);
        ScreenCapture.CaptureScreenshot("screenek.png");
        UI_Panel.SetActive(true);
    }
    
}
