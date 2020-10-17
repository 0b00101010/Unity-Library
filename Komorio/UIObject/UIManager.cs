using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : DontDestroySingletonObject<UIManager>
{
    public void OpenUI<T>(params object[] args) where T : UIBase {
        SingletonObject<T>.Instance.OpenUI();
    }
    public void CloseUI<T>(params object[] args) where T : UIBase {
        SingletonObject<T>.Instance.CloseUI();
    }
}
