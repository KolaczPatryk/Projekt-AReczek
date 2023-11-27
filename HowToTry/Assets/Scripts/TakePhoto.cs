using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System.Collections;

public class TakeARPhoto : MonoBehaviour
{
    public ARCameraManager arCameraManager;
    public Camera arCamera;
    public Button captureButton;
    public string cameraDirectory = "/storage/DCIM/Camera";
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

        // Zapisz zdj�cie na dysku
        SaveScreenshot(texture);

        Debug.Log("INFO: Zdj�cie zrobione!");

        // Mo�esz tak�e przetworzy� dalej lub wy�wietli� miniatur�, itp.
    }
    
    void SaveScreenshot(Texture2D texture)
    {
        // Generuj unikaln� nazw� pliku na podstawie daty i godziny
        string fileName = "AR_Screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

        // Zapisz tekstur� jako plik PNG
        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(System.IO.Path.Combine(cameraDirectory, fileName), bytes);

        Debug.Log("INFO: Zapisano zdj�cie w " + cameraDirectory + "/" + fileName);
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
        //RenderTexture renderTexture = arCamera.targetTexture;
        Texture2D texture = new Texture2D(600, 800);
        texture.ReadPixels(new Rect(0, 0, 600, 800), 0, 0);
        texture.Apply();

        // Resetuj stan RenderTexture
        RenderTexture.active = null;

        return texture;
    }
}