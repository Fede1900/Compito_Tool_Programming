using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static Unity.VisualScripting.Icons;
using Unity.VisualScripting;
using Codice.Client.Common.GameUI;

[CustomEditor(typeof(TextMeshTranslation))]
public class TextMeshTranslationInspector : Editor
{
    private Language _selectedLanguage;
    private string _translationPreview;
    private SerializedObject _serializedObject;
    private SerializedProperty _textIDProperty;

    private LanguageManager _languageManager;

    private void OnEnable()
    {
        _serializedObject = new SerializedObject(target);
        _textIDProperty = _serializedObject.FindProperty("textID");
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        base.OnInspectorGUI();

        if(EditorGUI.EndChangeCheck())
        {
            _serializedObject.Update();
        }       

        GUILayout.Space(10f);

        TextMeshTranslation component = (TextMeshTranslation)target;

        _languageManager=FindObjectOfType<LanguageManager>();

        _selectedLanguage = _languageManager.GetSelectedLanguage();
        
        if(_selectedLanguage == null)
        {
            EditorGUILayout.HelpBox("Nessuna lingua selezionata", MessageType.Warning);

            return;
        }

        GUILayout.Label("Preview",EditorStyles.boldLabel);

        List<Language.LanguageID> IDs = _selectedLanguage.GetLanguagesListIDs();

        bool found = false;

        foreach (Language.LanguageID id in IDs)
        {
            if (id.ID == _textIDProperty.stringValue)
            {
                found = true;

                _translationPreview = id.Traduzione;

                GUILayout.Label(_translationPreview);
            }
        }

        if (!found)
        {
            _translationPreview = $"??{_textIDProperty.stringValue}??";
            GUILayout.Label(_translationPreview);
            EditorGUILayout.HelpBox("id non trovato", MessageType.Warning);
        }


    }







}
