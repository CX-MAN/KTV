using UnityEngine;
using System.Collections;
public class MusicItem : MonoBehaviour
{
    MusicInfo mMusicInfo;
    public UILabel musicName, singer, Album;
    string strColor = "7aff8b";
    public GameObject mark;
    void Start()
    {

    }
    public void IniData(MusicInfo mi)
    {
        mMusicInfo = mi;
        
        if(PFVDatas.selectedList.Contains(mi))
        {
            MarkAsSelected();
        }
        else
        {
            MarkAsUnSelected();
        }
    }
    void MarkAsSelected()
    {
        musicName.text ="[b]["+strColor+"]"+mMusicInfo.musicName;
        singer.text = "[b][" + strColor + "]" + mMusicInfo.singer;
        Album.text = "[b][" + strColor + "]" + mMusicInfo.Album;
    }
    void MarkAsUnSelected()
    {
        musicName.text = mMusicInfo.musicName;
        singer.text = mMusicInfo.singer;
        Album.text = mMusicInfo.Album;
    }
    public void OnSelected()
    {
        bool flag = PFVDatas.selectedList.Contains(mMusicInfo);
        if (flag)
        {
            PFVDatas.selectedList.Remove(mMusicInfo);
            MarkAsUnSelected();
        }
        else
        {
            PFVDatas.selectedList.Add(mMusicInfo);
            MarkAsSelected();
        }
    }
    void OnPress(bool b)
    {
        if(b)
        {
            NGUIDragMenuClick nmc = GetComponent<NGUIDragMenuClick>();
            if (nmc == null)
            {
                nmc = this.gameObject.AddComponent<NGUIDragMenuClick>();
                NGUIDragMenu dm = this.gameObject.GetComponentInParent<NGUIDragMenu>();
                if (dm != null)
                {
                    nmc.SetNGUIDragMenu(dm);
                }
            }
        }
    }
}
