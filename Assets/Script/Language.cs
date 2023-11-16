using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Language/Language")]

public class Language : ScriptableObject
{
    //[SerializeField] private List<string> _IDs = new List<string> { "NEW_GAME", "LOAD_GAME", "QUIT_GAME" };

    //public List<string> IDS
    //{
    //    get { return _IDs; }       
    //}

    [Serializable]
    public class LanguageID
    {
        [SerializeField] private string _ID = "";
        [SerializeField] private string _traduzione;

        public LanguageID(string id, string traduzione)
        {
            _ID = id;
            _traduzione = traduzione;
        }

        public bool openInEditor;

        public string ID
        {
            get => _ID;
            set => _ID = value;
        }

        public string Traduzione
        {
            get => _traduzione;
            set => _traduzione = value;
        }
    }

    [SerializeField] private List<LanguageID> _languagesIDs = new List<LanguageID>();

    public List<LanguageID> GetLanguagesListIDs() => _languagesIDs;

    public int GetLanguageIDsCount() => _languagesIDs.Count;

    public LanguageID GetLanguageID(int index)=> _languagesIDs[index];

    public void RemoveLanguageID(int index) => _languagesIDs.RemoveAt(index);

    public void AddLanguageID() => _languagesIDs.Add(new LanguageID("",""));
    public void AddLanguageID(string id, string traduzione)
    {
        _languagesIDs.Add(new LanguageID(id, traduzione));
    }

    public void SortLanguageIDs() => _languagesIDs.Sort((x, y) => x.ID.CompareTo(y.ID));

    

}
