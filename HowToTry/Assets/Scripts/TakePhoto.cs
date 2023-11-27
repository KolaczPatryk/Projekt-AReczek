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
        // SprawdŸ, czy ARCameraManager zosta³ przypisany
        if (arCameraManager == null)
        {
            Debug.LogError("ERROR: ARCameraManager nie zosta³ przypisany!");
            return;
        }

        // SprawdŸ, czy przycisk zosta³ przypisany
        if (captureButton != null)
        {
            // Dodaj funkcjê do przycisku, aby wywo³ywa³ metodê CapturePhoto
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
        // Poczekaj na klatki kamery, aby mieæ aktualny obraz przed zrobieniem zdjêcia
        yield return new WaitForEndOfFrame();

        // Uzyskaj teksturê z kamery AR
        Texture2D texture = CaptureCameraImage();

        // Zapisz zdjêcie na dysku
        SaveScreenshot(texture);

        Debug.Log("INFO: Zdjêcie zrobione!");

        // Mo¿esz tak¿e przetworzyæ dalej lub wyœwietliæ miniaturê, itp.
    }
    
    void SaveScreenshot(Texture2D texture)
    {
        // Generuj unikaln¹ nazwê pliku na podstawie daty i godziny
        string fileName = "AR_Screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

        // Zapisz teksturê jako plik PNG
        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(System.IO.Path.Combine(cameraDirectory, fileName), bytes);

        Debug.Log("INFO: Zapisano zdjêcie w " + cameraDirectory + "/" + fileName);
    }

    Texture2D CaptureCameraImage()
    {
        // Uzyskaj obraz z kamery AR
        //Camera arCamera = arCameraManager.GetComponent<Camera>();

        //if (arCamera == null)
        //{
        //    Debug.LogError("ERROR: Nie mo¿na znaleŸæ komponentu Camera w ARCameraManager.");
        //    return null;
        //}

        // Utwórz now¹ teksturê i wczytaj dane pikseli z aktualnej klatki
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