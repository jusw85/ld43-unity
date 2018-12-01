using UnityEngine;
using UnityEditor;
using Tiled2Unity;
using System.Collections.Generic;
using System;

[CustomTiledImporter]
public class TiledImporter : ICustomTiledImporter {

//    private const float SkinWidth = 0.05f;
//
    public void CustomizePrefab(GameObject prefab) {
//        Transform transform = prefab.transform.Find("Spikes/Collision");
//        if (transform != null) {
//            transform.gameObject.AddComponent<SpikesController>();
//            PolygonCollider2D polygonCollider = transform.GetComponent<PolygonCollider2D>();
//            if (polygonCollider != null) {
//                for (int i = 0; i < polygonCollider.pathCount; i++) {
//                    Vector2[] points = polygonCollider.GetPath(i);
//                    if (points.Length == 4) { // assume no rotation
//                        Vector2 max, min;
//                        max = min = points[0];
//                        foreach (Vector2 point in points) {
//                            max = Vector2.Max(point, max);
//                            min = Vector2.Min(point, min);
//                        }
//                        points[0] = new Vector2(min.x + SkinWidth, min.y + SkinWidth);
//                        points[1] = new Vector2(min.x + SkinWidth, max.y - SkinWidth);
//                        points[2] = new Vector2(max.x - SkinWidth, max.y - SkinWidth);
//                        points[3] = new Vector2(max.x - SkinWidth, min.y + SkinWidth);
//                    }
//                    polygonCollider.SetPath(i, points);
//                }
//            }
//        }
    }
//
//    //private Vector2[] expandBoxBounds(float v) {
//    //}
//
    public void HandleCustomProperties(GameObject gameObject, IDictionary<string, string> customProperties) {
//        //printDictionary(customProperties);
//        if (customProperties.ContainsKey("type")) {
//            var spawnType = customProperties["type"];
//            if (spawnType != null) {
//                var spawnedObject = SpawnGeneric(spawnType, gameObject);
//                if (spawnType.Equals("Powerup")) {
//                    var powerup = customProperties["powerup"];
//                    spawnedObject.GetComponent<PowerupController>().powerup = powerup;
//                }
//            }
//        }
    }

    public GameObject SpawnGeneric(string spawnType, GameObject parent) {
        var collider = parent.GetComponentInChildren<Collider2D>();
        if (collider == null) return null;

        var prefabPath = "Assets/Project/Prefabs/" + spawnType + ".prefab";
        var spawn = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));

        GameObject spawnInstance = (GameObject)GameObject.Instantiate(spawn);
        spawnInstance.name = spawn.name;
        spawnInstance.transform.parent = collider.gameObject.transform;

        Vector3 offset = collider.bounds.center - parent.transform.position;
        spawnInstance.transform.localPosition = offset;
        return spawnInstance;
    }

    private void printDictionary(IDictionary<string, string> dictionary) {
        foreach (KeyValuePair<string, string> kvp in dictionary) {
            Debug.Log(kvp.Key + ": " + kvp.Value);
        }
    }

}
