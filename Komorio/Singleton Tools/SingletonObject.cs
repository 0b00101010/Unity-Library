using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
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