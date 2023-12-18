using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VerticalObjectControl : MonoBehaviour
{

    public void MoveUp()
    {
        transform.position += new Vector3(0, 0.1f, 0);
        Debug.Log("INFO: Moving object UP");
    }

    public void MoveDown()
    {
        transform.position -= new Vector3(0, 0.1f, 0);
        Debug.Log("INFO: Moving object DOWN");
    }

}
