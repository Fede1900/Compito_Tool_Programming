using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LanguageDropDown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;

    [SerializeField] private List<string> _languages;

    private void Awake()
    {
        _languages = new List<string>();

        string[] assetsName = AssetDatabase.FindAssets("t:Language", new[] { "Assets/Script/Language" });
        foreach (string _assetName in assetsName)
        {
            string path = AssetDatabase.GUIDToAssetPath(_assetName);
            string language = Path.GetFileNameWithoutExtension(path);
            _languages.Add(language);
        }

        _dropdown.AddOptions(_languages);


    }

    private void Start()
    {
        _dropdown.value = LanguageManager.Instance.GetSelectedLanguageIndex();
    }

    public void OnDropDownValueChanged(TMP_Dropdown _dropddown)
    {
        LanguageManager.Instance.SelectedLanguage = LanguageManager.Instance.languages[_dropddown.value];
    }
}
