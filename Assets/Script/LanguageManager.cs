using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageManager : MonoBehaviour
{
    [SerializeField] private Language _selectedLanguage;
    public static LanguageManager Instance;

    public List<Language> languages { get; private set; }
    

    public Language SelectedLanguage
    {
        get => _selectedLanguage;

        set
        {
            _selectedLanguage = value;            
            OnLanguageChanged.Invoke(value);
        }
    }



    private void Awake()
    {
        if (Instance == null)
        {           

            Instance = this;

            languages = new List<Language>();

            string[] assetsName = AssetDatabase.FindAssets("t:Language", new[] {"Assets/Script/Language"});
            foreach(string _assetName in assetsName)
            {
                string path = AssetDatabase.GUIDToAssetPath(_assetName);
                Language language=AssetDatabase.LoadAssetAtPath<Language>(path);
                languages.Add(language);
            }

            if(_selectedLanguage == null)
            {
                _selectedLanguage = languages[0];
            }
            

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("L'ho distrutto");
            Destroy(gameObject);
        }


    }

    public delegate void OnLanguageChangedFunction(Language language);
    public event OnLanguageChangedFunction OnLanguageChanged;

    public int GetSelectedLanguageIndex()
    {
        for (int i = 0; i < languages.Count; i++)
        {
            if (languages[i] == _selectedLanguage)
            {
                return i;
            }
        }

        Debug.LogError("Lingua selezionata non trovata");

        return -1;
    }

    public Language GetSelectedLanguage()
    {
        return _selectedLanguage;
    }

    private void Start()
    {
        OnLanguageChanged.Invoke(_selectedLanguage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {           
            SelectedLanguage=languages.SkipWhile(x => x != _selectedLanguage).Skip(1).DefaultIfEmpty(languages[0]).FirstOrDefault();
        }
    }




}
