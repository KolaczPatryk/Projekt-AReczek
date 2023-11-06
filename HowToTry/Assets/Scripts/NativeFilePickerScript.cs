using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NativeFilePickerScript : MonoBehaviour
{
    public void Load()
    {
        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path == null)
                Debug.Log("Operation cancelled");
            else
            {
                selectedObject.modelPath = path;
                SceneManager.LoadScene("BlankAR");
            }

        }, new string[] { "*/*" }
        );
    }
}
