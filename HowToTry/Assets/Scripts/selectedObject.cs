using Dummiesman;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;

public class selectedObject : MonoBehaviour
{
    public static selectedObject Instance { get; private set; }
    void OnEnable()
    {
        Instance = this;
    }

    private void Start()
    {
        //test("C:/Users/m.skoncej/Downloads/FBX-20231106T145744Z-001/FBX/Koenigsegg.obj");
    }

    public string modelPath;

    GameObject loadedObject;
    public void setModelPath(string path)
    {
        Debug.Log("�cie�ka modelu w selectedObject.cs" + path);
        try
        {
            loadedObject = new OBJLoader().Load(Application.persistentDataPath+path);
            var cube = GameObject.Find("Cube");
            var bounds = GameObject.Find("Bounds");
            /*Vector3 parentSize = cube.GetComponent<BoxCollider>().bounds.size;
            while (cube.GetComponent<Renderer>().bounds.extents.x > parentSize.x   ||
                    cube.GetComponent<Renderer>().bounds.extents.y > parentSize.y ||
                        cube.GetComponent<Renderer>().bounds.extents.z > parentSize.z)
            {
                cube.transform.localScale *= 0.9f;
            }*/
            loadedObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

            loadedObject.transform.position = cube.transform.position;



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

    public void test(string path)
    {
        Debug.Log("�cie�ka modelu w selectedObject.cs" + path);
        try
        {
            loadedObject = new OBJLoader().Load(path);
            var cube = GameObject.Find("Cube");
            var bounds = GameObject.Find("Bounds");
            /*Vector3 parentSize = cube.GetComponent<BoxCollider>().bounds.size;
            while (cube.GetComponent<Renderer>().bounds.extents.x > parentSize.x   ||
                    cube.GetComponent<Renderer>().bounds.extents.y > parentSize.y ||
                        cube.GetComponent<Renderer>().bounds.extents.z > parentSize.z)
            {
                cube.transform.localScale *= 0.9f;
            }*/
            loadedObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            loadedObject.transform.position = cube.transform.position;



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
    public static void normalizeasset(GameObject asset)
    {
        var rend = asset.GetComponent<Renderer>();
        Bounds bound = rend.bounds;
        float ma;
        float sizer = 10;
        if (bound.size.x >= bound.size.y && bound.size.y >= bound.size.z) ma = bound.size.x;
        else if (bound.size.y >= bound.size.z && bound.size.z >= bound.size.x) ma = bound.size.y;
        else ma = bound.size.z;
        ma = ma / sizer;
        asset.transform.localScale = new Vector3(1 / ma, 1 / ma, 1 / ma);
    }
}
