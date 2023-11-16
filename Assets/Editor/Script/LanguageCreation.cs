using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using NUnit.Framework;

public class LanguageCreation : EditorWindow
{
    private Language _language;   
    private string _languageName;
    

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


        if (_language == null)
        {
            _language = ScriptableObject.CreateInstance<Language>();
        }

        _languageName =EditorGUILayout.TextField("Nome",_languageName);

        if (string.IsNullOrWhiteSpace(_languageName))
        {
            EditorGUILayout.HelpBox("Il nome non può essere vuoto", MessageType.Warning);
        }

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Crea",EditorStyles.miniButtonLeft))
        {
            bool exists = false;

            if (string.IsNullOrWhiteSpace(_languageName))
            {
                return;
            }

            foreach (string name in language)
            {
                if(_languageName ==name)
                {
                    exists = true;
                    break;

                }
                                                  
            }

            if (!exists)
            {
                AssetDatabase.CreateAsset(_language, $"Assets/Script/Language/{_languageName}.asset");
                AssetDatabase.SaveAssets();

                Close();
            }
            else
            {
                EditorUtility.DisplayDialog("Info", "La lingua è già presente nel database", "ok");
            }
           
        }

        if(GUILayout.Button("Annulla",EditorStyles.miniButtonRight))
        {
            Close();
        }

        EditorGUILayout.EndHorizontal();

        if(GUILayout.Button("Importa lingua"))
        {
            string filePath = EditorUtility.OpenFilePanel("Importa", "", "csv");

            string fileName=Path.GetFileNameWithoutExtension(filePath);

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                bool exists = false;

                foreach (string name in language)
                {
                    if (fileName == name)
                    {
                        exists = true;
                        break;

                    }                   
                }

                if (!exists)
                {
                    List<string> id = new List<string>();
                    List<string> traduzione = new List<string>();

                    using (var reader = new StreamReader(filePath))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');

                            id.Add(values[0]);
                            traduzione.Add(values[1]);
                        }
                        

                        
                    }

                    AssetDatabase.CreateAsset(_language, $"Assets/Script/Language/{fileName}.asset");
                    AssetDatabase.SaveAssets();

                    for (int i = 0; i < id.Count; i++)
                    {
                        _language.AddLanguageID(id[i], traduzione[i]);
                    }

                    EditorUtility.SetDirty(_language);

                    Close();

                }
                else
                {
                    EditorUtility.DisplayDialog("Info", "La lingua è già presente nel database", "ok");
                }
            }
        }

        
    }
}
