using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpriteInspector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string path = "Assets/Snapshots/SnapShot_User.png";// AssetDatabase.GetAssetPath(Selection.activeObject);
        //TextureImporter tImporter = AssetImporter.GetAtPath(path) as TextureImporter;
        //tImporter.textureType = TextureImporterType.Sprite;
        //tImporter.textureFormat = TextureImporterFormat.AutomaticCompressed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
