using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

using Klak.Ndi;

public class UI : MonoBehaviour
{
    [SerializeField] NdiReceiver receiver;
    string settingFilePath => Path.Combine(Application.streamingAssetsPath, "setting.json");

    private void OnEnable()
    {
        Setting setting = new Setting();
        if (File.Exists(settingFilePath))
        {
            var json = File.ReadAllText(settingFilePath);
            setting = JsonUtility.FromJson<Setting>(json);
            receiver.ndiName = setting.ndiName;
        }
        else
            SaveSetting();

        var doc = GetComponent<UIDocument>();
        var root = doc.rootVisualElement;
        var dropdown = root.Q<DropdownField>();

        dropdown.choices = NdiFinder.sourceNames.ToList();
        dropdown.RegisterCallback<FocusEvent>(evt => dropdown.choices = NdiFinder.sourceNames.ToList());
        dropdown.RegisterValueChangedCallback(evt =>
        {
            setting.ndiName = receiver.ndiName = evt.newValue;
            SaveSetting();
        });
        dropdown.value = setting.ndiName;

        void SaveSetting()
        {
            var json = JsonUtility.ToJson(setting);
            File.WriteAllText(settingFilePath, json);
        }
    }

    public struct Setting
    {
        public string ndiName;
    }
}
