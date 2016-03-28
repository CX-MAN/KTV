using UnityEngine;
using System.Collections;
using System;
public class SingerItem : MonoBehaviour
{
    public UILabel Name;
    public UITexture photo;
    MusicInfo mMI;
    public Action<MusicInfo> OnClickEv = null;
    void Start()
    {

    }
    public void IniData(MusicInfo mi)
    {

        mMI = mi;
        Name.text = mi.singer;
        photo.mainTexture = Resources.Load<Texture2D>("Photo/" + mMI.photo);

    }

    public void OnClick()
    {
        if (OnClickEv != null)
        {
            OnClickEv(mMI);
        }
    }
}
