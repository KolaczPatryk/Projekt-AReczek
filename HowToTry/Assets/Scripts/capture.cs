using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class capture : MonoBehaviour
{
    public void Capture_image()
    {
        ScreenCapture.CaptureScreenshot("screenek.png");
    }
    
}
