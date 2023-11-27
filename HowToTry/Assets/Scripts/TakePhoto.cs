using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System.Collections;

public class TakeARPhoto : MonoBehaviour
{
    public ARCameraManager arCameraManager;
    public Button captureButton;
    public string screenshotFolder = "AR_Screenshots";

    void Start()
    {
        // SprawdŸ, czy ARCameraManager zosta³ przypisany
        if (arCameraManager == null)
        {
            Debug.LogError("ARCameraManager nie zosta³ przypisany!");
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
            Debug.LogError("Przycisk nie zosta³ przypisany!");
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
        Texture2D texture = arCameraManager.GetTexture();

        // Zapisz zdjêcie na dysku
        SaveScreenshot(texture);

        Debug.Log("Zdjêcie zrobione!");

        // Mo¿esz tak¿e przetworzyæ dalej lub wyœwietliæ miniaturê, itp.
    }

    void SaveScreenshot(Texture2D texture)
    {
        // Utwórz folder, jeœli nie istnieje
        System.IO.Directory.CreateDirectory(screenshotFolder);

        // Generuj unikaln¹ nazwê pliku na podstawie daty i godziny
        string fileName = "AR_Screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

        // Zapisz teksturê jako plik PNG
        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(System.IO.Path.Combine(screenshotFolder, fileName), bytes);

        Debug.Log("Zapisano zdjêcie w " + screenshotFolder + "/" + fileName);
    }
}