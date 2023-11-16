using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LanguageEditorWindow : EditorWindow
{
    [MenuItem("Tools/Language Editor")]

    private static void OpenWindow()
    {
        GetWindow<LanguageEditorWindow>();
    }

    private int selectedElement;
    private Vector2 scrollview;
    

    private void OnGUI()
    {
        string[] languageIds = AssetDatabase.FindAssets("t:Language");
        string[] languagespaths = new string[languageIds.Length];
        string[] language = new string[languageIds.Length];

        for (int i = 0; i < languageIds.Length; i++)
        {
            languagespaths[i] = AssetDatabase.GUIDToAssetPath(languageIds[i]);
            language[i] = Path.GetFileNameWithoutExtension(languagespaths[i]);
        }

        if (languageIds.Length == 0)
        {
            EditorGUILayout.HelpBox("Non ci sono lingue salvate nel progetto", MessageType.Warning);
            return;
        }

        if (selectedElement >= languageIds.Length)
        {
            selectedElement = languageIds.Length - 1;
        }

        GUILayout.BeginHorizontal();

        selectedElement = EditorGUILayout.Popup(selectedElement, language);

        Language selectedLanguage = AssetDatabase.LoadAssetAtPath<Language>(languagespaths[selectedElement]);

        if (GUILayout.Button("Aggiungi lingua"))
        {
            CreateNewLanguage();
        }

        GUILayout.EndHorizontal();

        //visualizzazione coppie id-testo

        LanguageInfoViewGUI(selectedLanguage);

        

    }

    private void LanguageInfoViewGUI(Language selectedLanguage)
    {
        scrollview = EditorGUILayout.BeginScrollView(scrollview);

        for (int i = 0; i < selectedLanguage.GetLanguageIDsCount(); i++)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            Language.LanguageID _pair_Id_text = selectedLanguage.GetLanguageID(i);
            string id;
            string richiestraTraduzioneText;

            id = !string.IsNullOrWhiteSpace(_pair_Id_text.ID) ? _pair_Id_text.ID : "[NULL]";

            richiestraTraduzioneText = string.IsNullOrWhiteSpace(_pair_Id_text.Traduzione) ? "[Aggiungere traduzione]" : "";


            _pair_Id_text.openInEditor = EditorGUILayout.Foldout(_pair_Id_text.openInEditor, $"{id}   {richiestraTraduzioneText}");

            if (_pair_Id_text.openInEditor)
            {
                EditorGUI.indentLevel++;

                EditorGUI.BeginChangeCheck();


                _pair_Id_text.ID = EditorGUILayout.TextField("id identificativo", _pair_Id_text.ID);

                _pair_Id_text.Traduzione = EditorGUILayout.TextField("stringa tradotta", _pair_Id_text.Traduzione);

                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(selectedLanguage);
                }

                EditorGUI.indentLevel--;

                if (GUILayout.Button("Elimina stringa identificativa") && EditorUtility.DisplayDialog("Attenzione",$"Vuoi eliminare la stringa identificativa {id} e la sua relativa traduzione?", "Si", "No"))
                {
                    selectedLanguage.RemoveLanguageID(i);
                }
            }
          
            EditorGUILayout.EndVertical();

        }

        //Aggiunta nuova stringa id

        GUILayout.Space(30);

        GUILayout.Label("Altre funzionalità",EditorStyles.boldLabel);

        if (GUILayout.Button("Aggiungi nuovo ID"))
        {
            selectedLanguage.AddLanguageID();
        }

        if(GUILayout.Button("Riordina ID"))
        {
            selectedLanguage.SortLanguageIDs();
        }

        if(GUILayout.Button("Esporta lingua"))
        {
            string filePath = EditorUtility.SaveFilePanel("Esporta","" ,$"{selectedLanguage.name}", "csv");
            if(!string.IsNullOrWhiteSpace(filePath))
            {
                string fileContent = "";

                foreach(Language.LanguageID id in selectedLanguage.GetLanguagesListIDs())
                {
                    fileContent += $"{id.ID},{id.Traduzione}\n";
                }

                File.WriteAllText(filePath, fileContent);
            }
        }

        EditorGUILayout.EndScrollView();
    }

    private void CreateNewLanguage()
    {
        LanguageCreation languageCreation=CreateInstance<LanguageCreation>();
        languageCreation.ShowUtility();
    }
}
