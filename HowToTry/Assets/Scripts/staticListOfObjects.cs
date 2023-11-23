using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class staticListOfObjects : MonoBehaviour
{
    [SerializeField]
    public List<Object> models;

    void Start()
    {
        string toChoose = holdURL.linkURL;
        Object choosenOBJ = FindModel(toChoose);
        if(choosenOBJ != null)
        {
            InstantiateModel(choosenOBJ);
        }
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
            Debug.Log("No i chuj no i czeœæ");
        }
    }

    /*public Object folder;
    string URL = holdURL.linkURL;
    //Na przysz³oœæ. Mo¿na referowaæ do folderów w Unity, ale nie chce mi siê z tym pierdzieliæ xD
    void Start()
    {
        Debug.Log("KUTAS "+folder);
        ListObjects();
    }

    void ListObjects()
    {
        Debug.Log("KUTAS "+Application.dataPath);
        //string path = folder.;
    }*/
}
