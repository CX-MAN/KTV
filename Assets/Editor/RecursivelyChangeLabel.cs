using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Text;
using System.IO;
public class RecursivelyChangeLabel : Editor
{
    static Dictionary<string, string> LabelContexts = new Dictionary<string, string>();
    static int index = 0;
    [MenuItem("NGUI/LabelToGenerateConfigFile")]
    static void FindLabelsInChildren()
    {
        index = 0;
        LabelContexts.Clear();
        Transform[] selection =  Selection.GetTransforms( SelectionMode.TopLevel | SelectionMode.Editable);
        foreach(Transform t in selection)
        {
            Change(t);
        }
        SaveAsConfigFile();
    }
     
    static void Change(Transform T)
    {
        for(int i = 0 ; i < T.childCount; i ++)
        {
            Transform childTransform = T.GetChild(i);
            UILabel lb = childTransform.GetComponent<UILabel>();
            if(lb != null)
            {
                int k = 0;
                if(int.TryParse(lb.text,out k))
                {
                    //数字
                }
                else
                {
                    //非数字
                    index++;
                    LabelContexts.Add("Key" + index, lb.text);
                }
            }
            Change(childTransform);
        }
    }
    // 如果什么都没有选择将禁用菜单功能
    [MenuItem("NGUI/LabelToGenerateConfigFile",true)]
    static bool ValidateSelection()
    {
        return Selection.activeGameObject != null;
    }
    static void SaveAsConfigFile()
    {
        string path = Application.dataPath + "/Resources/TextFolder/TextConfig.txt";
        StreamWriter sw = new StreamWriter(path,true,Encoding.UTF8);
   
         string strout = "\n";
         foreach (KeyValuePair<string, string> s in LabelContexts)
         {
             strout += s.Key + "=" + s.Value + "\r\n";
         }
         sw.Write(strout);
         sw.Flush();
         sw.Close();
        
       Debug.Log("配置文件路径:" + path + "\n配置文件生成完毕!");
    }
}