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

        // SprawdŸ, czy u¿ytkownik ma uprawnienia do odczytu plików z pamiêci urz¹dzenia
        bool hasPermission = Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead);

        // Wyœwietl komunikat o b³êdzie, jeœli u¿ytkownik nie ma uprawnieñ
        /*if (!hasPermission)
        {
            Debug.Log("U¿ytkownik nie ma uprawnieñ do odczytu plików z pamiêci urz¹dzenia.");
            return;
        }*/
            //currentActivity.Call("startActivityForResult", intentObject, 0);
        
    } // Zmienna do przechowywania œcie¿ki wybranego pliku

    // Ta metoda obs³uguje rezultat wyboru pliku
    public void onActivityResult(int requestCode, int resultCode, AndroidJavaObject data)
    {
        if (requestCode == 0)
        {
            if (requestCode == 0 && resultCode == -1) // Oznacza, ¿e u¿ytkownik wybra³ plik
            {
                AndroidJavaObject uri = data.Call<AndroidJavaObject>("getData");
                selectedFilePath = uri.Call<string>("getPath()");
                Debug.Log("Wybrany plik: " + selectedFilePath);

                // PrzejdŸ do sceny z kamer¹ i modelem
                selectedObject.modelPath = selectedFilePath;
                SceneManager.LoadScene("BlankAR");
            }
            else
            {
                Debug.Log("Anulowano wybór pliku.");
            }
        }
    }
}
