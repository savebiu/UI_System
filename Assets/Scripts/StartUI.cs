using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

//需要导入UI包
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class StartUI : MonoBehaviour
{   
    //文本
    public Text text;
    public Rect rect;

    //图片
    public Image image;
    float targetamount = 1;

    //Toggle
    public Toggle toggle;
    public Text text1;
    public Text text2;

    //Canvas切换
    //初始界面    
    public GameObject targetCanvas;

    

    void Start()
    {
        Text();
        Toggle();       
    }

    private void Update()
    {
        Image();

    }

    //文本
    public void Text()
    {
        //Text
        text.text = "空洞骑士";
        text.fontSize = 162;
        text.color = Color.white;
        text.alignment = TextAnchor.MiddleCenter;
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
        text.verticalOverflow = VerticalWrapMode.Overflow;
    }

    //图片
    public void Image()
    {
        if (image.fillAmount != 1)
        {
            image.fillAmount = Mathf.Lerp(image.fillAmount, targetamount, Time.deltaTime);
        }    
    }

    //开始按钮
    public void OnClick()
    {
        FindObjectOfType<UIManager>().SwitchCanvas(targetCanvas);
    }

    //设置按钮
    public void Setting()
    {
        Debug.Log("Setting");
    }

    //退出按钮
    public void Quit()
    {
        Debug.Log("Quit");
    }

    //Toggle切换
    public void Toggle()
    {
        
        if (toggle.isOn)
        {
            text1.gameObject.SetActive(false);
            text2.gameObject.SetActive(true);
        }
        else 
        {
            text1.gameObject.SetActive(true);
            text2.gameObject.SetActive(false);
        }
    }   
}
