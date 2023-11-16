using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuLanguage : MonoBehaviour
{
    [Header("New Game")]
    [SerializeField] private TextMeshProUGUI newGameText;
    [SerializeField] private string newGameID;

    [Header("Continue")]
    [SerializeField] private TextMeshProUGUI continueGameText;
    [SerializeField] private string continueID;

    [Header("Quit")]
    [SerializeField] private TextMeshProUGUI quitGameText;
    [SerializeField] private string quitID;

    private void Awake()
    {
        LanguageManager.Instance.OnLanguageChanged += SetTranslation;
    }

    private void SetTranslation(Language language)
    {
        List<Language.LanguageID> IDs = language.GetLanguagesListIDs();
        ClearTlanslation();

        foreach (Language.LanguageID id in IDs)
        {
            if (id.ID == newGameID)
            {
                newGameText.text = id.Traduzione;
            }

            if (id.ID == continueID)
            {
                continueGameText.text = id.Traduzione;
            }

            if (id.ID == quitID)
            {
                quitGameText.text = id.Traduzione;
            }


        }




    }

    private string DeafultTranslation(string id)
    {
        return $"??{id}??";
    }
   

    //setto le traduzione ad un valore default
    private void ClearTlanslation()
    {
        newGameText.text = DeafultTranslation(newGameID);
        continueGameText.text = DeafultTranslation(continueID);
        quitGameText.text = DeafultTranslation(quitID);
    }
}
