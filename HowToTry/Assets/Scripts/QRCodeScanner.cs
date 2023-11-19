using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QRCodeScanner : MonoBehaviour
{
    [SerializeField]
    private RawImage rawBackgroud;
    [SerializeField]
    private AspectRatioFitter aspectRatio;
    [SerializeField]
    private TextMeshProUGUI textResult;
    [SerializeField]
    private RectTransform scanZone;
    public string resultLink;


    private bool camAvaible;
    private WebCamTexture cameratexture;
    void Start()
    {
        SetCamera();
    }
    void Update()
    {
        UpdateCamRender();
    }
    private void SetCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if(devices.Length == 0)
        {
            camAvaible = false;
            return;
        }
        for(int i = 0;i<devices.Length; i++)
        {
            if (devices[i].isFrontFacing==false)
            {
                cameratexture=new WebCamTexture(devices[i].name,(int)scanZone.rect.width,(int)scanZone.rect.height);
            }
        }

        cameratexture.Play();
        rawBackgroud.texture = cameratexture;
        camAvaible=true;
    }

    private void Scan()
    {
        try
        {
            IBarcodeReader readerQR = new BarcodeReader();
            Result result = readerQR.Decode(cameratexture.GetPixels32(), cameratexture.width, cameratexture.height);
            if(result !=null)
            {
                textResult.text = result.Text;
                resultLink = result.Text;
                holdURL.linkURL = resultLink;
                SceneManager.LoadScene("LoadedEcho3DObject");
            }
            else
            {
                textResult.text = "Nie uda³o siê za³adowaæ kodu";
            }
        }
        catch
        {
            textResult.text = "Nie uda³o siê za³adowaæ kodu";
        }
    }
    public void OnClickScan()
    {
        Scan();
    }

    private void UpdateCamRender()
    {
        if(camAvaible==false)
        {
            return;
        }
        float ratio = (float)cameratexture.width/(float)cameratexture.height;
        aspectRatio.aspectRatio= ratio;

        int orientation = -cameratexture.videoRotationAngle;
        rawBackgroud.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);   
    }
}
