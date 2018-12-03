using UnityEditor;
using UnityEngine;

public class FileModificationWarning : UnityEditor.AssetModificationProcessor
{
//    static string[] OnWillSaveAssets(string[] paths)
//    {
//        Debug.Log("OnWillSaveAssets");
//        foreach (string path in paths)
//        {
//            if (path.EndsWith(".prefab"))
//            {
//                var transform = AssetDatabase.LoadAssetAtPath<GameObject>(path).transform;
//                transform.position = Vector3.zero;
//            }
//        }
//
//        return paths;
//    }
}