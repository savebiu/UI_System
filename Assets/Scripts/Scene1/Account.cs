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
    //文件夹路径
    private string userFilePath;

    public GameObject targetCanvas;
    //初始化对象
    private List<User> users = new List<User>();

    private void Awake()
    {
        //设置文件路径
        //Combine合并 Application.persistentDataPath持久数据目录路径(只读)，保存位置为C:\Users\用户名\AppData\LocalLow\公司名\项目名
        //Application.dataPath指向当前文件夹
        userFilePath = Path.Combine(Application.dataPath, "UserFile.json");
    }

    //用户数据结构
    [Serializable]      //序列化对象,JsonUtility 不支持直接反序列化包含 List 的嵌套类,所以需要进行序列化标记才能正确写入文件
    public class User
    {
        public string Username;
        public string Password;
    }

    //包装用户数据
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
            string json = File.ReadAllText(userFilePath);//读取所有文本内容
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
            //文件不存在则创建空文件
            SaveUsers();
        }
    }

    

    //登录按钮
    public void OnLogin()
    {
        //检查用户信息
        var user = users.Find(u => u.Username == username.text);
        if(user != null)
        {
            if(user.Password == password.text)
            {
                Debug.Log("登陆成功");
                FindAnyObjectByType<UIManager>().SwitchCanvas(targetCanvas);
            }
            else
            {
                Debug.Log("密码错误");
            }
        }
        else
        {
            Debug.Log("该账号为空");
        }
    }
    //注册按钮
    public void OnRegister()
    {
        //检查用户信息
        if(users.Exists(u =>u.Username == username.text))
        {
            Debug.Log("该账号已存在");
            return;
        }
        else
        {
            //将注册信息添加到列表
            users.Add(new User { Username = username.text, Password = password.text });
            SaveUsers();//保存
            Debug.Log("注册成功");

            // 打印用户列表，确认数据已添加
            foreach (var user in users)
            {
                Debug.Log($"Registered User: {user.Username}, {user.Password}");
            }
        }
    }

    //保存用户信息
    private void SaveUsers()
    {
        Debug.Log("路径" + userFilePath);

        // 检查文件夹路径是否存在，不存在则创建
        string directoryPath = Path.GetDirectoryName(userFilePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var userWrapper = new UserWrapper { users = users };
        string json = JsonUtility.ToJson(userWrapper, true);
        // 输出保存的 JSON 数据以进行调试
        Debug.Log("Saving JSON: " + json);

        try
        {
            File.WriteAllText(userFilePath, json);
            Debug.Log("写入路径 " + userFilePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save user data to " + e.Message);
        }
    }
}
