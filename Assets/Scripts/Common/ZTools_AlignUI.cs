using UnityEngine;
using System.Collections;

public class ZTools_AlignUI : MonoBehaviour
{

    UISprite mRefSpr = null;
    UITexture mRefTexture = null;
    bool isRemoveThisScript = false;
    public Transform tTrans;
    Vector3 mTargetPos = new Vector3 (0, 0, 0);
    float mRefW, mRefH;
    void Awake ()
    {
        if (isRemoveThisScript)
            Destroy (this);
    }
    [ContextMenu ("AlignPosition")]
    public void AlignPosition ()
    {
        mRefSpr = GetComponent<UISprite>();
        mRefTexture = GetComponent<UITexture> ();
        if (mRefSpr != null)
        {
            Debug.Log ("Ref object is UISprite:");
            mRefW = mRefSpr.width;
            mRefH = mRefSpr.height;
            if (tTrans != null)
            {
                mTargetPos = tTrans.localPosition;
                tTrans.localPosition = new Vector3 (mTargetPos.x - 0.5f*mRefW,0.5f*mRefH - mTargetPos.y,mTargetPos.z);
            }
        }
        if (mRefTexture != null)
        {
            Debug.Log ("Ref object is UITexture:");
            mRefW = mRefTexture.width;
            mRefH = mRefTexture.height;
            if (tTrans != null)
            {
                mTargetPos = tTrans.localPosition;
                tTrans.localPosition = new Vector3 (mTargetPos.x - 0.5f * mRefW, 0.5f * mRefH - mTargetPos.y, mTargetPos.z);
            }
        }
        if (mRefTexture == null && mRefSpr == null)
        {
            Debug.Log ("Can not find a Ref object:");
        }
    }
}
