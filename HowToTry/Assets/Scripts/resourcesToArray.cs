using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class resourcesToArray : MonoBehaviour
{
    public List<string> namesOfModels;
    public List<string> allModelsInResources;
    string url = holdURL.linkURL;
    void Start()
    {
        allModelsInResources = ListFBXAndOBJModelsInResources();
        // Wyœwietlenie wszystkich modeli w konsoli
        Debug.Log("Wszystkie modele w folderze Resources:");
        foreach (string model in allModelsInResources)
        {
            Debug.Log(model);
        }
        if(allModelsInResources.Count>0 && url != "")
        {
            InstantiateModel(WhereModel());
        }
    }

    List<string> ListFBXAndOBJModelsInResources()
    {
        List<string> allModels = new List<string>();

        string resourcesPath = Application.dataPath + "/Resources";

        if (Directory.Exists(resourcesPath))
        {
            string[] fbxFiles = Directory.GetFiles(resourcesPath, "*.fbx");
            string[] objFiles = Directory.GetFiles(resourcesPath, "*.obj");

            foreach (string fbxFile in fbxFiles)
            {
                string assetPath = "Assets/Resources/" + Path.GetFileNameWithoutExtension(fbxFile);
                allModels.Add(assetPath);
                namesOfModels.Add(Path.GetFileNameWithoutExtension(fbxFile));
            }

            foreach (string objFile in objFiles)
            {
                string assetPath = "Assets/Resources/" + Path.GetFileNameWithoutExtension(objFile);
                allModels.Add(assetPath);
                namesOfModels.Add(Path.GetFileNameWithoutExtension(objFile));
            }
        }
        else
        {
            Debug.LogError("Folder Resources nie istnieje.");
        }

        return allModels;
    }

    int WhereModel()
    {
        int modelIndex = 0;
        foreach(string name in namesOfModels)   //Odczytanie indexu pod którym powinienn znajdowaæ siê model.
        {
            if(url == name)
            {
                return modelIndex;
            }else
            {
                modelIndex++;
            }
        }
        return modelIndex;
    }


    void InstantiateModel(int index)    //Wczytanie modelu na scenê
    {
        GameObject loadedModel = Resources.Load(allModelsInResources[index]) as GameObject;
        if (loadedModel != null)
        {
            GameObject toSceneModel = Instantiate(loadedModel);
            toSceneModel.transform.position = Vector3.zero;
        }
        else
        {
            Debug.Log("Nie uda³o siê wczytaæ modelu :/");
        }
    }
}
