using UnityEngine;
using System.Collections.Generic;

public class NGUIDragMenuClick : MonoBehaviour
{
    NGUIDragMenu cNGUIDragMenu;
    [HideInInspector]
    public List<NGUIDragMenuClick> list = new List<NGUIDragMenuClick>();
    public int DatasIndex { set; get; }
    public void SetNGUIDragMenu(NGUIDragMenu nguiDragmenu)
    {
        cNGUIDragMenu = nguiDragmenu;
    }
    void OnPress(bool b)
    {
        if (b)
        {

            if (cNGUIDragMenu != null)
            {
                cNGUIDragMenu.OpenUpdate(b);
            }

        }
    }
}
 

