using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIEvents : MonoBehaviour
{
    [SerializeField] RenderTexture cameraTex;
    [SerializeField] RenderTexture ndiTexture;

    private void OnEnable()
    {
        var doc = GetComponent<UIDocument>();
        var root = doc.rootVisualElement;

        var viewport = root.Q("ViewPort");
        var toggle = root.Q<Toggle>();

        toggle.RegisterValueChangedCallback(evt => SetBackground(evt.newValue));
        SetBackground(toggle.value);

        void SetBackground(bool val)=>
            viewport.style.backgroundImage = Background.FromRenderTexture(val ? ndiTexture : cameraTex);
    }
}
