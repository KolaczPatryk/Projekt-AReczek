using Dummiesman;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectedObject : MonoBehaviour
{
    public static selectedObject Instance { get; private set; }
    void OnEnable()
    {
        Instance = this;
    }

    public string modelPath;

    GameObject loadedObject;
    public void setModelPath(string path)
    {
        Debug.Log("Œcie¿ka modelu w selectedObject.cs" + path);
        try
        {
            loadedObject = new OBJLoader().Load(path);
            var m1 = GameObject.Find("Cube").GetComponent<MeshFilter>();
            var m2 = loadedObject.GetComponent<MeshFilter>();
            m1 = m2;

            Debug.Log("ok");
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        //Debug.Log("Model: " + model);
        //if (model != null)
        //{
        //   Transform transform = gameObject.GetComponent<Transform>();
        //   Instantiate(model, transform.position, transform.rotation);
        //}

        //Destroy(pattern);
    }
}
