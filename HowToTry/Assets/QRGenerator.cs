using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;
using TMPro;

public class QRGenerator : MonoBehaviour
{
    [SerializeField]
    private RawImage rawImage;
    [SerializeField]
    private TMP_InputField textInput;

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
        TextToQR();
    }

    private void TextToQR()
    {
        string textWrite = string.IsNullOrEmpty(textInput.text) ? "Brak tekstu w polu" : textInput.text;

        Color32[] PixelsToTerxture = Encode(textWrite, encodedText.width, encodedText.height);
        encodedText.SetPixels32(PixelsToTerxture);
        encodedText.Apply();

        rawImage.texture = encodedText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
