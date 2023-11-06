using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;


public class FileManager : MonoBehaviour
{

    private string selectedFilePath;
    public void OpenFile()
    {

        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject intent = currentActivity.Call<AndroidJavaObject>("getIntent");

        AndroidJavaClass intentClass = currentActivity.Call<AndroidJavaClass>("getIntent");
        //AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        intent.Call<AndroidJavaObject>("setType", "application/fbx, application/obj, application/vnd.ms-3d, model/vnd.collada, application/x-3ds, application/x-blender, text/plain, application/pcl");
		intent.Call<AndroidJavaObject> ("setAction", intentClass.GetStatic<string> ("ACTION_GET_CONTENT"));


        //AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

        // Sprawd�, czy u�ytkownik ma uprawnienia do odczytu plik�w z pami�ci urz�dzenia
        bool hasPermission = Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead);

        // Wy�wietl komunikat o b��dzie, je�li u�ytkownik nie ma uprawnie�
        /*if (!hasPermission)
        {
            Debug.Log("U�ytkownik nie ma uprawnie� do odczytu plik�w z pami�ci urz�dzenia.");
            return;
        }*/
            //currentActivity.Call("startActivityForResult", intentObject, 0);
        
    } // Zmienna do przechowywania �cie�ki wybranego pliku

    // Ta metoda obs�uguje rezultat wyboru pliku
    public void onActivityResult(int requestCode, int resultCode, AndroidJavaObject data)
    {
        if (requestCode == 0)
        {
            if (requestCode == 0 && resultCode == -1) // Oznacza, �e u�ytkownik wybra� plik
            {
                AndroidJavaObject uri = data.Call<AndroidJavaObject>("getData");
                selectedFilePath = uri.Call<string>("getPath()");
                Debug.Log("Wybrany plik: " + selectedFilePath);

                // Przejd� do sceny z kamer� i modelem
                selectedObject.modelPath = selectedFilePath;
                SceneManager.LoadScene("BlankAR");
            }
            else
            {
                Debug.Log("Anulowano wyb�r pliku.");
            }
        }
    }
}
