using UnityEngine;
using System.Collections.Generic;
public class MusicByStarsWin : MonoBehaviour
{
    public NGUIDragMenu dm,dm1;
    public MusicItem musicItem;
    public SingerItem singerItem;
    Dictionary<string, MusicInfo> AllSingers = new Dictionary<string, MusicInfo>();
    public  List<SingerItem> Singers = new List<SingerItem>();

    List<MusicItem> m_Caches1 = new List<MusicItem>();
    public GameObject singers, songs;
    int mPageInd = 0;
    void Start()
    {
        this.gameObject.SetActive(false);
        transform.localPosition = Vector3.zero;
    }
    public void Open()
    {
        musicItem.gameObject.SetActive(false);
        singerItem.gameObject.SetActive(false);
        gameObject.SetActive(true);
        songs.SetActive(false);
        singers.SetActive(true);
        IniData();
    }
    // Update is called once per frame
    public void IniData()
    {
        AllSingers.Clear();
        for(int i = 0;i < PFVDatas.allMuscs.Count;i ++)
        {
            MusicInfo mi = PFVDatas.allMuscs[i];
            if(!AllSingers.ContainsKey(mi.singer))
            {
                AllSingers.Add(mi.singer, mi);
            }
        }

        mPageInd = 1;
        IniSingers();
    }
    void IniSingers()
    {
        for (int i = 0; i < Singers.Count; i++)
        {
            Singers[i].gameObject.SetActive(false);
        }
        int n = 0;
        foreach (var s in AllSingers.Values)
        {
            if(n < (mPageInd-1)*3)
            {
                n++;
                continue;
            }
            int n1 = n - (mPageInd - 1) * 3;
            Singers[n1].gameObject.SetActive(true);
            Singers[n1].IniData(s);
            Singers[n1].OnClickEv = OnSingerClick;
            n++;
            if (n >= (mPageInd -1)*3+3)
                break;
        }
    }
    public void NextPage()
    {
        int totalP = AllSingers.Count % 3 == 0? AllSingers.Count/3: AllSingers.Count / 3+1;
        if(mPageInd < totalP)
        {
            mPageInd++;
            IniSingers();
        }
    }
    public void LastPage()
    {
        
        if (mPageInd > 1)
        {
            mPageInd--;
            IniSingers();
        }
    }
    public void ReturnBt()
    {
        singers.SetActive(true);
        songs.SetActive(false);
    }
    void OnSingerClick(MusicInfo mi)
    {
        singers.SetActive(false);
        songs.SetActive(true);
        IniSongs(mi);
    }
    void IniSongs(MusicInfo mi)
    {
        for (int i = 0; i < m_Caches1.Count; i++)
        {
            Destroy(m_Caches1[i].gameObject);
        }
        m_Caches1.Clear();
        List<MusicInfo> t = new List<MusicInfo>();
        for (int i = 0; i < PFVDatas.allMuscs.Count; i++)
        {
            if (PFVDatas.allMuscs[i].singer == mi.singer)
            {
                t.Add(PFVDatas.allMuscs[i]);
            }
        }
        for (int i = 0; i < t.Count; i++)
        {

            MusicItem si = Instantiate(musicItem) as MusicItem;
            si.transform.parent = dm1.transform;
            si.transform.localScale = Vector3.one;
            si.IniData(t[i]);
            m_Caches1.Add(si);
        }
        dm1.SetContent<MusicItem>(m_Caches1.ToArray(), false, true);
    }
    public void BackBt()
    {
        MainMenue.mainMenue.OnHomeSubWinBack();

    }
}
