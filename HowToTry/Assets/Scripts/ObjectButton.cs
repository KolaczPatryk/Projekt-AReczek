using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectButton : MonoBehaviour
{
    [SerializeField] private static GameObject model3d;
    [SerializeField] private int modelID;


    public void BtnClicked()
    {
        holdID.id = modelID;
        
        SceneManager.LoadScene("PlaceAndStand");
    }
    

}
