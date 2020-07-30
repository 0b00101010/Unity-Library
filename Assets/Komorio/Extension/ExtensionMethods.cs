using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static void Log(object value){
        #if UNITY_EDITOR
        Debug.Log(value.ToString());
        #endif
    }
}