using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ARScreenshot : MonoBehaviour
{
    private Camera camera;
    private int width;
    private int height;
    private RenderTexture rt;
    // Start is called before the first frame update
    public void TakePhoto()
    {
        StartCoroutine(TakePhotoCorutine());
    }

    IEnumerator TakePhotoCorutine()
    {
        yield return new WaitForEndOfFrame();
        Camera cam = Camera.main;
        width = Screen.width;
        height = Screen.height;
        rt = new RenderTexture(width, height, 24);
        camera.targetTexture = rt;

        var currentRT = RenderTexture.active;
        RenderTexture.active = rt;
        camera.Render();

        Texture2D image = new Texture2D(width, height);
        image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        image.Apply();

        camera.targetTexture = null;

        RenderTexture.active = currentRT;

        byte[] bytes = image.EncodeToPNG();
        string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "ARCheck.png";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log(filePath);
        File.WriteAllBytes(filePath, bytes);
        Destroy(rt);
        Destroy(image);

    }
    

}
