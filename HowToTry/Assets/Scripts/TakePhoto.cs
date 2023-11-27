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
        // Sprawd�, czy ARCameraManager zosta� przypisany
        if (arCameraManager == null)
        {
            Debug.LogError("ARCameraManager nie zosta� przypisany!");
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
            Debug.LogError("Przycisk nie zosta� przypisany!");
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
        Texture2D texture = arCameraManager.GetTexture();

        // Zapisz zdj�cie na dysku
        SaveScreenshot(texture);

        Debug.Log("Zdj�cie zrobione!");

        // Mo�esz tak�e przetworzy� dalej lub wy�wietli� miniatur�, itp.
    }

    void SaveScreenshot(Texture2D texture)
    {
        // Utw�rz folder, je�li nie istnieje
        System.IO.Directory.CreateDirectory(screenshotFolder);

        // Generuj unikaln� nazw� pliku na podstawie daty i godziny
        string fileName = "AR_Screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

        // Zapisz tekstur� jako plik PNG
        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(System.IO.Path.Combine(screenshotFolder, fileName), bytes);

        Debug.Log("Zapisano zdj�cie w " + screenshotFolder + "/" + fileName);
    }
}