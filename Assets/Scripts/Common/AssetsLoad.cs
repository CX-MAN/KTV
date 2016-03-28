using UnityEngine;
using System.Collections.Generic;
using System.IO;
public class AssetsLoad
{
    public static Dictionary<string, string> Texts = new Dictionary<string, string>();
    static List<string> FilterWords = new List<string>();
    public static Dictionary<string, Object> AllAssetObject = new Dictionary<string, Object>();
    public static Dictionary<string, Texture2D> TextureDic = new Dictionary<string, Texture2D>();
    public static Dictionary<string, List<UIWidget>> TextureWidgetDic = new Dictionary<string, List<UIWidget>>();
    public static Dictionary<string, AudioClip> AudioClipDic = new Dictionary<string, AudioClip>();
    public static Dictionary<string, List<GameObject>> particles = new Dictionary<string, List<GameObject>>();
    public static Dictionary<string, string> PlayerPrefsDatas = new Dictionary<string, string>();
    public static void UnAssets()
    {
        foreach (List<GameObject> obj in particles.Values)
        {
            for (int i = 0; i < obj.Count; i++)
            {
                if (obj[i])
                {
                    Object.DestroyImmediate(obj[i]);
                }
            }
        }
        particles.Clear();
        List<string> keys = new List<string>(AudioClipDic.Keys);
        for (int i = 0; i < keys.Count; i++)
        {
            Resources.UnloadAsset(AudioClipDic[keys[i]]);
            AudioClipDic[keys[i]] = null;
        }
        keys = new List<string>(TextureDic.Keys);
        for (int i = 0; i < keys.Count; i++)
        {
            Resources.UnloadAsset(TextureDic[keys[i]]);
            TextureDic[keys[i]] = null;
        }
        keys = new List<string>(AllAssetObject.Keys);
        for (int i = 0; i < keys.Count; i++)
        {
            AllAssetObject[keys[i]] = null;
        }
        TextureWidgetDic.Clear();
        keys.Clear();
        AllAssetObject.Clear();
        AudioClipDic.Clear();
        TextureDic.Clear();
    }
    public static void UnAssetsWar()
    {
        foreach (List<GameObject> obj in particles.Values)
        {
            for (int i = 0; i < obj.Count; i++)
            {
                if (obj[i])
                {
                    if (obj[i].gameObject.activeInHierarchy)
                    {
                        obj[i].gameObject.SetActive(false);
                    }
                    Object.DestroyImmediate(obj[i]);
                }
            }
        }
        particles.Clear();
        List<string> keys = new List<string>(AllAssetObject.Keys);
        for (int i = 0; i < keys.Count; i++)
        {
            AllAssetObject[keys[i]] = null;
        }
        keys.Clear();
        AllAssetObject.Clear();
    }
    public static string GetText(string Key)
    {
        if (string.IsNullOrEmpty(Key))
        {
            return "";
        }
        if (Texts.ContainsKey(Key))
        {
            return Texts[Key];
        }
        else
        {
           
        }
        return string.Format(Texts["Not"], Key);
    }
    public static void LoadText()
    {
        if (Texts.Count == 0)
        {
            
        }
        Texts.Clear();
        string text = "";
        TextAsset textAsset = Resources.Load("TextFolder/TextConfig") as TextAsset;
        text = textAsset.text;

        string[] str = text.Split('\n');
        for (int i = 0; i < str.Length; i++)
        {
            if (!string.IsNullOrEmpty(str[i]))
            {
                string[] s = str[i].Split('=');
                if (s.Length < 2) continue;
                string te = s[0];
                if (!string.IsNullOrEmpty(te))
                {
                    te = te.Trim();
                    if (te.Contains("LodingScene"))
                    {
                        
                    }
                    if (te.Contains("ChatTisp00"))
                    {
                       
                    }
                    if (s[1].IndexOf('|') > -1)
                    {
                        s[1] = s[1].Replace('|', '\n');
                    }
                    if (!Texts.ContainsKey(te))
                    {
                        Texts.Add(te, s[1].Trim());
                    }
                    else
                    {
                        Texts[te] = s[1].Trim();
                    }
                }
            }
        }
    }
}
