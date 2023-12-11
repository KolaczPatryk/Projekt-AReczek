using Dummiesman;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;
using Unity.VisualScripting.Dependencies.NCalc;

public class selectedObject : MonoBehaviour
{
    public static selectedObject Instance { get; private set; }
    void OnEnable()
    {
        Instance = this;
    }

    private void Start()
    {
        test("C:/Users/m.skoncej/Downloads/FBX-20231106T145744Z-001/FBX/Koenigsegg.obj");
    }

    public string modelPath;

    GameObject loadedObject;
    public void setModelPath(string path)
    {
        Debug.Log("�cie�ka modelu w selectedObject.cs" + path);
        try
        {
            loadedObject = new OBJLoader().Load(Application.persistentDataPath+path);
            var obj = GameObject.Find(path.Substring(0, path.Length - 4));
            var cube = GameObject.Find("Cube");
            /*
            var renderers = loadedObject.GetComponentsInChildren<Renderer>();
            var bounds = renderers[0].bounds;
            for (var i = 1; i < renderers.Length; ++i)
                bounds.Encapsulate(renderers[i].bounds);
            Vector3 parentSize = cube.GetComponent<BoxCollider>().bounds.size;
            while (bounds.extents.x > parentSize.x   ||
                    bounds.extents.y > parentSize.y ||
                        bounds.extents.z > parentSize.z)
            {
                loadedObject.transform.localScale *= 0.9f;
            }
            */
            
            var x = 4.43f;
            var y = 2.15f;
            var z = 2.00f;
            loadedObject.transform.localScale = new Vector3(loadedObject.transform.localScale.x/x,loadedObject.transform.localScale.y/y,loadedObject.transform.localScale.z/z);
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
            /*Vector3 parentSize = cube.GetComponent<BoxCollider>().bounds.size;
            while (cube.GetComponent<Renderer>().bounds.extents.x > parentSize.x   ||
                    cube.GetComponent<Renderer>().bounds.extents.y > parentSize.y ||
                        cube.GetComponent<Renderer>().bounds.extents.z > parentSize.z)
            {
                cube.transform.localScale *= 0.9f;
            }*/
            var x = 4.43f;
            var y = 2.15f;
            var z = 2.00f;
            loadedObject.transform.localScale = new Vector3(loadedObject.transform.localScale.x/x,loadedObject.transform.localScale.y/y,loadedObject.transform.localScale.z/z);

            //loadedObject.transform.localScale = new Vector3(bounds2.size.x/bounds.size.x, bounds2.size.y/bounds.size.y, bounds2.size.z/bounds.size.z);
            //normalizeasset(loadedObject);

            //loadedObject.transform.position = cube.transform.position;



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
