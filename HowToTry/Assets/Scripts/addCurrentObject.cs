using System.IO;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;
using Directory = System.IO.Directory;
using System.Linq;
using UnityEngine.XR.MagicLeap;
using System.ComponentModel;

public class addCurrentObject : MonoBehaviour
{
    public Button classButton;
    public Transform containerTransform;
    public void LoadObject()
    {
        float buttonY = classButton.transform.position.y;
        string fullPath = Application.dataPath + "/Resources";

        if (Directory.Exists(fullPath))
        {
            // Lista plików .prefab i .obj w folderze
            string[] prefabFiles = Directory.GetFiles(fullPath, "*.prefab");
            string[] objFiles = Directory.GetFiles(fullPath, "*.obj");
            string[] fbxFiles = Directory.GetFiles(fullPath, "*.fbx");
            string[] allFiles = prefabFiles.Concat(objFiles).Concat(fbxFiles).ToArray();
            // Wyœwietl znalezione pliki
            //Debug.Log("Prefaby w folderze " + fullPath + ":");
            foreach (string file in allFiles)
            {
                //Wywo³ywanie przycisków przypisanych do modeli
                //Aby zmieniæ wygl¹d tych przycisków nale¿y edytowaæ przycisk "Matkê" -> w projekcie element: ModelButton
                Button newButton = Instantiate(classButton, containerTransform);
                Vector3 buttonPosition = newButton.transform.position;
                buttonPosition.y = buttonY;
                newButton.transform.position = buttonPosition;
                newButton.GetComponentInChildren<Text>().text = Path.GetFileNameWithoutExtension(file);
                newButton.gameObject.SetActive(true);
                newButton.onClick.AddListener(() => SceneWithObject(file));
                buttonY=buttonY - 120;
                //Debug.Log(file);
            }
        }
        else
        {
            //Debug.LogError("Folder o nazwie " + fullPath + " nie istnieje w folderze 'Resources'.");
        }
    }
    void SceneWithObject(string loadedModelPath)
    {
        //Za³aduj scene z modelem
    //   selectedObject.modelPath = loadedModelPath;
        SceneManager.LoadScene("BlankAR");    
    }
}
 
