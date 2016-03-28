using UnityEngine;
using System.Collections;

public class PanelBg : MonoBehaviour
{
    public UITexture texBg;
    void Start()
    {
        
        this.transform.localPosition = Vector3.zero;
        Global.instance.BG = this;
    }
    public void SetBg(string bgName)
    {
       texBg.mainTexture = Resources.Load("PFVRes/"+ bgName) as Texture;
    }
    public void Ini(Transform t)
    {
        transform.parent = t;
        transform.localScale = Vector3.one;
        transform.transform.localPosition = Vector3.zero;
    }

}
