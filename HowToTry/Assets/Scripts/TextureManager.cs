using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureManager : MonoBehaviour
{
    //Skrypt podepnij do obiektu TextureManager, do sceny dodaj przyciski i wywo?uj nimi NextTexture/PreviousTexture
    //Do listy textures dodaj proponowane tekstury
    [SerializeField] List<Texture> textures = new List<Texture>();
    //[SerializeField] List<string> names = new List<string>();
    [SerializeField] Material defaultMaterial;


    [SerializeField] private GameObject model;
    private Material modelMaterial;
    private int currentTextureID;


    private void Start()
    {
        model = GameObject.FindGameObjectWithTag("Model");
        if (model == null) Debug.Log("ERROR: GameObject with Tag 'Model' is not foud");
        if (model.GetComponent<Renderer>() == null)
        {
            Debug.Log("INFO: Renderer added to model");
            model.AddComponent<Renderer>();
        }
        if (model.GetComponent<Renderer>().material != null)
        {
            modelMaterial = model.GetComponent<Renderer>().material;
            Debug.Log("INFO: Material component found successfully");
        }
        else
        {
            model.GetComponent<Renderer>().material = defaultMaterial;
            Debug.Log("INFO: Material component added to Model");
        }
        currentTextureID = 0;
        TextureIDChange(0);
    }

    public void NextTexture()
    {
        Debug.Log("INFO: NextTexture button clicked.");
        TextureIDChange(1);
    }

    public void PreviousTexture()
    {
        Debug.Log("INFO: PreviousTexture button clicked.");
        TextureIDChange(-1);
    }

    private void TextureIDChange(int i)
    {
        //ModelIDChange(1) -> w g?r?
        //ModelIDChange(-1) -> w d?
        //Sprawdzenie czy id nie wychodzi poza zakres listy
        currentTextureID += i;
        if (currentTextureID != 0 && currentTextureID != textures.Count)
        {
            modelMaterial.SetTexture("_MainTex", textures[currentTextureID]);
            Debug.Log("INFO: Texture is changed to ID" + currentTextureID);

        }
        else
        {
            currentTextureID -= i;
            Debug.Log("INFO: currentTextureID = " + currentTextureID);
        }


    }
}