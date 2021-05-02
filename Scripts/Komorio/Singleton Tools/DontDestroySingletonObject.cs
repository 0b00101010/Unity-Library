using System;
using System.Collections;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Convert this class to Singleton format.
/// And keep the object from being destroyed.
/// </summary>
public class DontDestroySingletonObject<T> :MonoBehaviour where T : MonoBehaviour {
    private static T instance;
    public static T Instance {
        get {
            if (instance == null) {
                var obj = GameObject.FindObjectOfType<T>(true);

                if (obj == null) {
                    var prefabs = Resources.Load<T>($"Prefabs/{typeof(T).ToString()}");
                    
                    if (prefabs != null) {
                        obj = Instantiate(prefabs).gameObject.GetComponentSafe<T>();
                    }
                    #if UNITY_EDITOR
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
                    #endif
                }

                instance = obj;
                DontDestroyOnLoad(obj.gameObject);
            }

            return instance;
        }
    }
    
    
}