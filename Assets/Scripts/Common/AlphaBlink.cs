using UnityEngine;
using System.Collections;

public class AlphaBlink : MonoBehaviour
{

    public UISprite spr;
    public UITexture tex;
    float Acc = 0;
    bool isEnable = true;
    public float A = 1.0f, speed = 0.02f,B = 0,mFrom = 0.4f;
    void Start ()
    {
        
    }
    public void DisableBlink (bool isReset = true)
    {
        isEnable = false;
        if (isReset)
        {
            if(spr != null)
            spr.alpha = 1.0f;
             Acc = 0;
             if (tex != null)
                 tex.alpha = 1.0f;
        }
    }
    public void EnableBlink (bool isFromStart = true)
    {
        isEnable = true;
        if (isFromStart)
        {
            if (spr != null)
            spr.alpha = 1.0f;
            if (tex != null)
                tex.alpha = 1.0f;
            Acc = 0;
        }
    }
    void Update ()
    {
        if (!isEnable)
            return;
        A = Mathf.Abs (1.0f - mFrom) / 2;
        B = (1.0f + mFrom) / 2;
        Acc += speed * Time.deltaTime;
        if (Acc > 2 * Mathf.PI)
            Acc -= 2 * Mathf.PI;
        if (spr != null)
        spr.alpha = B+ A*Mathf.Cos (Acc);
        if (tex != null)
            tex.alpha = B + A * Mathf.Cos (Acc);
    }
}
