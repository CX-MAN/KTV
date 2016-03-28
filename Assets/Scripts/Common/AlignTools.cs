using UnityEngine;
using System;
using System.Collections.Generic;
public class AlignTools : MonoBehaviour
{

    public Transform[] items;
    public enum Style
    {
        horizonal,
        vertical,
    }
    //对齐风格
    public enum AlignStyle
    {
        left,
        right,
        middle
    }
    public Style style = Style.horizonal;
    public AlignStyle alignStyle = AlignStyle.left;
    public int row = 0, colum = 0;
    public int w, h;
    public Transform basePos;
    [ContextMenu ("Execute")]
    public void Align ()
    {
        if (items.Length == 0)
            return;
        if (style == Style.horizonal)
        {
            if (row == 0)
                row = 1;
            if (alignStyle == AlignStyle.left)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    items[i].localPosition = basePos.localPosition + new Vector3 (i * w, 0, 0);
                }

            }
            else if (alignStyle == AlignStyle.middle)
            {
                //    if (bit % 2 == 0)
                //    {
                //        int offset = bit / 2;
                //        for (int i = 0; i < bit; i++)
                //        {
                //            t[i].transform.localPosition = basePos + new Vector3 (-(offset - 1) * d - d / 2 + i * d, 0, 0);
                //        }
                //    }
                //    else
                //    {
                //        int offset = bit / 2;
                //        for (int i = 0; i < bit; i++)
                //        {
                //            t[i].transform.localPosition = basePos + new Vector3 (-offset * d + i * d, 0, 0);
                //        }
                //    }
                //}
                //else
                //{
                //    for (int i = 0; i < bit; i++)
                //    {
                //        t[i].transform.localPosition = basePos + new Vector3 (-i * d, 0, 0);
                //    }
                }
            }
            else if (style == Style.vertical)
            {
                if (colum == 0)
                    colum = 1;
                if (alignStyle == AlignStyle.left)
                {
                    //for (int i = 0; i < items.Length; i++)
                    //{
                    //    items[i].localPosition = basePos.localPosition + new Vector3 (i * w, 0, 0);
                    //}

                }
                else if (alignStyle == AlignStyle.middle)
                {
                    for (int i = 0; i < items.Length; i++)
                    {
                        items[i].transform.localPosition = basePos.transform.localPosition + new Vector3 (0,h*i,0);
                    }
                }
                else
                {
                    //for (int i = 0; i < bit; i++)
                    //{
                    //    t[i].transform.localPosition = basePos + new Vector3 (-i * d, 0, 0);
                    //}
                }
            }
        }
    }


