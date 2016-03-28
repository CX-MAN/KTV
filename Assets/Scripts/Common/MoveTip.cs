using UnityEngine;
using System.Collections;

public class MoveTip : MonoBehaviour
{
    public static int instCount = 0;
    public static MoveTip current = null;
    public UILabel tip;
    bool run = false;
    public float speed = 0;
    Vector3 endPosition;
    void Start()
    {
        current = this;
    }
    public void SetTip(string str)
    {
        tip.text = str;
    }
    public void StartRun(Vector3 endPos)
    {
        endPosition = endPos;
        run = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(run)
        {
            this.transform.localPosition += new Vector3(speed*Time.deltaTime, 0, 0);
        }
        if(transform.localPosition.x <= endPosition.x)
        {
            Destroy(gameObject);
        }
    }
    void OnDestroy()
    {
        instCount--;
    }
}
