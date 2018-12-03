using System.IO;
using UnityEngine;

[ExecuteInEditMode]
class SceneNameSingleton : MonoBehaviour
{
    private FileSystemWatcher m_SceneFileWatcher; // This watches to see if we have saved the level

    private static SceneNameSingleton instance;

    static GameObject genericSingletonGO;
    public static string SINGLETON_NAME = "Singleton";

    public static SceneNameSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                if (genericSingletonGO == null)
                {
                    genericSingletonGO = GameObject.Find(SINGLETON_NAME);
                    if (genericSingletonGO == null)
                        genericSingletonGO = new GameObject(SINGLETON_NAME);
                }

                if (genericSingletonGO != null && genericSingletonGO.GetComponent<SceneNameSingleton>() != null)
                {
                    instance = genericSingletonGO.GetComponent<SceneNameSingleton>();
                }
                else
                {
                    instance = genericSingletonGO.AddComponent<SceneNameSingleton>();
                }
            }

            return instance;
        }
    }

    void OnSceneFileWatcher_Created(object sender, FileSystemEventArgs e)
    {
        if (e.FullPath.Contains(".meta")) return;
        if (!e.FullPath.Contains(".unity")) return;

        Debug.Log("OnSceneFileWatcher_Changed");
        string sceneName = Path.GetFileNameWithoutExtension(e.FullPath);
        Debug.Log("File: " + sceneName + " " + e.ChangeType);
    }

    void OnEnable()
    {
        Debug.Log("OnEnable");
        m_SceneFileWatcher = new FileSystemWatcher(Path.GetFullPath("Assets"), "*.unity");
        m_SceneFileWatcher.Filter = "*.*";
        m_SceneFileWatcher.NotifyFilter = NotifyFilters.LastWrite;
        m_SceneFileWatcher.EnableRaisingEvents = true;

        m_SceneFileWatcher.Created += new FileSystemEventHandler(OnSceneFileWatcher_Created);
        m_SceneFileWatcher.Changed += new FileSystemEventHandler(OnSceneFileWatcher_Created);
    }
//    using UnityEditor;
//    using UnityEngine;
//
//    [InitializeOnLoad]
//    class MyClass
//    {
//        static MyClass ()
//        {
//            Debug.Log("Up and Running");
//            EditorApplication.update += Update;
//        }
//
//        static void Update ()
//        {
//            Debug.Log("Updating");
//        }
//    }
}