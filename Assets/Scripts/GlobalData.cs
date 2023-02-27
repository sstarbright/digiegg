using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    Color _backgroundColor = new Color32(255,119,56, 255);
    public Color newBackgroundColor = new Color32(255,119,56, 255);

    public UIDisplay display;

    public void Start() {
        display = gameObject.AddComponent<UIDisplay>() as UIDisplay;
        Application.targetFrameRate = 30;
        Shader.SetGlobalColor("_backgroundColor", _backgroundColor);
    }

    public void Update() {
        if (newBackgroundColor != _backgroundColor) {
            BackgroundColor = newBackgroundColor;
        }
    }

    public Color BackgroundColor {
        get {return _backgroundColor;}
        set {_backgroundColor = value;
            Shader.SetGlobalColor("_backgroundColor", _backgroundColor);}
    }
}
