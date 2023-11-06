using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;



public class FileManager : MonoBehaviour
{
    public string modelPath;

    //public void LoadModel()
    //{
    //    AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.READ_EXTERNAL_STORAGE");
    //    if (result == AndroidRuntimePermissions.Permission.Granted)
    //    {
    //        // U¿ytkownik udzieli³ permisji.
    //        string path = Application.persistentDataPath;
    //        AndroidRuntimePermissions.RequestPermissions(path);
    //        // ...
    //    }
    //    else
    //    {
    //        // U¿ytkownik nie udzieli³ permisji.
    //    }

    ////string modelType = NativeFilePicker.ConvertExtensionToFileType("*");
    ////AndroidRuntimePermissions.Permission writePerm = AndroidRuntimePermissions.RequestPermission("android.permission.WRITE_EXTERNAL_STORAGE");
    ////AndroidRuntimePermissions.Permission readPerm = AndroidRuntimePermissions.RequestPermission("android.permission.READ_EXTERNAL_STORAGE");

    ////if (writePerm == AndroidRuntimePermissions.Permission.Granted && readPerm == AndroidRuntimePermissions.Permission.Granted)
    ////{
    ////    NativeFilePicker.Permission perm = NativeFilePicker.PickFile((path) =>
    ////    {
    ////        if (path == null)
    ////        {
    ////            //Debug.Log("Anulowano wybór");
    ////        }
    ////        else
    ////        {
    ////            modelPath = path;
    ////            //Debug.Log("Wybrano plik");

    ////        }
    ////    }, new string[] { modelType });

    ////    }
    ////    else
    ////    {
    ////        LoadModel();
    ////    }
    //}

    private string selectedFilePath;
    public void LoadModel()
    {
        //AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.READ_EXTERNAL_STORAGE");
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");



        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_GET_CONTENT"));
        //intentObject.Call<AndroidJavaObject>("setType", "application/fbx, application/obj, application/vnd.ms-3d, model/vnd.collada, application/x-3ds, application/x-blender, text/plain, application/pcl");
        intentObject.Call<AndroidJavaObject>("setType", "*/*");

        // SprawdŸ, czy u¿ytkownik ma uprawnienia do odczytu plików z pamiêci urz¹dzenia
        //bool hasPermission = Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead);

        // Wyœwietl komunikat o b³êdzie, jeœli u¿ytkownik nie ma uprawnieñ
        /*if (!hasPermission)
        {
            Debug.Log("U¿ytkownik nie ma uprawnieñ do odczytu plików z pamiêci urz¹dzenia.");
            return;
        }*/
        currentActivity.Call("startActivityForResult", intentObject, 0);

    } // Zmienna do przechowywania œcie¿ki wybranego pliku

    internal static void WriteAllBytes(object filepath, byte[] bytes)
    {
        throw new NotImplementedException();
    }

    // Ta metoda obs³uguje rezultat wyboru pliku
    public void onActivityResult(int requestCode, int resultCode, AndroidJavaObject data)
    {
        Debug.Log("Test");
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
