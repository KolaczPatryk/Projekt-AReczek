using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class loadURL : MonoBehaviour
{

    public Object echo3D;
    // Start is called before the first frame update
    void Start()
    {
        string url = holdURL.linkURL;
        Debug.Log(url + "HERE");
    }
}
