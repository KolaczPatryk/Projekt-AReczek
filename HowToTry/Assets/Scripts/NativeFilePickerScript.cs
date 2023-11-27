using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NativeFilePickerScript : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    selectedObject script = new selectedObject();
    public void Load()
    {
        SceneManager.LoadScene("BlankAR");
        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path == null)
                Debug.Log("Operacja anulowana");
            else
            {   
                int index = path.LastIndexOf('/');
                String temp = path.Substring(index);
                FileInfo file = new FileInfo(path);
                if (!file.Exists)
                {
                    file.CopyTo(Application.persistentDataPath);
                }
                Debug.Log("Persistend data path:"+Application.persistentDataPath);
                script.setModelPath(temp);
            }

        }, new string[] { "*/*" }
        );
    }
}
