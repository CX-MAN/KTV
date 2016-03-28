using UnityEngine;
using System.Collections;

public class Begin : MonoBehaviour
{
    public Animation anim;
    void Awake()
    {
        if (Global.instance == null)
        {
            Global.instance = (new GameObject()).AddComponent<GameMange>();
        }
    }
    void Start()
    {
        Global.instance.BG.Ini(transform);
        Global.instance.BG.SetBg("Res_BackGround0");
        if (anim != null)
        {
            anim.wrapMode = WrapMode.PingPong;
            anim.Play("IconAnim");
        }
    }
    public void OnEnter()
    {
        Global.EnterScene(GlobalEnumType.EnumType.MainMenuScene);
    }
}
