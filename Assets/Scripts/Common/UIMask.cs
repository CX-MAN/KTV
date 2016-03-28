using UnityEngine;
using System.Collections.Generic;

public class UIMask : MonoBehaviour
{
    public List<GameObject> CloseList = new List<GameObject> ();
    public UISprite uiMask;
    public bool isUseCollider = true;
    public EventDelegate eventDelegate;
    public UIPanel myPanel;
    [HideInInspector]
    public GameObject higherLayer = null;
    public bool IsUseSpringEffect = false;
    public bool isCloseSelfOnClick = false;
    void Awake()
    {
        uiMask.SetDimensions(2500, 1280);
    }
    void Start ()
    {
        if (isUseCollider)
        {
            BoxCollider bc = NGUITools.AddWidgetCollider(this.gameObject);
            bc.isTrigger = false;
        }
    }
    void OnEnable ()
    {
    }
    void OnDisable ()
    {
    }
    void OnClick ()
    {
        if (IsUseSpringEffect)
        {
            List<Transform> t = new List<Transform> ();
            for (int i = 0; i < CloseList.Count; i++)
            {
                if (CloseList[i] != null && CloseList[i].activeInHierarchy)
                    t.Add (CloseList[i].transform);
            }
            ZTools.SpringWinEffect (t.ToArray (), Vector3.one, ZTools.defaultMin, ZTools.defaultMax, 0.3f, CloseCallBack);
        }
        else
        {
            for (int i = 0; i < CloseList.Count; i++)
                if (CloseList[i] != null && CloseList[i].activeInHierarchy)
                    CloseList[i].SetActive (false);
            if (eventDelegate != null)
                eventDelegate.Execute ();
            if (isCloseSelfOnClick)
                this.gameObject.SetActive (false);
        }
        
        
    }
    void CloseCallBack ()
    {
        for (int i = 0; i < CloseList.Count; i++)
            if (CloseList[i] != null && CloseList[i].activeInHierarchy)
            {
                CloseList[i].SetActive (false);
                CloseList[i].transform.localScale = Vector3.one;
            }
        if (eventDelegate != null)
            eventDelegate.Execute ();
        if (isCloseSelfOnClick)
            this.gameObject.SetActive (false);
    }
    [ContextMenu ("Execute")]
    public void Reposition ()
    {

    }
}
