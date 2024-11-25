using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Account : MonoBehaviour
{
    public InputField username;
    public InputField password;
    //�ļ���·��
    private string userFilePath;

    public GameObject targetCanvas;
    //��ʼ������
    private List<User> users = new List<User>();

    private void Awake()
    {
        //�����ļ�·��
        //Combine�ϲ� Application.persistentDataPath�־�����Ŀ¼·��(ֻ��)������λ��ΪC:\Users\�û���\AppData\LocalLow\��˾��\��Ŀ��
        //Application.dataPathָ��ǰ�ļ���
        userFilePath = Path.Combine(Application.dataPath, "UserFile.json");
    }

    //�û����ݽṹ
    [Serializable]      //���л�����,JsonUtility ��֧��ֱ�ӷ����л����� List ��Ƕ����,������Ҫ�������л���ǲ�����ȷд���ļ�
    public class User
    {
        public string Username;
        public string Password;
    }

    //��װ�û�����
    [Serializable]
    private class UserWrapper
    {
        public List<User> users = new List<User>();
    }


    private void Start()
    {
        LoadUser();
    }

    public void LoadUser()
    {
        if (File.Exists(userFilePath))
        {
            string json = File.ReadAllText(userFilePath);//��ȡ�����ı�����
            var userWrapper = JsonUtility.FromJson<UserWrapper>(json);
            if(userWrapper != null)
            {
                users = userWrapper.users;
                Debug.Log($"Loaded {users.Count} users.");
            }
            else
            {
                Debug.LogWarning("Failed to parse JSON. Creating a new list.");
                users = new List<User>();
            }
        }
        else
        {
            //�ļ��������򴴽����ļ�
            SaveUsers();
        }
    }

    

    //��¼��ť
    public void OnLogin()
    {
        //����û���Ϣ
        var user = users.Find(u => u.Username == username.text);
        if(user != null)
        {
            if(user.Password == password.text)
            {
                Debug.Log("��½�ɹ�");
                FindAnyObjectByType<UIManager>().SwitchCanvas(targetCanvas);
            }
            else
            {
                Debug.Log("�������");
            }
        }
        else
        {
            Debug.Log("���˺�Ϊ��");
        }
    }
    //ע�ᰴť
    public void OnRegister()
    {
        //����û���Ϣ
        if(users.Exists(u =>u.Username == username.text))
        {
            Debug.Log("���˺��Ѵ���");
            return;
        }
        else
        {
            //��ע����Ϣ��ӵ��б�
            users.Add(new User { Username = username.text, Password = password.text });
            SaveUsers();//����
            Debug.Log("ע��ɹ�");

            // ��ӡ�û��б�ȷ�����������
            foreach (var user in users)
            {
                Debug.Log($"Registered User: {user.Username}, {user.Password}");
            }
        }
    }

    //�����û���Ϣ
    private void SaveUsers()
    {
        Debug.Log("·��" + userFilePath);

        // ����ļ���·���Ƿ���ڣ��������򴴽�
        string directoryPath = Path.GetDirectoryName(userFilePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var userWrapper = new UserWrapper { users = users };
        string json = JsonUtility.ToJson(userWrapper, true);
        // �������� JSON �����Խ��е���
        Debug.Log("Saving JSON: " + json);

        try
        {
            File.WriteAllText(userFilePath, json);
            Debug.Log("д��·�� " + userFilePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save user data to " + e.Message);
        }
    }
}
