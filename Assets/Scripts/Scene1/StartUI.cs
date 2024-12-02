using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

//��Ҫ����UI��
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class StartUI : MonoBehaviour
{   
    //�ı�
    public Text text;
    public Rect rect;

    //ͼƬ
    public Image image;
    float targetamount = 1;

    //Toggle
    public Toggle toggle;
    public Text text1;
    public Text text2;

    //Canvas�л�
    //��ʼ����    
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

    //�ı�
    public void Text()
    {
        //Text
        text.text = "�ն���ʿ";
        text.fontSize = 162;
        text.color = Color.white;
        text.alignment = TextAnchor.MiddleCenter;
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
        text.verticalOverflow = VerticalWrapMode.Overflow;
    }

    //ͼƬ
    public void Image()
    {
        if (image.fillAmount != 1)
        {
            image.fillAmount = Mathf.Lerp(image.fillAmount, targetamount, Time.deltaTime);
        }    
    }

    //��ʼ��ť
    public void OnClick()
    {
        FindObjectOfType<UIManager>().SwitchCanvas(targetCanvas);
    }

    //���ð�ť
    public void Setting()
    {
        Debug.Log("Setting");
    }

    //�˳���ť
    public void Quit()
    {
        Debug.Log("Quit");
    }

    //Toggle�л�
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
