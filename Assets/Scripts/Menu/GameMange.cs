using UnityEngine;
using System.Collections;
using System;
public class GameMange : MonoBehaviour
{
    public Action refresh = null;
    public PanelBg BG
    {
        set
        {
            mBG = value;
        }
        get
        {
            if(mBG == null)
            {
                mBG = (Instantiate(Resources.Load("PanelBg")) as GameObject).GetComponent<PanelBg>();
            }
            return mBG;
        }
    }
    PanelBg mBG;
    public float Volume
    {
        set
        {
            mVolume = value;
            Global.instance.audioSource.volume = value;
        }
        get
        {
            return mVolume;
        }
    }
    float mVolume = 0.5f;
    int m_Ind
    {
        set
        {
            mInd = value;
            if(refresh != null)
            {
                refresh();
            }
        }
        get
        {
            return mInd;
        }
    }
    int mInd = 0;
    public AudioSource audioSource { set; get; }
    
    void OnDestroy()
    {
        Debug.Log("destroy:"+this.gameObject.name);
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.name = this.GetType().FullName;
        audioSource = gameObject.AddComponent<AudioSource>();
        PFVDatas.pfvDatas.IniData();
        Application.runInBackground = true;
        Volume = 0.5f;
    }
    public void Replay()
    {
        audioSource.time = 0;
        audioSource.Play();
    }
    public void Reset()
    {
        m_Ind = 0;
    }
   public void Play()
    {
        CancelInvoke("AutoNext");
        if (PFVDatas.selectedList.Count > m_Ind)
        {
            MusicInfo mi = PFVDatas.selectedList[m_Ind];
            Global.instance.audioSource.clip = Resources.Load("Musics/" + mi.fullName) as AudioClip;
            audioSource.Play();
            //current.text = "正在播放:" + mi.musicName;
            Invoke("AutoNext", Global.instance.audioSource.clip.length);
        }
    }
    void AutoNext()
    {
        if (m_Ind < PFVDatas.selectedList.Count - 1)
        {
            m_Ind++;
            Play();
        }
    }
    public void Pause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            CancelInvoke("AutoNext");
        }
        else
        {
            audioSource.Play();
        }
    }
    public void Next()
    {
        if (m_Ind < PFVDatas.selectedList.Count - 1)
        {
            m_Ind++;
            audioSource.time = 0;
        }
        Play();
    }
    public void Last()
    {
        if (m_Ind > 0)
        {
            m_Ind--;
            audioSource.time = 0;
        }
        Play();
    }
    public void Original()
    {
        if (PFVDatas.selectedList.Count <= m_Ind)
            return;
        MusicInfo mi = PFVDatas.selectedList[m_Ind];
        AudioClip ac = Resources.Load("Musics/" + mi.fullName) as AudioClip;
        if (ac != null)
        {
            float t = audioSource.time;
            audioSource.clip = ac;
            audioSource.time = t;
            audioSource.Play();
        }
    }
    public void Accompany()
    {
        if (PFVDatas.selectedList.Count <= m_Ind)
            return;
        if (audioSource.isPlaying)
        {
            MusicInfo mi = PFVDatas.selectedList[m_Ind];
            AudioClip ac = Resources.Load("Accompany/" + mi.fullName) as AudioClip;
            if (ac != null)
            {
                float t = audioSource.time;
                audioSource.clip = ac;
                audioSource.time = t;
                audioSource.Play();
            }

        }
    }
    public int GetInd()
    {
        return m_Ind;
    }

}
