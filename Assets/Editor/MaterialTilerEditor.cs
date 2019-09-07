using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MaterialTiler))]
public class TTSTilerEditor : Editor {
    private string dirName;
    private float tilingX;
    private float offsetX;
    private float tilingY;
    private float offsetY;

    private int counter;
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        MaterialTiler tilerVars = (MaterialTiler)target;
        if (GUILayout.Button("Build")) {
            createDirectory();
        }

        void createDirectory() {
            if (string.IsNullOrEmpty(tilerVars.assetName)) {
                dirName = tilerVars.Texture.name;
            } else {
                dirName = tilerVars.assetName;
            }
            AssetDatabase.CreateFolder("Assets", dirName);
            AssetDatabase.CreateFolder("Assets/" +dirName, "Materials");
            createMaterialX();
        }

        void createMaterialX() {
            offsetX = 0;
            offsetY = 0;
            for (int i = 0; i < tilerVars.numTilesX * tilerVars.numTilesY; i++) {
                Material material = new Material(Shader.Find("Standard"));
                material.SetTexture("_MainTex", tilerVars.Texture);
                tilingX = 1f / tilerVars.numTilesX;
                tilingY = 1f / tilerVars.numTilesY;
                material.mainTextureScale = new Vector2(tilingX, tilingY);
                material.mainTextureOffset = new Vector2(offsetX, offsetY);

                offsetX = offsetX + tilingX;
                if (offsetX == 1) {
                    offsetY = offsetY + tilingY;
                    offsetX = 0;
                }

                AssetDatabase.CreateAsset(material, "Assets/" +dirName + "/Materials/" +dirName +i +".mat");
                if (tilerVars.createPrefab == true) {
                    createPrefab(material);
                }
            }
        }


        void createPrefab(Material material) {
            GameObject newTile = Instantiate(tilerVars.targetObj, new Vector3(0,0,0), Quaternion.identity);
            newTile.GetComponent<Renderer>().material = material;
            PrefabUtility.SaveAsPrefabAssetAndConnect(newTile, "Assets/" + dirName + "/" + dirName + counter + ".prefab", InteractionMode.UserAction);
            AssetImporter.GetAtPath("Assets/" + dirName + "/" + dirName + counter + ".prefab").SetAssetBundleNameAndVariant(dirName+counter, "");
            counter = counter + 1;
            DestroyImmediate(newTile);
        }
    }
}
