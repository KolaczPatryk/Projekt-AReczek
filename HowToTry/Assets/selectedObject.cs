using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectedObject : MonoBehaviour
{
    static public string modelPath;
    public GameObject pattern;

    private void Start()
    {
        Debug.Log(modelPath);
        GameObject model = Resources.Load<GameObject>(modelPath);
        if (model != null)
        {
            Transform transform = pattern.GetComponent<Transform>();
            Instantiate(model, transform.position, transform.rotation);
        }
        Destroy(pattern);
    }
}
