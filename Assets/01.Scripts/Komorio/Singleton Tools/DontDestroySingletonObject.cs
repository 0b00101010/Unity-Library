using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEditor;
/// <summary>
/// Convert this class to Singleton format.
/// And keep the object from being destroyed.
/// </summary>
public class DontDestroySingletonObject<T> :MonoBehaviour where T : MonoBehaviour {
    private static T instance;
    public static T Instance {
        get {
            if (instance == null) {
                var obj = GameObject.FindObjectOfType<T>();

                if (obj == null) {
                    var prefabs = Resources.Load<T>($"Prefabs/{typeof(T).ToString()}");
                    
                    if (prefabs != null) {
                        obj = prefabs;
                        Instantiate(prefabs.gameObject, Vector2.zero, Quaternion.identity);
                    }
                    else {
                        obj = new GameObject(typeof(T).ToString()).AddComponent<T>();

                        var directoryPath = $"{Application.dataPath}/Resources/Prefabs";
                        
                        var prefabsFolder = new DirectoryInfo(directoryPath);
                        if (prefabsFolder.Exists == false) {
                            prefabsFolder.Create();
                        }
                        
                        var prefabPath = $"{directoryPath}/{typeof(T).ToString()}.prefab";
                        prefabPath = AssetDatabase.GenerateUniqueAssetPath(prefabPath);

                        PrefabUtility.SaveAsPrefabAssetAndConnect(obj.gameObject, prefabPath, InteractionMode.UserAction);
                    }
                }

                instance = obj;
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }
    
    
}