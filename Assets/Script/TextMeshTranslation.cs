using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextMeshTranslation : MonoBehaviour
{
    [Header("testo da tradurre")]
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string textID;

    bool found;

    private void Awake()
    {
        
    }

    private void Start()
    {
        LanguageManager.Instance.OnLanguageChanged += SetTranslation;
    }

    private void SetTranslation(Language language)
    {
        found = false;

        List<Language.LanguageID> IDs = language.GetLanguagesListIDs();

        foreach (Language.LanguageID id in IDs)
        {
            if (id.ID == textID)
            {
                found = true;
                text.text = id.Traduzione;
            }


        }

        if (!found)
        {
            text.text = DeafultTranslation(textID);
        }



    }

    private string DeafultTranslation(string id)
    {
        return $"??{id}??";
    }

}
