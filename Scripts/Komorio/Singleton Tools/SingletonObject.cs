using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Convert this class to Singleton format.
/// </summary>
public class SingletonObject<T> : MonoBehaviour where T : MonoBehaviour {
    private static T instance;
    public static T Instance {
        get {
            if (instance == null) {
                var obj = GameObject.FindObjectOfType<T>(true);

                if (obj == null) {
                    var prefabs = Resources.Load<T>($"Prefabs/{typeof(T).ToString()}");
                    
                    if (prefabs != null) {
                        obj = prefabs;
                        Instantiate(prefabs.gameObject, Vector2.zero, Quaternion.identity);
                    }
                    #if UNITY_EDITOR
                    else {
                        obj = new GameObject(typeof(T).ToString()).AddComponent<T>();

                        var directoryPath = $"{Application.dataPath}/Resources/Prefabs";
                        
                        var prefabsFolder = new DirectoryInfo(directoryPath);
                        if (prefabsFolder.Exists == false) {
                            prefabsFolder.Create();
                        }

                        var prefabPath = $"{Application.dataPath}/Resources/Prefabs/{typeof(T).ToString()}.prefab";
                        prefabPath = AssetDatabase.GenerateUniqueAssetPath(prefabPath);
                        
                        PrefabUtility.SaveAsPrefabAssetAndConnect(obj.gameObject, prefabPath, InteractionMode.UserAction);
                    }
                    #endif
                }

                SceneManager.activeSceneChanged += (beforeScene, afterScene) => {
                    if (beforeScene != afterScene && afterScene.name != "DontDestroyOnLoad") {
                        instance = null;
                    }
                };
                
                instance = obj;
            }

            return instance;
        }
    }
}