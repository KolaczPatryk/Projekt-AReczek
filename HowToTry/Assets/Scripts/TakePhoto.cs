using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class TakeARPhoto : MonoBehaviour
{
    [SerializeField] private ARCameraManager arCameraManager;
    [SerializeField] private Camera arCamera;
    [SerializeField] private Button captureButton;
    [SerializeField] private GameObject canvas;
    [SerializeField] private string cameraDirectory;

    

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        if (arCameraManager == null)
        {
            Debug.LogError("ERROR: ARCameraManager nie zosta³ przypisany!");
            return;
        }
        if (captureButton != null)
        {
            captureButton.onClick.AddListener(CapturePhoto);
        }
        else
        {
            Debug.LogError("ERROR: Przycisk nie zosta³ przypisany!");
        }
    }

    void CapturePhoto()
    {
        StartCoroutine(TakeScreenshot());
    }

    IEnumerator TakeScreenshot()
    {
        yield return new WaitForEndOfFrame();
        canvas.SetActive(false);
        Texture2D texture = CaptureCameraImage();
        Debug.Log("INFO: Zdjêcie zrobione!");
        SaveScreenshot(texture);
    }
    

    void SaveScreenshot(Texture2D texture)
    {
        // Œcie¿ka do folderu kamery
        string cameraDirectory = "/storage/emulated/0/DCIM/Camera/";
        string fileName = "AR_Screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        // Œcie¿ka docelowa dla obrazka w katalogu kamery
        string destinationFilePath = Path.Combine(cameraDirectory, fileName + ".png");

        // Zapisz obrazek jako PNG na urz¹dzeniu
        byte[] imageBytes = texture.EncodeToPNG();

        // Zapisz plik do katalogu kamery
        File.WriteAllBytes(destinationFilePath, imageBytes);

        // Odœwie¿enie galerii
        AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass classUri = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject objIntent = new AndroidJavaObject("android.content.Intent", "android.intent.action.MEDIA_SCANNER_SCAN_FILE");
        AndroidJavaObject objFile = new AndroidJavaObject("java.io.File", destinationFilePath);
        AndroidJavaObject objUri = classUri.CallStatic<AndroidJavaObject>("fromFile", objFile);
        objIntent.Call<AndroidJavaObject>("setData", objUri);
        objActivity.Call("sendBroadcast", objIntent);
    }

    Texture2D CaptureCameraImage()
    {
        
        Texture2D texture = new Texture2D(Screen.width, Screen.height);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        // Resetuj stan RenderTexture
        canvas.SetActive(true);
        RenderTexture.active = null;

        return texture;
    }
}