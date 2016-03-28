using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NGUIDragMenu : MonoBehaviour
{
    public NGUIDragMenu nGUIDragMenu;
    public GameObject PressSendObject;
    public GameObject ToTargetSendObject;
    public string FuntionName;
    public bool IsNoCreateCollider;
    public enum Direction { Cross, Vertical }
    public Direction direction = Direction.Cross;
    public int col = 0, row = 0;
    public float clipRangew, clipRangez, photoWidth = 90.0f, photoHeight = 90.0f, spaceX = 2.0f, spaceY = 2.0f;
    //public bool IsApplySort;
    [HideInInspector]
    public List<GameObject> photos = new List<GameObject>();
    Rect mouseRect = new Rect(0, 0, 1, 1);
    GameObject photoAdd;
    float allWidth = 0, touchDownX, lastLeft, touchFollowX, mouseX, xianzhiLeft, offsetX;
    float touchDownY, lastTop, touchFollowY, mouseY, xianzhiTop, ti;
    public float offsetY { set; get; }
    public float top { set; get; }
    public float left { set; get; }
    public float allHeight { set; get; }
    public bool hasLibTouchDown { set; get; }
    bool HasHome;
    public bool isLock { set; get; }
    Transform _trans;
    public delegate void TweenBackEv(object state);//0上1下2左3右
    public TweenBackEv tweenBackEv = null;
    bool mIsSendEvent = false;
    public Transform MyTransform
    {
        set
        {
            _trans = value;
        }
        get
        {
            if (_trans == null)
            {
                _trans = transform;
            }
            return _trans;
        }
    }
    float tweenBackTo = float.MinValue, Cookioleft;
    Vector4 ve4;
    public int x { set; get; }
    public UIPanel mPanel { set; get; }
    UISprite Arrow1, Arrow2;
    public bool IsHideWhenOutOfRect = true;
    void OnDestroy()
    {
        photos = null;
        MyTransform = null;
    }
    void Awake()
    {
        top = 0;
        left = 0;
        ve4.x = (clipRangew * 0.5f + photoHeight * 0.5f + 2) * -1;
        ve4.y = clipRangez * 0.5f + photoHeight * 0.5f + 2;
        ve4.z = clipRangew * 0.5f + photoHeight * 0.5f + 2;
        ve4.w = (clipRangez * 0.5f + photoHeight * 0.5f + 2) * -1;
        if (!IsNoCreateCollider)
        {
            GameObject go = new GameObject("Collider");
            go.layer = gameObject.layer;
            BoxCollider boxc = go.AddComponent<BoxCollider>();
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            boxc.center = new Vector3(0, 0, 0.1f);
            boxc.size = new Vector3(clipRangew, clipRangez, 1);
            go.AddComponent<NGUIDragMenuClick>().SetNGUIDragMenu(this);
            go.AddComponent<UISprite>().depth = -1;
        }
        Vector4 Ave4 = new Vector4(0, 0, clipRangew, clipRangez);
        mPanel = GetComponent<UIPanel>();
        if (mPanel != null)
        {
            Ave4 = mPanel.finalClipRegion;
            if (mPanel.clipping != UIDrawCall.Clipping.SoftClip)
            {
                mPanel.clipping = UIDrawCall.Clipping.SoftClip;
            }
        }
        
        //boxc.center = new Vector3(up.baseClipRegion.x,up.baseClipRegion.y,0.1f);
        this.enabled = false;
    }
    public void OpenUpdate(bool OnPress)
    {
        if (!gameObject.activeInHierarchy) return;
        if (!OnPress) return;
        ti = 0;
        lastLeft = left;
        lastTop = top;
        mouseX = touchDownX = touchFollowX = Input.mousePosition.x;
        mouseY = touchDownY = touchFollowY = Input.mousePosition.y;
        if (isLock) return;
        hasLibTouchDown = OnPress;
        HasHome = false;
        if (!this.enabled) this.enabled = true;
        if (PressSendObject != null)
        {
            PressSendObject.SendMessage("OnPress", OnPress);
        }
        if (nGUIDragMenu != null)
        {
            nGUIDragMenu.OpenUpdate(OnPress);
        }
        SetPhotoPotion();
    }
    void OnDisable()
    {
        hasLibTouchDown = false;
        HasHome = true;
        if (this.enabled) this.enabled = false;
    }
    //	void OnEnable () {
    //		Cookioleft = -100;
    //		touchFollowX = mouseX;
    //		touchFollowY = mouseY;
    //		top = left = 0;
    //		tweenBackTo = float.MinValue;
    //	}

    void Update()
    {
        if (isLock) return;
        if (!hasLibTouchDown) return;
        if (Input.GetMouseButtonUp(0))
        {
            HasHome = true;
            mIsSendEvent = false;
        }
        UpdatePositon();
    }
    public void OnBottom(int index)
    {
        if (index < 0)
        {
            return;
        }
        if (index >= photos.Count)
        {
            return;
        }
        if (direction == Direction.Cross)
        {
            if (allWidth <= mouseRect.width)
            {
                return;
            }
            if ((photoWidth + spaceX) * index <= mouseRect.width)
            {
                return;
            }
            left = (photoWidth + spaceX) * (index+1) + mouseRect.width;
        }
        else
        {
            if (allHeight <= mouseRect.height)
            {
                return;
            }
            if ((photoHeight + spaceY) * index <= mouseRect.height)
            {
                return;
            }
            top = (photoHeight + spaceY) * (index+1) - mouseRect.height;
        }
        UpdatePositon();
    }
    public void OnCenter(int index)
    {
        if (Mathf.Abs(index) >= photos.Count)
        {
            return;
        }
        if (direction == Direction.Cross)
        {
            if (allWidth <= mouseRect.width)
            {
                return;
            }
          
            left = (photoWidth + spaceX) * index ;
        }
        else
        {
            if (allHeight <= mouseRect.height)
            {
                return;
            }
            
            top = (photoHeight + spaceY) * index ;
        }
        UpdatePositon();
    }
    public void SetContent(GameObject[] objects)
    {
        if (this == null)
        {
            return;
        }
        this.enabled = false;
        ClearALl();
        StartZero();
        InitData();
        photos.Clear();
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].transform.parent = MyTransform;
            objects[i].transform.localPosition = new Vector3(0, 0, 0);
            objects[i].transform.localScale = new Vector3(1, 1, 1);
            if (!IsNoCreateCollider)
            {
                objects[i].AddComponent<NGUIDragMenuClick>().SetNGUIDragMenu(this);
            }
            UIPanel cUIPanel = objects[i].GetComponent<UIPanel>();
            if (cUIPanel) { Destroy(cUIPanel); }
            photos.Add(objects[i]);
        }
        SetLengthValue(objects.Length);
        SetPhotoPotion();
    }
    void StartYunxing()
    {
        InitData();
        SetLengthValue(photos.Count);
    }
    public T SetContent<T>(T[] objects, bool ItsClear, bool ItsInstar) where T : Component
    {
        if (ItsClear)
        {
            ClearALl();
        }
        StartZero();
        photos.Clear();
        if (ItsInstar)
        {
            InitData();
            for (int i = 0; i < objects.Length; i++)
            {
                int index = photos.Count;
                photos.Add(objects[i].gameObject);
                photos[index].transform.parent = MyTransform;
                photos[index].transform.localPosition = new Vector3(0, 0, 0);
                photos[index].transform.localScale = new Vector3(1, 1, 1);
                if (!IsNoCreateCollider)
                {
                    photos[index].AddMissingComponent<NGUIDragMenuClick>().SetNGUIDragMenu(this);
                }
                UIPanel cUIPanel = photos[index].GetComponent<UIPanel>();
                if (cUIPanel) { Destroy(cUIPanel); }

            }
        }
        SetLengthValue(photos.Count);
        SetPhotoPotion();
        return null;
    }

    public void SetContent(GameObject[] objects, bool ItsClear, bool ItsInstar)
    {
        if (ItsClear)
        {
            ClearALl();
        }
        StartZero();
        InitData();
        for (int i = 0; i < photos.Count; i++)
        {
            if (photos[i] != null)
            {
                if (photos[i].activeInHierarchy)
                {
                    if (IsHideWhenOutOfRect)
                    {
                        photos[i].SetActive(false);
                    }
                }
            }
        }
        photos.Clear();
        if (ItsInstar)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].transform.parent = MyTransform;
                objects[i].transform.localPosition = new Vector3(0, 0, 0);
                objects[i].transform.localScale = new Vector3(1, 1, 1);
                if (!IsNoCreateCollider)
                {
                    objects[i].AddMissingComponent<NGUIDragMenuClick>().SetNGUIDragMenu(this);
                }
                UIPanel cUIPanel = objects[i].GetComponent<UIPanel>();
                if (cUIPanel) { Destroy(cUIPanel); }
                photos.Add(objects[i]);
            }
        }
        SetLengthValue(photos.Count);
        SetPhotoPotion();
    }
    public void SetLengthValue(int Count)
    {
        if (direction == Direction.Cross)
        {
            if (((float)Count / (float)row) % row == 0)
            {
                col = (int)((float)Count / (float)row);
            }
            else
            {
                col = (int)((float)Count / (float)row) + 1;
            }
        }
        else if (direction == Direction.Vertical)
        {
            if (((float)Count / (float)col) % col == 0)
            {
                row = (int)((float)Count / (float)col);
            }
            else
            {
                row = (int)((float)Count / (float)col) + 1;
            }
        }
        allWidth = col * (photoWidth + spaceX);
        xianzhiLeft = -(allWidth - mouseRect.width + spaceX);
        allHeight = row * (photoHeight + spaceY);
        xianzhiTop = -(allHeight - mouseRect.height + spaceY);
    }
    public void InitData()
    {
        if (this == null)
        {
            return;
        }
        mouseRect.x = Screen.width * 0.5f + transform.localPosition.x - clipRangew * 0.5f;
        mouseRect.y = Screen.height * 0.5f + transform.localPosition.y - clipRangez * 0.5f;
        mouseRect.width = clipRangew;
        mouseRect.height = clipRangez;
        offsetX = mouseRect.width * 0.5f - photoWidth * 0.5f - spaceX;
        offsetY = mouseRect.height * 0.5f - photoHeight * 0.5f - spaceY;
    }

    public void StartZero()
    {
        if (Arrow1 != null)
        {
            Arrow1.enabled = false;
        }
        if (Arrow2 != null)
        {
            Arrow2.enabled = false;
        }
        if (this != null)
        {
            this.enabled = false;
        }
        Cookioleft = 0;
        left = 0; touchDownX = 0; lastLeft = 0; touchFollowX = 0; mouseX = 0;
        top = 0; touchDownY = 0; lastTop = 0; touchFollowY = 0; mouseY = 0;
        hasLibTouchDown = false;
    }

    void ClearALl()
    {
        int count = photos.Count;
        //if (count > 0)
        //{
        //    BlacksmithItem b = photos[0].GetComponent<BlacksmithItem>();
        //    if (b != null)
        //    {
        //        if (b.MyPointObjectLight != null)
        //        {
        //            b.MyPointObjectLight.transform.parent = b.transform.parent;
        //        }
        //    }
        //}
        for (int i = 0; i < count; i++)
        {
            Destroy(photos[i]);
        }
        photos.Clear();
    }

    public void SetAlpha(float alpha)
    {
        GetComponent<UIPanel>().alpha = alpha;
    }

    public void Lock(bool b)
    {
        isLock = b;
    }
    public void UpdatePositon()
    {
        if (direction == Direction.Cross)
        {
           // mPanel.clipSoftness = new Vector2(30, 4);
            if (allWidth <= mouseRect.width)
            {
                this.enabled = false;
                return;
            }
            if (HasHome)
            {
                if (tweenBackTo != float.MinValue)
                {
                    left += (tweenBackTo - left) * 0.5f;
                    if (Mathf.Abs(left - tweenBackTo) < 0.1f)
                    {
                        left = tweenBackTo;
                        tweenBackTo = float.MinValue;
                    }
                }
            }
            if (Mathf.Abs(Cookioleft - left) > 0.5f)
            {
                SetPhotoPotion();
                Cookioleft = left;
            }
            else
            {
                if (HasHome)
                {
                    //if (ti > 0.3f)
                    //{
                    //    mPanel.clipSoftness = new Vector2(4, 4);
                    //}
                    if (ti > 2f)
                    {
                        this.enabled = false;
                        hasLibTouchDown = false;
                    }
                    else
                    {
                        ti += Time.deltaTime;
                    }
                }
            }
            if (allWidth > mouseRect.width)
            {
                if (!HasHome)
                {
                    //mPanel.clipSoftness = new Vector2(30, 4);
                    mouseX = Input.mousePosition.x;
                    left = lastLeft + (mouseX - touchDownX);
                }
                else
                {
                    left += mouseX - touchFollowX;
                }
                touchFollowX += (mouseX - touchFollowX) * 0.3f;
                if (left > 0)
                {
                    tweenBackTo = 0f;
                }
                else if (left < xianzhiLeft)
                {
                    tweenBackTo = xianzhiLeft;
                }
                else
                {
                    if (tweenBackTo != float.MinValue)
                    {
                        tweenBackTo = float.MinValue;
                    }
                }
            }
        }
        else if (direction == Direction.Vertical)
        {
          //  mPanel.clipSoftness = new Vector2(4, 30);
            if (allHeight <= mouseRect.height)
            {
                this.enabled = false;
                
                return;
            }
            
            if (HasHome)
            {
                
                if (tweenBackTo != float.MinValue)
                {
                    if (!mIsSendEvent && tweenBackEv != null)
                    {
                        mIsSendEvent = true;
                        int state = tweenBackTo - top > 0 ? 0 : 1;
                        tweenBackEv(state);
                    }
                    top += (tweenBackTo - top) * 0.5f;
                    if (Mathf.Abs(top - tweenBackTo) < 0.1f)
                    {
                        top = tweenBackTo;
                        tweenBackTo = float.MinValue;
                    }
                }
            }
            if (Mathf.Abs(Cookioleft - top) > 0.5f)
            {
                SetPhotoPotion();
                Cookioleft = top;
            }
            else
            {
                if (HasHome)
                {
                    //if (ti > 0.3f)
                    //{
                    //    mPanel.clipSoftness = new Vector2(4, 4);
                    //}
                    if (ti > 2f)
                    {
                        this.enabled = false;
                        hasLibTouchDown = false;
                    }
                    else
                    {
                        ti += Time.deltaTime;
                    }
                }
            }
            if (allHeight > mouseRect.height)
            {
                if (!HasHome)//while draging...
                {
                   // mPanel.clipSoftness = new Vector2(4, 30);
                    mouseY = Input.mousePosition.y;
                    top = lastTop + (mouseY - touchDownY);
                }
                else
                {
                    top += mouseY - touchFollowY;
                }
                touchFollowY += (mouseY - touchFollowY) * 0.3f;
                if (-top > 0)
                {
                    tweenBackTo = 0f;
                }
                else if (-top < xianzhiTop)
                {
                    tweenBackTo = -xianzhiTop;
                }
                else
                {
                    if (tweenBackTo != float.MinValue)
                    {
                        tweenBackTo = float.MinValue;
                    }
                }
            }
        }
    }
    [HideInInspector]
    public float test1 = 0, test2 = 0;
    public void SetPhotoPotion()
    {
        x = 0;
        if (direction == Direction.Cross)
        {
            for (int c = 0; c < col; c++)
            {
                for (int r = 0; r < row; r++)
                {
                    if (x < photos.Count)
                    {
                        if (photos[x] != null)
                        {
                            Vector3 ve3 = new Vector3(c * (photoWidth + spaceX) + left - offsetX, offsetY - r * (photoHeight + spaceY) + top, 0);
                            photos[x].transform.localPosition = ve3;
                            if (ve3.x > ve4.z || ve3.x < ve4.x)
                            {
                                if (IsHideWhenOutOfRect)
                                {
                                    if (photos[x].gameObject.activeInHierarchy) photos[x].gameObject.SetActive(false);
                                }
                                if (c == col - 1)
                                {
                                    if (Arrow2 != null && !Arrow2.enabled)
                                    {
                                        Arrow2.enabled = true;
                                    }
                                }
                                if (c == 0)
                                {
                                    if (Arrow1 != null && !Arrow1.enabled)
                                    {
                                        Arrow1.enabled = true;
                                    }
                                }
                            }
                            else
                            {
                                if (c == col - 1)
                                {
                                    if (Arrow2 != null && Arrow2.enabled)
                                    {
                                        Arrow2.enabled = false;
                                    }
                                }
                                if (c == 0)
                                {
                                    if (Arrow1 != null && Arrow1.enabled)
                                    {
                                        Arrow1.enabled = false;
                                    }
                                }
                                if (!photos[x].gameObject.activeInHierarchy)
                                {
                                    photos[x].gameObject.SetActive(true);
                                }
                            }
                        }
                        x++;
                    }
                }
            }
        }
        else if (direction == Direction.Vertical)
        {
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    if (x < photos.Count)
                    {
                        if (photos[x] != null)
                        {
                            Vector3 ve3 = new Vector3(c * (photoWidth + spaceX) + left - offsetX, offsetY - r * (photoHeight + spaceY) + top, 0);
                            test1 = ve3.y;
                            test2 = r;
                            photos[x].transform.localPosition = ve3;
                            if (ve3.y > ve4.y || ve3.y < ve4.w)
                            {
                                if (IsHideWhenOutOfRect)
                                {
                                    if (photos[x].gameObject.activeInHierarchy) photos[x].gameObject.SetActive(false);
                                }
                                if (x == photos.Count - 1)
                                {
                                    if (Arrow2 != null && !Arrow2.enabled)
                                    {
                                        Arrow2.enabled = true;
                                    }
                                }
                                if (r == 0)
                                {
                                    if (Arrow1 != null && !Arrow1.enabled)
                                    {
                                        Arrow1.enabled = true;
                                    }
                                }
                            }
                            else
                            {
                                if (x == photos.Count - 1)
                                {
                                    if (Arrow2 != null && Arrow2.enabled)
                                    {
                                        Arrow2.enabled = false;
                                    }
                                }
                                if (r == 0)
                                {
                                    if (Arrow1 != null && Arrow1.enabled)
                                    {
                                        Arrow1.enabled = false;
                                    }
                                }
                                if (!photos[x].gameObject.activeInHierarchy)
                                {
                                    photos[x].gameObject.SetActive(true);
                                }
                            }
                        }
                        x++;
                    }
                }
            }
        }
    }
    public void AddElement(List<GameObject> gos)
    {
        if (gos == null || gos.Count == 0)
        {
            return;
        }
        for (int i = 0; i < gos.Count; i++)
        {
            gos[i].transform.parent = MyTransform;
            gos[i].transform.localPosition = new Vector3(0, 0, 0);
            gos[i].transform.localScale = new Vector3(1, 1, 1);
            if (!IsNoCreateCollider)
            {
                gos[i].AddMissingComponent<NGUIDragMenuClick>().SetNGUIDragMenu(this);
            }
            UIPanel cUIPanel = gos[i].GetComponent<UIPanel>();
            if (cUIPanel) { Destroy(cUIPanel); }
            photos.Add(gos[i]);
        }
        SetLengthValue(photos.Count);
        SetPhotoPotion();
    }
    public void AddElement(GameObject go)
    {
        photos.Add(go);
        go.transform.parent = MyTransform;
        go.transform.localPosition = new Vector3(0, 0, 0);
        go.transform.localScale = new Vector3(1, 1, 1);
        if (!IsNoCreateCollider)
        {
            go.AddMissingComponent<NGUIDragMenuClick>().SetNGUIDragMenu(this);
        }
        UIPanel cUIPanel = go.GetComponent<UIPanel>();
        if (cUIPanel) { Destroy(cUIPanel); }
        SetLengthValue(photos.Count);
        SetPhotoPotion();
    }
    public void RemoveGameObject(List<GameObject> gos, bool isDestroy)
    {
        this.enabled = false;
        for (int i = 0; i < gos.Count; i++)
        {
            for (int j = 0; j < photos.Count; j++)
            {
                if (gos[i] == photos[j])
                {
                    if (photos[j] != null)
                    {
                        if (isDestroy)
                        {
                            Destroy(photos[j]);
                        }
                        else
                        {
                            photos[j].SetActive(false);
                        }
                    }
                    photos.RemoveAt(j);
                    j--;
                }
            }
        }
        SetLengthValue(photos.Count);
        if (direction == Direction.Vertical)
        {
            if (allHeight <= mouseRect.height)
            {
                StartZero();
            }
        }
        else if (direction == Direction.Cross)
        {
            if (allWidth <= mouseRect.width)
            {
                StartZero();
            }
        }
        SetPhotoPotion();
    }
    public void RemoveGameObject(GameObject gos, bool isDestroy)
    {
        this.enabled = false;
        for (int j = 0; j < photos.Count; j++)
        {
            if (gos == photos[j])
            {
                if (photos[j] != null)
                {
                    if (isDestroy)
                    {
                        Destroy(photos[j]);
                    }
                    else
                    {
                        photos[j].SetActive(false);
                    }
                }
                photos.RemoveAt(j);
                j--;
                break;
            }
        }
        SetLengthValue(photos.Count);
        if (direction == Direction.Vertical)
        {
            if (allHeight <= mouseRect.height)
            {
                StartZero();
            }
        }
        else if (direction == Direction.Cross)
        {
            if (allWidth <= mouseRect.width)
            {
                StartZero();
            }
        }
        SetPhotoPotion();
    }
    public void RemoveObject(int ind, bool isDestroy = false)//移除指定对象
    {
        if (ind > photos.Count - 1 || ind < 0)
            return;
        GameObject go = photos[ind];
        photos.RemoveAt(ind);
        if (isDestroy)
        {
            Destroy(go);
        }
        UpdatePositon();
    }
    void SwapPosition(GameObject pos1, GameObject pos2)
    {
        GameObject go = pos1;
        Vector3 pos = pos1.transform.localPosition;
        pos1.transform.localPosition = pos2.transform.localPosition;
        pos2.transform.localPosition = pos;
        pos1 = pos2;
        pos2 = go;
    }
    [ContextMenu("Execute")]
    public void Reposition()
    {
        StartZero();
        InitData();
        for (int i = 0; i < photos.Count; i++)
        {
            photos[i].transform.parent = MyTransform;
            photos[i].transform.localPosition = new Vector3(0, 0, 0);
            photos[i].transform.localScale = new Vector3(1, 1, 1);
            UIPanel cUIPanel = photos[i].GetComponent<UIPanel>();
            if (cUIPanel) { Destroy(cUIPanel); }
        }
        SetLengthValue(photos.Count);
        SetPhotoPotion();
    }
}
