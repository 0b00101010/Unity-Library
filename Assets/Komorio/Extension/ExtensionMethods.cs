using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extension Method is a method that can be used in a particular data type.
/// </summary>
public static class ExtensionMethods
{
    public static void Log(this object value){
        #if UNITY_EDITOR
        Debug.Log(value.ToString());
        #endif
    }
}