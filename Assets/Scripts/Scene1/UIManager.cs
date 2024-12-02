using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Stack<GameObject> canvasStack = new Stack<GameObject>();
    public GameObject initCanvas;

    void Start()
    {
        //��������canvas
        GameObject[] allCanvas = GameObject.FindGameObjectsWithTag("Canvas");
        foreach(GameObject canvas in allCanvas)
        {
            canvas.SetActive(false);
        }
        //����initCanvas
        if (initCanvas != null)
        {
            canvasStack.Push(initCanvas);
            initCanvas.SetActive(true);
        }
    }

    void Update()
    {
        //ʹ��ջ��ϵ,������ESC��ťʱ,������һ������
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToPreviousCanvas();
        }
    }

    //�����л�
    public void SwitchCanvas(GameObject newCanvas)
    {
        //���ص�ǰ����
        if (canvasStack.Count > 0)
        {
            canvasStack.Peek().SetActive(false);
        }
        // ��ʾ�½��沢ѹ��ջ��
        canvasStack.Push(newCanvas);
        newCanvas.SetActive(true);
    }

    //������һ������
    public void ReturnToPreviousCanvas()
    {
        Debug.Log("����Return");
        if (canvasStack.Count > 1)        // ȷ��������һ�����汣��
        {
            // ���ص�ǰ���沢��ջ���Ƴ�
            GameObject currentCanvas = canvasStack.Pop();
            currentCanvas.SetActive(false);

            // ��ʾ��һ������
            canvasStack.Peek().SetActive(true);
        }
    }
}
