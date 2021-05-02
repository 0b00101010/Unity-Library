using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIBase : SingletonObject<UIBase> {
    private Graphic uiImage;
    
    public virtual void OpenUI(params object[] args) { }
    public virtual void CloseUI(params object[] args) { }
    
    public IEnumerator FadeUI(float value, float duration) {
        // FIXME : 이부분 뭔가 이상함
        var timer = (duration - 1) / 60.0f;
        timer = timer < 0 ? 0 : timer;
        
        var waitForSecond = new WaitForSecondsRealtime(timer);
        
        if (uiImage is null) {
            uiImage = gameObject.GetComponent<Graphic>();
        }

        var initialValue = uiImage.color.a;

        for (int i = 0; i < 60; i++) {
            uiImage.SetAlpha(Mathf.Lerp(initialValue, value, i / 60.0f));
            yield return waitForSecond;
        }

        uiImage.SetAlpha(value);
    }

    public IEnumerator FadeUI(float value, float duration, Action onSuccess) {
        yield return StartCoroutine(FadeUI(value, duration));
        onSuccess?.Invoke();
    }

    public IEnumerator FadeUI(float value, float duration, Action untilSuccess, Action onSuccess) {
        //FIXME : 이부분 뭔가 이상함
        var timer = (duration - 1) / 60.0f;
        timer = timer < 0 ? 0 : timer;
        
        var waitForSecond = new WaitForSecondsRealtime(timer);
        
        if (uiImage is null) {
            uiImage = gameObject.GetComponent<Graphic>();
        }
        
        var initialValue = uiImage.color.a;
        
        for (int i = 0; i < 60; i++) {
            uiImage.SetAlpha(Mathf.Lerp(initialValue, value, i / 60.0f));
            untilSuccess?.Invoke();
            yield return waitForSecond;
        }
        
        uiImage.SetAlpha(value);
        onSuccess?.Invoke();
    }

    public IEnumerator FadeUI(float value, float duration, Action beforeStart, Action untilSuccess, Action onSuccess) {
        beforeStart?.Invoke();
        yield return StartCoroutine(FadeUI(value, duration, untilSuccess, onSuccess));
    }
}
