using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : SingletonObject<UIBase>
{
    public virtual void OpenUI(params object[] args) { }
    public virtual void CloseUI(params object[] args) { }
}
