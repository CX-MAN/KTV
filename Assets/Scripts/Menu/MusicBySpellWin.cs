using UnityEngine;
using System.Collections.Generic;
using System;
public class MusicBySpellWin : MonoBehaviour
{
    public UIInput input;
    public UniversalButton universalButton;
    List<UniversalButton> allButtons = new List<UniversalButton>();
    List<MusicItem> mCaches = new List<MusicItem>();
    public NGUIDragMenu dm;
    public MusicItem musicItem;
    public GameObject allLetters;
    void Start()
    {
        this.gameObject.SetActive(false);
        transform.localPosition = Vector3.zero;
    }
    public void Open()
    {
        musicItem.gameObject.SetActive(false);
        universalButton.gameObject.SetActive(false);
        gameObject.SetActive(true);
        if (allButtons.Count == 0)
        {
            for (int i = (int)'A'; i <= (int)'Z'; i++)
            {
                UniversalButton u = Instantiate(universalButton) as UniversalButton;
                u.transform.parent = universalButton.transform.parent;
                u.transform.localScale = Vector3.one;
                u.gameObject.SetActive(true);
                u.Name.text = ((char)i) + "";
                u.typeId = i;
                allButtons.Add(u);
                u.ce = OnCharacterClick;
            }
            SetPos();
        }
        for(int i = 0;i < allButtons.Count;i ++)
        {
            allButtons[i].selectedFrame.SetActive(false);
        }
    }
    public void SwitchBt()
    {
        bool flag = allLetters.activeInHierarchy;
        allLetters.SetActive(!flag);
    }
    void OnCharacterClick(int id)
    {
        for (int i = 0; i < allButtons.Count; i++)
        {
            allButtons[i].selectedFrame.SetActive(id == allButtons[i].typeId);
        }
        input.value += ((char)id).ToString();
    }
    void SetPos()
    {
        for(int i = 0;i < allButtons.Count;i ++)
        {
            if (i < 13)
            {
                allButtons[i].transform.localPosition = new Vector3(-515 + (i / 7) * 85, 235 - (i % 7) * 53, 0);
            }
            else if (i < 25)
            {
                allButtons[i].transform.localPosition = new Vector3(495 + ((i - 13) / 6) * 85, 235 - ((i - 13) % 6) * 53, 0);
            }
            else
            {
                allButtons[i].transform.localPosition = allButtons[24].transform.localPosition + new Vector3(0, -53, 0);
            }
            
        }
    }
    void IniResults(List<MusicInfo> l)
    {
        for(int i = 0;i < mCaches.Count;i ++)
        {
            Destroy(mCaches[i].gameObject);
        }
        mCaches.Clear();
        for(int i = 0;i < l.Count;i ++)
        {
            MusicItem mi = Instantiate(musicItem) as MusicItem;
            mi.transform.parent = dm.transform;
            mi.transform.localScale = Vector3.one;
            mi.IniData(l[i]);
            mi.mark.SetActive(l[i].spellName == input.value.ToUpper());
            mCaches.Add(mi);
        }
        dm.SetContent<MusicItem>(mCaches.ToArray(), false, true);
    }
    public void DeleteBt()
    {
        string str = input.value;
        if (str.Length == 0)
            return;
        string s = str.Substring(0, str.Length - 1);
        input.value = s;
    }
    public void ClearBt()
    {
        input.value = "";
    }
  
    public void BtSearch()
    {
        //InSearch = true;
        string KeyWords = input.value;
        if (input.value == "")
        {
            return;
        }
        char[] ch = { ' ' };
        string[] KeyArray = KeyWords.Split(ch, StringSplitOptions.RemoveEmptyEntries);
        ZTools.ZLog("L=" + KeyArray.Length);
        for(int i = 0;i < PFVDatas.allMuscs.Count;i ++)
        {
            PFVDatas.allMuscs[i].sortParam = 0;
        }
        for (int i = 0; i < KeyArray.Length; i++)//match by every single keyword.
        {
            for (int j = 0; j < PFVDatas.allMuscs.Count; j++)
            {
                ZTools.ZLog(PFVDatas.allMuscs[j].musicName + ":" + PFVDatas.allMuscs[j].spellName+"  keyword="+KeyArray[i]);
                PFVDatas.allMuscs[j].sortParam += MatchCount(KeyArray[i], PFVDatas.allMuscs[j].spellName);
            }
        }
        List<MusicInfo> t = new List<MusicInfo>();
        for(int i = 0;i <PFVDatas.allMuscs.Count;i ++)
        {
            if(PFVDatas.allMuscs[i].sortParam > 0)
            {
                t.Add(PFVDatas.allMuscs[i]);
            }
        }
        t.Sort();
        IniResults(t);
    }
    
    int MatchCount(string _keyword, string _matchobject)//返回匹配字符个数
    {
        string keyword, matchobject;
        keyword = _keyword.ToLower();
        matchobject = _matchobject.ToLower();
        int k = 0, ret = 0;
        if (keyword == matchobject)
            return keyword.Length;
        ZTools.ZLog(matchobject);
        for (int i = 0; i < matchobject.Length; i++)
        {
            k = 0;
            if (keyword[0] == matchobject[i])
            {
                do
                {
                    k++;
                } while (k < keyword.Length && ((k + i) < matchobject.Length) && keyword[k] == matchobject[i + k]);
                if (k > ret)
                    ret = k;
            }
        }
        return ret;
    }
}
