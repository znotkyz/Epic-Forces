using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class ImageManager : MonoBehaviour
{
    public static ImageManager Instance;

    string _basePath;
    //JSONObject _versionJson;

    // TODO:
    // 0. Make a base path
    // 1. Check if Image already exists
    // 2. Save Images
    // 3. Load Images (IO)
    // 4. Try to get Image

    // TODO:
    // 1. Create Version Dictionary (JSON)
    // 2. Add Version in DB
    // 3. Get version in our Item Info (PHP)
    // 4. Save and Compare Version
    // 5. Check version Json and decide if we should download or load from device

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            GameObject.Destroy(this);
            return;
        }
        Instance = this;

        _basePath = Application.persistentDataPath + "/Images/";
        if (!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
        }
    }

    bool ImageExists(string name)
    {
        return File.Exists(_basePath + name);
    }

    public void SaveImage(string name, byte[] bytes)
    {
        File.WriteAllBytes(_basePath + name, bytes);
    }

    public byte[] LoadImage(string name)
    {
        byte[] bytes = new byte[0];
        if (ImageExists(name))
        {
            return File.ReadAllBytes(_basePath + name);
        }
        return bytes;
    }

    public Sprite BytesToSprite(byte[] bytes)
    {
        //Create texture2D
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);

        //Create sprite (to be placed in UI)
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        return sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
