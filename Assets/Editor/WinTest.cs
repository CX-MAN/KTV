using UnityEngine;
using System.Collections;
using  UnityEditor;
public class WinTest : EditorWindow  {

    [MenuItem("CustomMenu/TestWindow")]
    static void Win()
    {
        var window = GetWindow(typeof(WinTest));

       	window.position =new  Rect(800, 500, 200, 200);
		//window.ShowPopup();
        window.title = "测试窗口";
        window.Show();
    }
}
