using UnityEngine;
using System.Collections;

public class SystemVolume : MonoBehaviour
{
    public UISlider slider;
    bool flag = false;
    void Start()
    {
        slider.value = Global.instance.Volume;
        this.gameObject.SetActive(false);
    }
    void OnEnable()
    {
        
    }
    public void Open()
    {
        gameObject.SetActive(true);
        //Invoke("Close", 1.5f);
    }
   public void Close()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {

        if(!flag)
        {
            if(Global.instance.Volume != slider.value)
            {
                slider.value = Global.instance.Volume;
            }
        }
    }
    public void OnValueChange()
    {
        flag = true;
        Global.instance.Volume = slider.value;
        //CancelInvoke();
        //Invoke("Close", 1.0f);
    }
}
