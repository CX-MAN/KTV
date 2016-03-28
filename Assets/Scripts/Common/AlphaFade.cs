using UnityEngine;
using System.Collections;

public class AlphaFade : MonoBehaviour
{
    public float from = 1.0f, to = 0.0f;
    public float speed = 0.004f;
    public UISprite[] targets;
    bool isHide = false;
    public void StartToFade (bool hideWhenFinishe = true)
    {
        isHide = hideWhenFinishe;
        if (targets == null)
            return;
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] != null)
                targets[i].alpha = from;
        }
    }
    void FixedUpdate ()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] != null&&targets[i].gameObject.activeInHierarchy)
            {
                targets[i].alpha -= speed;
                if (targets[i].alpha <= to)
                {
                    targets[i].alpha = to;
                    if (isHide)
                        targets[i].gameObject.SetActive (false);
                }
            }
        }
    }
}
