using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectButton : MonoBehaviour
{
    [SerializeField] GameObject model3d;

    public void BtnClicked()
    {
        Instantiate(model3d);
        
        SceneManager.LoadScene("Patryk2");
    }
    
}
