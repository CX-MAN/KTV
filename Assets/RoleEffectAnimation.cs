using UnityEngine;
using System.Collections;

public class RoleEffectAnimation : MonoBehaviour {
   // ActorAnimation actorAnimation;
    public GameObject effect;
    void Start()
    {
        Application.targetFrameRate = 30;
        //actorAnimation = GetComponent<ActorAnimation>();
        //actorAnimation.ActorStart();
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Skill("SkillAnima0");
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            Skill("SkillAnima1");
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            Skill("SkillAnima2");
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            Skill("SkillAnima3");
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            Skill("SkillAnima4");
        }
    }
    void Skill(string CurrAniam)
    {
        if (effect != null)
        {
            effect.SetActive(false);
            effect.transform.position = transform.position + new Vector3(0,0.5f,0);
            effect.transform.rotation = Quaternion.LookRotation(transform.position + new Vector3(2, 0, 0));
            effect.transform.localEulerAngles = new Vector3(0, effect.transform.localEulerAngles.y, effect.transform.localEulerAngles.z);
            effect.SetActive(true);
        }
        else
        {
            Debug.Log("特效不存在");
        }
        //if (actorAnimation.anima.GetClip(CurrAniam) == null)
        //{
        //    Debug.Log("动画名：" + CurrAniam + "不存在");
        //}
        //else
        //{
        //    actorAnimation.anima.Stop();
        //    actorAnimation.anima.Play(CurrAniam);
        //}
    }
}
