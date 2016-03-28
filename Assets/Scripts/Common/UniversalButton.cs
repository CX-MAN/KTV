using UnityEngine;
using System;
public class UniversalButton : MonoBehaviour
{
    public delegate void ClickEvent(int typeId);
    public ClickEvent ce = null;
    public int typeId;
    AudioClip mAudioClip;
    public bool isPlayClickSound = true;
    public GameObject selectedFrame;
    public UILabel Name;
    public GameObject[] normals;
    public GameObject[] selecteds;
    public UILabel[] normalFonts;
    public UILabel[] selectedFonts;
    void Start()
    {
        mAudioClip = Resources.Load("Sound/Button") as AudioClip;
        if(selectedFrame != null)
        {
            selectedFrame.SetActive(false);
        }
    }
    public void MarkAsSelected()
    {
        for(int i = 0;i < selectedFonts.Length;i ++)
        {
            selectedFonts[i].gameObject.SetActive(true);
        }
        for(int i = 0;i < selecteds.Length;i ++)
        {
            selecteds[i].SetActive(true);
        }
        for(int i = 0;i < normalFonts.Length;i ++)
        {
            normalFonts[i].gameObject.SetActive(false);

        }
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i].SetActive(false);
        }
    }
    public void MarkAsNormal()
    {
        for (int i = 0; i < selectedFonts.Length; i++)
        {
            selectedFonts[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < selecteds.Length; i++)
        {
            selecteds[i].SetActive(false);
        }
        for (int i = 0; i < normalFonts.Length; i++)
        {
            normalFonts[i].gameObject.SetActive(true);

        }
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i].SetActive(true);
        }
    }
    void OnClick()
    {
        Debug.Log("click");
        if (ce != null)
        {
            ce(typeId);
        }
        if(isPlayClickSound)
        {
            Global.instance.audioSource.PlayOneShot(mAudioClip);
        }
    }
}
