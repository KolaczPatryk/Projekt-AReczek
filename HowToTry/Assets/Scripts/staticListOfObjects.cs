using Dummiesman;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class staticListOfObjects : MonoBehaviour
{
    [SerializeField]
    public List<Object> models;
    private Object choosenOBJ;
    void Start()
    {
        LoadNewModelsToList();
        string toChoose = holdURL.linkURL;
        choosenOBJ = FindModel(toChoose);
        //if (choosenOBJ != null)
        //{
        //    InstantiateModel(choosenOBJ);
        //}
    }
    
   Object FindModel(string choosenOne)
    {
        foreach(Object elem in models)
        {
            if(elem.name == choosenOne)
            {
                return elem;
            }
        }return null;
    }

    void InstantiateModel(Object choosenOBJ)
    {
        if(choosenOBJ!=null)
        {
            GameObject toScene = (GameObject)Instantiate(choosenOBJ);
            toScene.transform.position = Vector3.zero;
        }
        else
        {
            Debug.Log("ERROR: Object does not exist");
        }
    }

    void LoadNewModelsToList()
    {
        foreach(string path in holdURL.newModels)
        {
            GameObject loadingModel;
            loadingModel = new OBJLoader().Load(path);
            loadingModel.name = Path.GetFileNameWithoutExtension(path);
            if(!models.Contains(loadingModel))
            {
                models.Add(loadingModel);
            }
        }
    }

    public Object GetObject()
    {
        return choosenOBJ;
    }

}
