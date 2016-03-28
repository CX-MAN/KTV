using System;
using System.Collections.Generic;
using UnityEngine;
public class PFVDatas
{
    public static PFVDatas pfvDatas
    {
        get
        {
            if (mPFVDatas == null)
            {
                mPFVDatas = new PFVDatas();
            }
            return mPFVDatas;
        }
    }
    static PFVDatas mPFVDatas;
    public static List<MusicInfo> allMuscs = new List<MusicInfo>();
    public static List<MusicInfo> selectedList = new List<MusicInfo>();
    public void IniData()
    {
        LoadConfig();
        Debug.Log("load config finished!");
    }
    void LoadConfig()
    {
        allMuscs.Clear();
        string text = "";
        TextAsset textAsset = Resources.Load("Config/Config") as TextAsset;
        text = textAsset.text;
        string[] str = text.Split('\n');
        for (int i = 0; i < str.Length; i++)
        {
            if (!string.IsNullOrEmpty(str[i]))
            {
                string[] s = str[i].Split('*');
                if (s.Length < 2) continue;
                MusicInfo mi = new MusicInfo();
                if (mi.IniData(s))
                {
                    allMuscs.Add(mi);
                }
            }
        }
        Resources.UnloadAsset(textAsset);
    }
    public static void LoadLyrics(ref MusicInfo mi)
    {
        if (mi.Lyrics.Count > 0)
            return;
        mi.Lyrics.Clear();
        string text = "";
        TextAsset textAsset = Resources.Load("Lyrics/" + mi.fullName) as TextAsset;
        if (textAsset == null)
        {
            Debug.Log("未配置歌词!");
            mi.Lyrics.Add(0, "");
            return;
        }
        Debug.Log("加载歌词成功!");
        text = textAsset.text;
        string[] str = text.Split('\n');
        for (int i = 0; i < str.Length; i++)
        {
            if (!string.IsNullOrEmpty(str[i]))
            {
                Char[] ch = new char[] { '[', ']' };
                string[] s = str[i].Split(ch);
                string key = "";
                float k = 0;
                if (s.Length != 3)
                    continue;
                key = s[1];
                string[] l = key.Split(':');
                if (l.Length < 2)
                    continue;
                int m = 0;
                float f = 0;
                if(!int.TryParse(l[0], out m))
                    continue;
                if(!float.TryParse(l[1], out f))
                    continue;
                k = m * 60 + f;
                if (!mi.Lyrics.ContainsKey(k))
                {
                    mi.Lyrics.Add(k, s[2]);
                }
            }
        }
        Resources.UnloadAsset(textAsset);
    }
}

