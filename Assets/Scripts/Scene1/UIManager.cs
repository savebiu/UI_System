using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Stack<GameObject> canvasStack = new Stack<GameObject>();
    public GameObject initCanvas;

    void Start()
    {
        //禁用所有canvas
        GameObject[] allCanvas = GameObject.FindGameObjectsWithTag("Canvas");
        foreach(GameObject canvas in allCanvas)
        {
            canvas.SetActive(false);
        }
        //激活initCanvas
        if (initCanvas != null)
        {
            canvasStack.Push(initCanvas);
            initCanvas.SetActive(true);
        }
    }

    void Update()
    {
        //使用栈关系,当按下ESC按钮时,返回上一个画布
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToPreviousCanvas();
        }
    }

    //画布切换
    public void SwitchCanvas(GameObject newCanvas)
    {
        //隐藏当前画布
        if (canvasStack.Count > 0)
        {
            canvasStack.Peek().SetActive(false);
        }
        // 显示新界面并压入栈中
        canvasStack.Push(newCanvas);
        newCanvas.SetActive(true);
    }

    //返回上一个界面
    public void ReturnToPreviousCanvas()
    {
        Debug.Log("触发Return");
        if (canvasStack.Count > 1)        // 确保至少有一个界面保留
        {
            // 隐藏当前界面并从栈中移除
            GameObject currentCanvas = canvasStack.Pop();
            currentCanvas.SetActive(false);

            // 显示上一个界面
            canvasStack.Peek().SetActive(true);
        }
    }
}
