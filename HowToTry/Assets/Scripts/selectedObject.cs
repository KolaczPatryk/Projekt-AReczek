using Dummiesman;
using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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
            loadedObject = new OBJLoader().Load(Application.persistentDataPath + path);
            var obj = GameObject.Find(path.Substring(0, path.Length - 4));
            var cube = GameObject.Find("Cube");

            //cube = loadedObject;
            //PrefabUtility.SaveAsPrefabAssetAndConnect(cube, loadedObject.name);
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
            loadedObject.transform.localScale = new Vector3(loadedObject.transform.localScale.x / x, loadedObject.transform.localScale.y / y, loadedObject.transform.localScale.z / z);
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
            loadedObject.gameObject.tag = "Model";
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
            loadedObject.AddComponent<MeshFilter>();
            loadedObject.AddComponent<MeshRenderer>();
            MeshCombine(loadedObject);
        }catch (Exception ex){
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

    public static MeshFilter MeshCombine(GameObject gameObject, bool destroyObjects = false, params GameObject[] ignore)
    {
        Vector3 originalPosition = gameObject.transform.position;
        Quaternion originalRotation = gameObject.transform.rotation;
        Vector3 originalScale = gameObject.transform.localScale;
        gameObject.transform.position = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.transform.localScale = Vector3.one;

        List<Material> materials = new List<Material>();
        List<List<CombineInstance>> combineInstanceLists = new List<List<CombineInstance>>();
        MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>().Where(m => !ignore.Contains(m.gameObject) && !ignore.Any(i => m.transform.IsChildOf(i.transform))).ToArray();

        foreach (MeshFilter meshFilter in meshFilters)
        {
            MeshRenderer meshRenderer = meshFilter.GetComponent<MeshRenderer>();

            if (!meshRenderer ||
                !meshFilter.sharedMesh ||
                meshRenderer.sharedMaterials.Length != meshFilter.sharedMesh.subMeshCount)
            {
                continue;
            }

            for (int s = 0; s < meshFilter.sharedMesh.subMeshCount; s++)
            {
                int materialArrayIndex = materials.FindIndex(m => m.name == meshRenderer.sharedMaterials[s].name);
                if (materialArrayIndex == -1)
                {
                    materials.Add(meshRenderer.sharedMaterials[s]);
                    materialArrayIndex = materials.Count - 1;
                }
                combineInstanceLists.Add(new List<CombineInstance>());

                CombineInstance combineInstance = new CombineInstance();
                combineInstance.transform = meshRenderer.transform.localToWorldMatrix;
                combineInstance.subMeshIndex = s;
                combineInstance.mesh = meshFilter.sharedMesh;
                combineInstanceLists[materialArrayIndex].Add(combineInstance);
            }
        }

        // Get / Create mesh filter & renderer
        MeshFilter meshFilterCombine = gameObject.GetComponent<MeshFilter>();
        if (meshFilterCombine == null)
        {
            meshFilterCombine = gameObject.AddComponent<MeshFilter>();
        }
        MeshRenderer meshRendererCombine = gameObject.GetComponent<MeshRenderer>();
        if (meshRendererCombine == null)
        {
            meshRendererCombine = gameObject.AddComponent<MeshRenderer>();
        }

        // Combine by material index into per-material meshes
        // Create CombineInstance array for next step
        Mesh[] meshes = new Mesh[materials.Count];
        CombineInstance[] combineInstances = new CombineInstance[materials.Count];

        for (int m = 0; m < materials.Count; m++)
        {
            CombineInstance[] combineInstanceArray = combineInstanceLists[m].ToArray();
            meshes[m] = new Mesh();
            meshes[m].CombineMeshes(combineInstanceArray, true, true);

            combineInstances[m] = new CombineInstance();
            combineInstances[m].mesh = meshes[m];
            combineInstances[m].subMeshIndex = 0;
        }

        // Combine into one
        meshFilterCombine.sharedMesh = new Mesh();
        meshFilterCombine.sharedMesh.CombineMeshes(combineInstances, false, false);

        // Destroy other meshes
        foreach (Mesh oldMesh in meshes)
        {
            oldMesh.Clear();
            DestroyImmediate(oldMesh);
        }

        // Assign materials
        Material[] materialsArray = materials.ToArray();
        meshRendererCombine.materials = materialsArray;

        if (destroyObjects)
        {
            IEnumerable<Transform> toDestroy = meshFilters.Select(m => m.transform);
            List<Transform> toSave = new List<Transform>(8);
            Transform child;
            for (int i = 0; i < meshFilters.Length; i++)
            {
                if (meshFilters[i].gameObject == gameObject)
                {
                    continue;
                }
                //Check if any children should be saved
                for (int c = 0; c < meshFilters[i].transform.childCount; c++)
                {
                    child = meshFilters[i].transform.GetChild(c);
                    if (!toDestroy.Contains(child))
                    {
                        toSave.Add(child);
                    }
                }
                //Move toSave children to root object
                for (int s = 0; s < toSave.Count; s++)
                {
                    toSave[s].parent = gameObject.transform;
                }
                toSave.Clear();

                Destroy(meshFilters[i].gameObject);
            }
        }
        else
        {
            for (int i = 0; i < meshFilters.Length; i++)
            {
                if (meshFilters[i].gameObject == gameObject)
                {
                    continue;
                }
                Destroy(meshFilters[i].GetComponent<MeshRenderer>());
                Destroy(meshFilters[i]);
            }
        }
        gameObject.transform.position = originalPosition;
        gameObject.transform.rotation = originalRotation;
        gameObject.transform.localScale = originalScale;
        return meshFilterCombine;
    }



}
