using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class TakeARPhoto : MonoBehaviour
{
    public ARCameraManager arCameraManager;
    public Camera arCamera;
    public Button captureButton;
    public string cameraDirectory;
    //private string cameraDirectory;
    

    void Start()
    {
        //cameraDirectory = Application.persistentDataPath;
        // Sprawd�, czy ARCameraManager zosta� przypisany
        if (arCameraManager == null)
        {
            Debug.LogError("ERROR: ARCameraManager nie zosta� przypisany!");
            return;
        }

        // Sprawd�, czy przycisk zosta� przypisany
        if (captureButton != null)
        {
            // Dodaj funkcj� do przycisku, aby wywo�ywa� metod� CapturePhoto
            captureButton.onClick.AddListener(CapturePhoto);
        }
        else
        {
            Debug.LogError("ERROR: Przycisk nie zosta� przypisany!");
        }
    }

    void CapturePhoto()
    {
        StartCoroutine(TakeScreenshot());
    }

    IEnumerator TakeScreenshot()
    {
        // Poczekaj na klatki kamery, aby mie� aktualny obraz przed zrobieniem zdj�cia
        yield return new WaitForEndOfFrame();

        // Uzyskaj tekstur� z kamery AR
        Texture2D texture = CaptureCameraImage();
        Debug.Log("INFO: Zdj�cie zrobione!");
        // Zapisz zdj�cie na dysku
        SaveScreenshot(texture);

        

        // Mo�esz tak�e przetworzy� dalej lub wy�wietli� miniatur�, itp.
    }
    
    //void SaveScreenshot(Texture2D texture)
    //{
    //    // Generuj unikaln� nazw� pliku na podstawie daty i godziny
    //    string fileName = "AR_Screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

    //    // Zapisz tekstur� jako plik PNG
    //    byte[] bytes = texture.EncodeToPNG();
    //    System.IO.File.WriteAllBytes(System.IO.Path.Combine(Application.persistentDataPath, fileName), bytes);




    //    Debug.Log("INFO: Zapisano zdj�cie w " + cameraDirectory + "/" + fileName);
    //}

    void SaveScreenshot(Texture2D texture)
    {
        // �cie�ka do folderu kamery
        string cameraDirectory = "/storage/emulated/0/DCIM/Camera/";
        string fileName = "AR_Screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        // �cie�ka docelowa dla obrazka w katalogu kamery
        string destinationFilePath = Path.Combine(cameraDirectory, fileName + ".png");

        // Zapisz obrazek jako PNG na urz�dzeniu
        byte[] imageBytes = texture.EncodeToPNG();

        // Zapisz plik do katalogu kamery
        File.WriteAllBytes(destinationFilePath, imageBytes);

        // Od�wie�enie galerii
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
        // Uzyskaj obraz z kamery AR
        //Camera arCamera = arCameraManager.GetComponent<Camera>();

        //if (arCamera == null)
        //{
        //    Debug.LogError("ERROR: Nie mo�na znale�� komponentu Camera w ARCameraManager.");
        //    return null;
        //}

        // Utw�rz now� tekstur� i wczytaj dane pikseli z aktualnej klatki
        //RenderTexture renderTexture = arCamera.targetTexture;
        //RenderTexture renderTexture = arCamera.targetTexture;0
        Texture2D texture = new Texture2D(Screen.width, Screen.height);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        // Resetuj stan RenderTexture
        RenderTexture.active = null;

        return texture;
    }
}