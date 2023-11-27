using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeChildScript : MonoBehaviour
{
    private GameObject model;

    private void Start()
    {
        //skrypt do robienia dzieci
        model = GameObject.FindGameObjectWithTag("Model");
        model.transform.parent = this.transform;
    }
}
