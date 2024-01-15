using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class QRGenerator : MonoBehaviour
{
    [SerializeField]
    private RawImage rawImage;
    [SerializeField]
    private TMP_InputField textInput;
    [SerializeField]
    

    private Texture2D encodedText;

    // Start is called before the first frame update
    void Start()
    {
        encodedText = new Texture2D(256, 256);
        
    }

    private Color32[] Encode(string textForEncoding, int width, int height)
    {
        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

    public void OnClickEnabled()
    {
        TextToQR(textInput.text);
    }

    public void DoQRToChoosen()
    {
        QR4ChoosenOne();
    }

    private void TextToQR(string txtInp)
    {
        string textWrite = string.IsNullOrEmpty(txtInp) ? "Brak tekstu w polu" : txtInp;

        Color32[] PixelsToTerxture = Encode(textWrite, encodedText.width, encodedText.height);
        encodedText.SetPixels32(PixelsToTerxture);
        encodedText.Apply();

        rawImage.texture = encodedText;

        // Œcie¿ka do folderu kamery
        string cameraDirectory = "/storage/emulated/0/DCIM/Camera/";

        // Œcie¿ka docelowa dla obrazka w katalogu kamery
        string destinationFilePath = Path.Combine(cameraDirectory, txtInp+".png");

        // Zapisz obrazek jako PNG na urz¹dzeniu
        byte[] imageBytes = encodedText.EncodeToPNG();

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

    private void QR4ChoosenOne()
    {
        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path == null)
            {
                Debug.Log("D00pa");
            }
            else
            {
                FileInfo file = new FileInfo(path);
                if (!file.Exists)
                {
                    file.CopyTo(Application.persistentDataPath);
                }
                Debug.Log("Œcie¿ka modelu: " + path);
                AddToList(path);
                //Debug.Log("Œcie¿ka do aplikacji" + Application.persistentDataPath);
                string newModelName = Path.GetFileNameWithoutExtension(path);
                TextToQR(newModelName);
                Debug.Log("Nazwa modelu: " + newModelName);
            }
        }, new string[] { "*/*" }
        );
    }

    private void AddToList(string modelPath)
    {
        holdURL.newModels.Add(modelPath);
    }

    //Metoda testowa. Wywaliæ j¹ i przycisk
    public void ToTestMyScenes()
    {
        SceneManager.LoadScene("QReader");
    }
    //Metoda testowa
}
