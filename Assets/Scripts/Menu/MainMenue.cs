using UnityEngine;
using GlobalEnumType;
using System.Collections.Generic;
using System.Collections;
public class MainMenue : MonoBehaviour
{
    public static MainMenue mainMenue = null;
    public List<UniversalButton> allButtons;
    public List<UniversalButton> homeButtons;
    int m_ind = 0;
    public UILabel time,current,total;
    public AllMusicsWin allMusicWin;
    public MusicByStarsWin musicByStarsWin;
    public MusicByTypeWin musicByTypeWin;
    public MusicBySpellWin musicBySpellWin;
    public GameObject home;
    string currentName = "";
    SystemVolume mSystemVolume;
    List<Object> allSubWin = new List<Object>();
    
    void Start()
    {
        mainMenue = this;
        this.transform.localPosition = Vector3.zero;
        for (int i = 0; i < homeButtons.Count; i++)
        {
            homeButtons[i].ce = this.OnButtonClick;
        }
        for (int i = 0; i < allButtons.Count; i++)
        {
            allButtons[i].ce = this.OnButtonClick;
            allButtons[i].gameObject.SetActive(false);
        }
        m_ind = 0;
        allButtons[0].gameObject.SetActive(true);
        StartCoroutine("ButtonEffect");
        InvokeRepeating("UpdateTime", 0, 0.5f);
        mSystemVolume = (Instantiate(Resources.Load("Volume")) as GameObject).GetComponent<SystemVolume>();
        mSystemVolume.transform.parent = transform;
        mSystemVolume.transform.localScale = Vector3.one;
        mSystemVolume.transform.localPosition = Vector3.zero;
        allSubWin.Add(allMusicWin);
        allSubWin.Add(musicByStarsWin);
        allSubWin.Add(musicByTypeWin);
        allSubWin.Add(musicBySpellWin);
        home.SetActive(true);
        Global.instance.BG.Ini(transform.parent);
        Global.instance.BG.SetBg("Res_BackGround1");
    }
    void OnDestroy()
    {
        mainMenue = null;
        Global.instance.refresh = null;
    }
    IEnumerator ButtonEffect()
    {
        yield return new WaitForSeconds(0.025f);
        allButtons[m_ind].gameObject.SetActive(true);
        m_ind++;
        if (m_ind < allButtons.Count)
        {
            StartCoroutine("ButtonEffect");
        }
    }
   
    void UpdateTime()
    {
        time.text = System.DateTime.Now.ToString("HH: mm:ss");
        total.text = "已点: " + PFVDatas.selectedList.Count;
        AudioClip ac = Global.instance.audioSource.clip;
        if (ac != null&&currentName != ac.name)
        {
            int ind = Global.instance.GetInd();
            if(ind < PFVDatas.selectedList.Count)
            {
                current.text = "正在播放: " + PFVDatas.selectedList[ind].musicName;
            }
            currentName = ac.name;
        }
    }
    public void OnHomeSubWinBack()
    {
        home.SetActive(true);
        for(int i = 0;i < allSubWin.Count;i ++)
        {
            MonoBehaviour mb = (MonoBehaviour)allSubWin[i];
            if(mb.gameObject.activeInHierarchy)
            {
                mb.gameObject.SetActive(false);
            }
            Global.instance.BG.SetBg("Res_BackGround1");
        }
    }
    void OnButtonClick(int typeId)
    {
        buttonType bt = (buttonType)typeId;
        switch (bt)
        {
            case buttonType.home://主页
                {
                    OnHomeSubWinBack();
                }
                break;
            case buttonType.play://播放
                {
                    Global.EnterScene(EnumType.Play);
                }
                break;
            case buttonType.songList://列表
                {
                    OnHomeSubWinBack();
                    home.SetActive(false);
                    allMusicWin.Open(1);
                    Global.instance.BG.SetBg("Res_BackGround1");
                }
                break;
            case buttonType.replay://重播
                {
                    
                    Global.instance.Replay();
                }
                break;
            case buttonType.mute://静音
                {
                    bool flag = Global.instance.audioSource.mute;
                    Global.instance.audioSource.mute =!flag;
                }
                break;
            case buttonType.volume://音量
                {
                    mSystemVolume.Open();
                }
                break;
            case buttonType.original://原唱
                {
                    Global.instance.Original();
                }
                break;
            case buttonType.pause://暂停
                {
                    Global.instance.Pause();
                }
                break;
            case buttonType.accompany://伴唱
                {
                    Global.instance.Accompany();
                }
                break;
            case buttonType.next://切歌
                {
                    Global.instance.Next();
                }
                break;
            case buttonType.exit://退出
                {
#if UNITY_EDITOR
                    Global.EnterScene(EnumType.Welcome);
#endif
                    Application.Quit();
                }
                break;
            case buttonType.allMusic:       //歌曲大全
                {
                    home.SetActive(false);
                    allMusicWin.Open();
                    Global.instance.BG.SetBg("Res_BackGround1");
                }
                break;
            case buttonType.star:       //歌星点歌
                {
                    home.SetActive(false);
                    musicByStarsWin.Open();
                    Global.instance.BG.SetBg("Res_BackGround2");
                }
                break;
            case buttonType.newMusic:     //新歌速递
                {
                }
                break;
            case buttonType.sevices:     //酒水服务
                {

                }
                break;
            case buttonType.spell:    //拼音
                {
                    home.SetActive(false);
                    musicBySpellWin.Open();
                    Global.instance.BG.SetBg("Res_BackGround3");
                }
                break;
            case buttonType.musicType: //类型点歌
                {
                    home.SetActive(false);
                    musicByTypeWin.Open();
                    Global.instance.BG.SetBg("Res_BackGround4");
                }
                break;
            case buttonType.ranking:      //歌曲排行
                {

                }
                break;
        }
    }
}
