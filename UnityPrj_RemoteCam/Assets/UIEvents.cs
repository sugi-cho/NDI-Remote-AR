using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

using Klak.Ndi;

public class UIEvents : MonoBehaviour
{
    [SerializeField] RenderTexture cameraTex;
    [SerializeField] RenderTexture ndiTexture;
    [SerializeField] NdiReceiver receiver;

    private void OnEnable()
    {
        var doc = GetComponent<UIDocument>();
        var root = doc.rootVisualElement;

        var viewport = root.Q("ViewPort");
        var dropdown = root.Q<DropdownField>();
        var realtimeButton = root.Q<Button>("Realtime");
        var ndiButton = root.Q<Button>("NDI");

        var ndiNames = NdiFinder.sourceNames.ToList();

        dropdown.RegisterCallback<FocusEvent>(evt => dropdown.choices = NdiFinder.sourceNames.ToList());
        dropdown.RegisterValueChangedCallback(evt => receiver.ndiName = evt.newValue);
        dropdown.choices = ndiNames;
        if(0 < ndiNames.Count)
        {
            dropdown.index = 0;
            receiver.ndiName = ndiNames[0];
        }

        realtimeButton.clicked += () => UseNdi(false);
        ndiButton.clicked += () => UseNdi(true);
        UseNdi(true);

        void UseNdi(bool val)=>
            viewport.style.backgroundImage = Background.FromRenderTexture(val ? ndiTexture : cameraTex);
    }
}
