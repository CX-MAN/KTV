using UnityEngine;
using System.Collections.Generic;

public class AllMusicsWin : MonoBehaviour
{
    public MusicItem musicItem;
    List<MusicItem> m_Caches = new List<MusicItem>();
    public NGUIDragMenu dm;
    int m_Type = 0;
    void Start()
    {
        this.transform.localPosition = Vector3.zero;
        this.gameObject.SetActive(false);
    }
    public void Open(int type = 0)
    {
        m_Type = type;
        this.gameObject.SetActive(true);
        musicItem.gameObject.SetActive(false);
        IniData();
    }
    void IniData()
    {
        for(int i = 0;i < m_Caches.Count;i ++)
        {
            Destroy(m_Caches[i].gameObject);
        }
        m_Caches.Clear();
        List<MusicInfo> t = m_Type == 0 ? PFVDatas.allMuscs : PFVDatas.selectedList;
        for (int i = 0; i < t.Count; i++)
        {
            MusicItem mi = Instantiate(musicItem) as MusicItem;
            mi.transform.parent = dm.transform;
            mi.transform.localScale = Vector3.one;
            mi.IniData(t[i]);
            m_Caches.Add(mi);
        }
        dm.SetContent<MusicItem>(m_Caches.ToArray(), false, true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
