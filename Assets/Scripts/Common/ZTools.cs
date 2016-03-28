using UnityEngine;
using System;
//对齐风格
public enum IconAlignStyle
{
    left,
    right,
    middle
}
public class ZTools
{
    static bool stop = false;
    public static void Clear ()
    {
        stop = true;
    }
    public static string MakeStringGrey (string str, int RGB)
    {
        if (RGB == 0)
            RGB = 0x666666;
        string ret = "[" + Convert.ToString (RGB, 16) + "]" + str + "[-]";
        return ret;
    }
    public static bool Approximately (Vector3 a, Vector3 b)
    {
        return Mathf.Approximately (a.x, b.x) && Mathf.Approximately (a.y, b.y) && Mathf.Approximately (a.z, b.z);
    }
    static public T FindInParentExcludeSelf<T> (GameObject g) where T : Component
    {
        GameObject go = g.transform.parent.gameObject;
        if (go == null) return null;
        T comp = go.transform.parent.GetComponent<T> ();
        if (comp == null)
        {
            Transform t = go.transform.parent;

            while (t != null && comp == null)
            {
                comp = t.gameObject.GetComponent<T> ();
                t = t.parent;
            }
        }
        return comp;
    }
    
    public static string ToChineseNum (int num)//阿拉伯数字转换成中文数字
    {
        string ret = "",strD = "",strU="";
        int n = 0;
        if (num <= 10)
        {
            n = 35 + num;
            ret = AssetsLoad.GetText ("Key" + n);
        }
        else if(num < 100)
        {
            int d = num / 10;
            int u = num % 10;
            switch (u)
            {
                case 0:
                    {
                        strU = "";
                    }
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    {
                        strU = AssetsLoad.GetText("Key" +(35+ u));
                    }
                    break;
            }
            switch (d)
            {
                case 1:
                    {
                        strD = AssetsLoad.GetText("Key45");
                    }
                    break;
                case 2:
                    {
                        strD = AssetsLoad.GetText("Key37")+ AssetsLoad.GetText("Key45");
                    }
                    break;
                case 3:
                    {
                        strD = AssetsLoad.GetText("Key38") + AssetsLoad.GetText("Key45");
                    }
                    break;
                case 4:
                    {
                        strD = AssetsLoad.GetText("Key39") + AssetsLoad.GetText("Key45");
                    }
                    break;
                case 5:
                    {
                        strD = AssetsLoad.GetText("Key40") + AssetsLoad.GetText("Key45");
                    }
                    break;
                case 6:
                    {
                        strD = AssetsLoad.GetText("Key41") + AssetsLoad.GetText("Key45");
                    }
                    break;
                case 7:
                    {
                        strD = AssetsLoad.GetText("Key42") + AssetsLoad.GetText("Key45");
                    }
                    break;
                case 8:
                    {
                        strD = AssetsLoad.GetText("Key43") + AssetsLoad.GetText("Key45");
                    }
                    break;
                case 9:
                    {
                        strD = AssetsLoad.GetText("Key44") + AssetsLoad.GetText("Key45");
                    }
                    break;
            }
            ret = strD + strU;
        }
        return ret;
    }
    public static string DexmalRGBToHexRGB (int r, int g, int b)
    {
        string str = "";
        if (r < 16)
            str += "0" + Convert.ToString (r, 16);
        else
            str += Convert.ToString (r, 16);
        if (g < 16)
            str += "0" + Convert.ToString (g, 16);
        else
            str += Convert.ToString (g, 16);
        if (b < 16)
            str += "0" + Convert.ToString (b, 16);
        else
            str += Convert.ToString (b, 16);
        return "[" + str + "]";
    }
    public static string ConvertToIntWithSignal (float ret)
    {
        if (ret < 0)
            return "-" + Mathf.Abs (ret).ToString ("f0");
        else
            return "+" + ret.ToString ("f0");
    }
    public static string RemoveLastCharacter (string str)
    {
        if (str == null)
            return "";
        if (str.Length <= 1)
            return "";
        return str.Substring (0, str.Length - 1);
    }
    public static string RemoveFirstCharacter (string str)
    {
        if (str == null)
            return "";
        if (str.Length <= 1)
            return "";
        return str.Substring (1, str.Length-1);
    }
    public static void RecursivelySetGrey (Transform t)
    {

        for (int i = 0; i < t.childCount; i++)
        {
            Transform tmp = t.GetChild (i);
            UISprite spr = null;
            UITexture tex = null;
            if (tmp != null)
            {
                spr = tmp.GetComponent<UISprite> ();
                tex = tmp.GetComponent<UITexture> ();
                if (spr != null)
                    spr.color = new Color (0.025f, 1.0f, 1.0f);
                if (tex != null)
                    tex.color = new Color (0.025f, 1.0f, 1.0f);
                RecursivelySetGrey (tmp);
            }

        }
    }
    public static void RecursivelyReSetColor (Transform t)
    {

        for (int i = 0; i < t.childCount; i++)
        {
            Transform tmp = t.GetChild (i);
            UISprite spr = null;
            UITexture tex = null;
            if (tmp != null)
            {
                spr = tmp.GetComponent<UISprite> ();
                tex = tmp.GetComponent<UITexture> ();
                if (spr != null)
                    spr.color = new Color (1.0f, 1.0f, 1.0f);
                if (tex != null)
                    tex.color = new Color (1.0f, 1.0f, 1.0f);
                RecursivelySetGrey (tmp);
            }

        }
    }

   public delegate void FinishEvent ();
    static FinishEvent finishEvent = null;
    static float speed = 0, acc = 0;
    static Transform[] tmp = null;
    static bool startSpring = false, springBack = false;
    static Vector3 from, to, maxValue;
    const float a1 = 0.1f,a2 = 0.08f;
    public static Vector3 defaultMax = new Vector3 (1.05f,1.05f,1.05f);
    public static Vector3 defaultMin = new Vector3 (0.05f, 0.05f, 0.05f);
    public static void SpringWinEffect (Transform[] myTrans, Vector3 _from, Vector3 _to, Vector3 max, float _speed = 0.3f, FinishEvent finishEv = null)
    {
        if (myTrans == null || tmp != null || myTrans.Length == 0)//ban repeatly Setting.
            return;
        if (!myTrans[0].gameObject.activeInHierarchy)
        {
            
            myTrans[0].localScale = to;
            return;
        }
        //string str = "";
        //Transform s = myTrans[0];
        //while (s != null)
        //{
        //    str += "/" + s.name;
        //    s = s.transform.parent;
        //}
      //  UnityEngine.Debug.Log ("kkkkkk"+str);
        finishEvent = finishEv;
        tmp = myTrans;
        for (int i = 0; i < myTrans.Length && tmp[i] != null; i++)
        {
            tmp[i].localScale = _from;
        }
        startSpring = true;
        springBack = false;
        speed = _speed;
        acc = 0;
        maxValue = max;
        from = _from;
        to = _to;
    }

    public static void MyTimer ()
    {
        //springEffect
        if (startSpring)
        {
            if (tmp== null||tmp[0] == null)
            {
                startSpring = false;
                springBack = false;
                return;
            }
            speed += a1 * Time.deltaTime;
            acc += speed;
            if (acc >= 1.0f)
                acc = 1.0f;
            Vector3 vec = new Vector3 (Mathf.Lerp (from.x, maxValue.x, acc), Mathf.Lerp (from.y, maxValue.y, acc), Mathf.Lerp (from.z, maxValue.z, acc));
            for (int i = 0; i < tmp.Length && tmp[i] != null; i++)
                tmp[i].localScale = vec;
            if (Approximately (tmp[0].localScale, maxValue))
            {
                startSpring = false;
                for (int i = 0; i < tmp.Length && tmp[i] != null; i++)
                tmp[i].localScale = maxValue;
                acc = 0;
                springBack = true;
            }
        }
        if (springBack)
        {
            if (tmp[0] == null)
            {
                startSpring = false;
                springBack = false;
                return;
            }
            speed += a2 * Time.deltaTime;
            acc += speed;
            if (acc >= 1.0f)
                acc = 1.0f;
            Vector3 vec = new Vector3 (Mathf.Lerp (maxValue.x, to.x, acc), Mathf.Lerp (maxValue.y, to.y, acc), Mathf.Lerp (maxValue.z, to.z, acc));
            for (int i = 0; i < tmp.Length && tmp[i] != null; i++)
            tmp[i].localScale = vec;
            if (Approximately (tmp[0].localScale, to))
            {
                springBack = false;
                acc = 0;
                for (int i = 0; i < tmp.Length && tmp[i] != null; i++)
                {
                    tmp[i].localScale = to;
                }
                if (finishEvent != null)
                    finishEvent ();
                finishEvent = null;
                tmp = null;
            }
        }
    }
    public static string ConvertInt2StringF (int scr,int fillNum,string fillChar = "0")//前填充填充位数
    {
        string ret = "";
        string str;
        str = scr.ToString ();
        if (str.Length > fillNum)
        {
            return str;
        }
        for (int i = 0; i < fillNum - str.Length;i ++ )
        {
            ret += fillChar;
        }
        ret += str;
        return ret;
    }
    public static void ZLog(object o)
    {
#if UNITY_EDITOR
        UnityEngine.Debug.Log(o);
#endif
    }

    //对齐
    public static void HorizonalAlignFunc (Transform[]t,Vector3 basePos,float d = 20,IconAlignStyle ias = IconAlignStyle.middle)
    {
        int bit = t.Length;
        if (ias == IconAlignStyle.left)
        {
            for (int i = 0; i < t.Length; i++)
            {
                t[i].localPosition =basePos+ new Vector3 (i * d, 0, 0);
            }

        }
        else if (ias == IconAlignStyle.middle)
        {
            if (bit % 2 == 0)
            {
                int offset = bit / 2;
                for (int i = 0; i < bit; i++)
                {
                    t[i].transform.localPosition = basePos + new Vector3 (-(offset - 1) * d - d / 2 + i * d, 0, 0);
                }
            }
            else
            {
                int offset = bit / 2;
                for (int i = 0; i < bit; i++)
                {
                    t[i].transform.localPosition = basePos + new Vector3 (-offset * d + i * d, 0, 0);
                }
            }
        }
        else
        {
            for (int i = 0; i < bit; i++)
            {
                t[i].transform.localPosition = basePos + new Vector3 (-i * d, 0, 0);
            }
        }
    }
}
