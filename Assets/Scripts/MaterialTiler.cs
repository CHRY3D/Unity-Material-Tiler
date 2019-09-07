using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTiler : MonoBehaviour {
    [Header("")]
    [Header("This utility can overwrite files, use with caution!")]
    [Header("Material Tiling Utility by CHRY")]
    public bool createPrefab = true;
    public Texture Texture;
    public GameObject targetObj;
    public string assetName;
    public int numTilesX;
    public int numTilesY;
}
