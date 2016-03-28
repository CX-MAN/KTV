using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalEnumType
{
    public enum EnumType
    {
        none,
        Welcome,//欢迎场景
        MainMenuScene,//主菜单场景
        Play,//播放场景
    }
    public enum buttonType
    {
        allMusic,//歌曲大全
        star,//歌星点歌
        newMusic,//新歌速递
        sevices,//酒水服务
        spell,//拼音
        musicType,//类型点歌
        ranking,//歌曲排行
        home      ,//主页
        play      ,//播放
        songList  ,//列表
        replay    ,//重播
        mute      ,//静音
        volume    ,//音量
        original  ,//原唱
        pause     ,//暂停
        accompany ,//伴唱
        next      ,//切歌
        exit      ,//退出

    }
    public enum buttonType1
    {
        play,//播放
        pause,//暂停
        last,//上一首
        next,//下一首
        origal,//原唱
        accompany,//伴唱
        volume,//音量
        back,//返回
    }
    public enum musicType//歌曲类型(语种)
    {
        Chinese,
        English,
        Cantonese
    }
}
