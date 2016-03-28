using System;
using System.Collections.Generic;
public class MusicInfo:IComparable<MusicInfo>
{
    public string fullName;//音乐文件全名
    public string musicName;//音乐名
    public string singer;//歌手
    public string Album = "未知专辑";//专辑
    public int type;//歌曲类型 0 汉语，1英语，2粤语
    public string spellName;//拼音名
    public string photo;//插画
    public Dictionary<float, string> Lyrics = new Dictionary<float, string>();
    public int sortParam = 0;
    public bool IniData(string[] s)
    {
        if (s.Length < 1)
            return false;
        bool ret = true;
        int len = s.Length;
        fullName = s[0];
        musicName = len > 1 ? s[1] : "";
        singer = len > 2 ? s[2] : "";
        Album = len > 3 ? s[3] : "";   
        type = len > 4 ?Convert.ToInt32(s[4]) : 0;
        photo = len > 5 ? s[5].Trim('\r') : "";
        spellName = len > 6 ? s[6].Trim('\r') : "";
        //if(type == 0||type == 2)
        //{
        //    spellName = ChineseToSpell.GetChineseSpell(musicName);
        //}
        //else
        //{
        //    spellName = musicName;
        //}
        sortParam = 0;
        return ret;
    }

    public int CompareTo(MusicInfo other)
    {
        return sortParam.CompareTo(other.sortParam)*-1;
    }

    public MusicInfo()
    {

    }
    public MusicInfo(MusicInfo mi)
    {
        fullName = mi.fullName;
        musicName = mi.musicName;
        singer = mi.singer;
        Album = mi.Album;
        type = mi.type;
        spellName = mi.spellName;
        photo = mi.photo;
        sortParam = mi.sortParam;
    }
}

