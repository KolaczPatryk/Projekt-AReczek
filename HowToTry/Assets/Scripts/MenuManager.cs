using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    public void OpenScannerScene()
    {
        Debug.Log("INFO: Loading QR_Scanner scene.");
        SceneManager.LoadScene("QR_Scanner");
    }

    public void OpenMainMenu()
    {
        Debug.Log("INFO: Loading MainMenu scene.");
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenARScene()
    {
        Debug.Log("INFO: Loading AR_Session scene.");
        SceneManager.LoadScene("AR_Session");
    }

    
}
