using UnityEngine;
using System.Collections.Generic;
public class MusicByTypeWin : MonoBehaviour
{
    public NGUIDragMenu dm;
    public MusicItem musicItem;
    List<MusicItem> m_Caches = new List<MusicItem>();
    public UniversalButton[] tabs;
    int m_Type = 0;
    void Start()
    {
        this.gameObject.SetActive(false);
        transform.localPosition = Vector3.zero;
    }
    public void Open()
    {
        musicItem.gameObject.SetActive(false);
        gameObject.SetActive(true);
        for(int i = 0;i < tabs.Length;i ++)
        {
            tabs[i].ce = OnClick;   
            if (i == 0)
            {
                tabs[i].MarkAsSelected();
            }
            else
            {
                tabs[i].MarkAsNormal();
            }
        }
        m_Type = 0;
        IniResults();
    }
    void OnClick(int typeId)
    {
        m_Type = typeId;
        for(int i = 0;i < tabs.Length;i ++)
        {
            if(tabs[i].typeId == typeId)
            {
                tabs[i].MarkAsSelected();
            }
            else
            {
                tabs[i].MarkAsNormal();
            }
        }
        IniResults();
    }
    void IniResults()
    {
        for(int i = 0;i < m_Caches.Count;i ++)
        {
            Destroy(m_Caches[i].gameObject);
        }
        m_Caches.Clear();
        List<MusicInfo> t = new List<MusicInfo>();
        for(int i = 0;i < PFVDatas.allMuscs.Count;i ++)
        {
            if (PFVDatas.allMuscs[i].type != m_Type)
                continue;
            t.Add(PFVDatas.allMuscs[i]);
        }
        for(int i = 0;i < t.Count;i ++)
        {
            MusicItem mi = Instantiate(musicItem) as MusicItem;
            mi.transform.parent = dm.transform;
            mi.transform.localScale = Vector3.one;
            mi.IniData(t[i]);
            m_Caches.Add(mi);
        }
        dm.SetContent<MusicItem>(m_Caches.ToArray(), false, true);
    }
}
