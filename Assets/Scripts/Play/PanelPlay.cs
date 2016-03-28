using UnityEngine;
using System.Collections;
using GlobalEnumType;
public class PanelPlay : MonoBehaviour
{
    public UniversalButton[] buttons;
    public UniversalButton back;
    public MoveTip moveTip;
    public UILabel current, count, lyrics;
    SystemVolume mSystemVolume;
    bool m_IsOpen = true;
    int m_ind = 0;
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].ce = OnButtonClick;
            buttons[i].gameObject.SetActive(false);
        }
        back.ce = OnButtonClick;
        transform.localPosition = Vector3.zero;
        if(!Global.instance.audioSource.isPlaying)
        {
            Global.instance.Reset();
            Global.instance.Play();
        }
        int ind = Global.instance.GetInd();
        if (PFVDatas.selectedList.Count > ind)
        {
            MusicInfo mi = PFVDatas.selectedList[ind];
            PFVDatas.LoadLyrics(ref mi);
            current.text = "正在播放: " + mi.musicName;
        }
        InvokeRepeating("SetTip", 10, 10);
        moveTip.gameObject.SetActive(false);
        count.text = "已点: " + PFVDatas.selectedList.Count;
        Global.instance.refresh = Refresh;
        lyrics.text = "";
        mSystemVolume = (Instantiate(Resources.Load("Volume")) as GameObject).GetComponent<SystemVolume>();
        mSystemVolume.transform.parent = transform;
        mSystemVolume.transform.localScale = Vector3.one;
        mSystemVolume.transform.localPosition = Vector3.zero;
        Global.instance.BG.Ini(transform.parent);
        Global.instance.BG.SetBg("Res_BackGround6");
        m_ind = 0;
        m_IsOpen = true;

        UILabel[] u = MoveTip.FindObjectsOfType<UILabel>();
        for (int i = 0; i < u.Length; i++)
        {
            ZTools.ZLog("xxx:" + u[i].name);
        }
    }
    void OnDestroy()
    {
        Global.instance.refresh = null;
        CancelInvoke();
    }
    void SetTip()
    {
        int m_ind = Global.instance.GetInd();
        if (m_ind < PFVDatas.selectedList.Count - 1 && MoveTip.instCount <= 0)
        {
            MoveTip mt = Instantiate(moveTip) as MoveTip;
            mt.transform.parent = transform;
            mt.transform.localScale = Vector3.one;
            mt.transform.localPosition = new Vector3(180, 300, 0);
            mt.gameObject.SetActive(true);
            MusicInfo mi = PFVDatas.selectedList[m_ind + 1];
            mt.SetTip("下一首: " + mi.musicName);
            mt.StartRun(new Vector3(-440, 0, 0));
            MoveTip.instCount = 1;
        }
    }
     void Refresh()
    {
        int ind = Global.instance.GetInd();
        if (PFVDatas.selectedList.Count > ind)
        {
            MusicInfo mi = PFVDatas.selectedList[ind];
            if(mi.Lyrics.Count == 0)
            {
                PFVDatas.LoadLyrics(ref mi);
            }
        }
        if (PFVDatas.selectedList.Count > ind)
        {
            current.text ="正在播放: "+ PFVDatas.selectedList[ind].musicName;
        }
        if (ind < PFVDatas.selectedList.Count - 1)
        {
            if (MoveTip.current != null)
            {
                MoveTip.current.SetTip("下一首: " + PFVDatas.selectedList[ind + 1].musicName);
            }
        }
        else
        {
            if (MoveTip.current != null)
            {
                Destroy(MoveTip.current.gameObject);
                MoveTip.current = null;
            }
        }
     
    }
    void Play()
    {
        Global.instance.Play();
    }
    
    void OnButtonClick(int typeId)
    {
        buttonType1 bt = (buttonType1)typeId;
        switch (bt)
        {
            case buttonType1.play://播放
                {
                    Global.instance.audioSource.time = 0;
                    Play();
                }
                break;
            case buttonType1.pause://暂停
                {
                    Global.instance.Pause();
                }
                break;
            case buttonType1.last://上一首
                {
                    Global.instance.Last();
                    //if (m_ind > 0)
                    //{
                    //    m_ind--;
                    //}
                    //Play();
                }
                break;
            case buttonType1.next://下一首
                {
                    Global.instance.Next();
                    //if (m_ind < PFVDatas.selectedList.Count - 1)
                    //{
                    //    m_ind++;
                    //}
                    //Play();
                }
                break;
            case buttonType1.origal://原唱
                {
                    Global.instance.Original();
                }
                break;
            case buttonType1.accompany://伴唱 
                {
                    Global.instance.Accompany();
                }
                break;
            case buttonType1.volume://音量 
                {
                    mSystemVolume.Open();
                }
                break;
            case buttonType1.back://返回
                {
                    Global.EnterScene(EnumType.MainMenuScene);
                }
                break;
        }
    }
    public void OpenList()
    {
        StopAllCoroutines();
        m_IsOpen = m_ind == 0;
        StartCoroutine("ButtonEffect");
    }
    IEnumerator ButtonEffect()
    {
        yield return new WaitForSeconds(0.025f);
        if(m_IsOpen)
        {
            buttons[m_ind].gameObject.SetActive(true);
           
            if (m_ind < buttons.Length-1)
            {
                m_ind++;
                StartCoroutine("ButtonEffect");
            }
        }
        else
        {
            buttons[m_ind].gameObject.SetActive(false);
            if (m_ind > 0)
            {
                m_ind--;
                StartCoroutine("ButtonEffect");
            }
        }
      
    }
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.K))
        //{
        //    MusicInfo mi = PFVDatas.selectedList[1];
        //    audioSource.clip = Resources.Load("Musics/" + mi.fullName) as AudioClip;
        //    audioSource.time = 10.0f;
        //    audioSource.Play();
        //    Debug.Log("change!:"+ audioSource.time);
        //}
        int id = Global.instance.GetInd();
        if(PFVDatas.selectedList.Count > id)
        {
            MusicInfo mi = PFVDatas.selectedList[id];
            if(mi.Lyrics.Count > 1)
            {
                string str = "";
                foreach(var s in mi.Lyrics)
                {
                    if(Global.instance.audioSource.time > s.Key)
                    {
                        str = s.Value;
                    }
                    else
                    {
                        break;
                    }
                }
                lyrics.text = str;
            }
        }
    }
}
