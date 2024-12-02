using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;//作用是:


//存储数据到本地Json送文件中
public class PackageLocalData
{
    //单例
    private static PackageLocalData _instance;

    //采用单例模式作为全局变量以供其他类访问
    public static PackageLocalData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PackageLocalData();
            }
            return _instance;

        }
    }

    //创建数据列表localDataList
    public List<PackageLocalItem> localDataList;


    //存储数据
    //1.序列化
    //2.存储
    //3.保存
    public void SaveData()
    {
        string json = JsonUtility.ToJson(this);//JsonUtility表格信息序列化为字符串
        PlayerPrefs.SetString("PackageLocalData", json);//PlayerPrefs游戏存档,讲数据存储到注册表 SetString 将数据写入到本地
        PlayerPrefs.Save();
    }

    //读取数据
    //1.判断缓存数据是否存在
    //2.读取
    //3.反序列化
    //4.关闭
    public List<PackageLocalItem> LocalPackage() 
    {
        //有缓存
        if(localDataList != null)
        {
            return localDataList;
        }

        //无缓存,到本地文件中读取
        if (PlayerPrefs.HasKey("PackageLocalData"))
        {
            string inventoryJson = PlayerPrefs.GetString("PackageLocalData");//GetString 读取本地数据
            //反序列化
            PackageLocalData packageLocalData = JsonUtility.FromJson<PackageLocalData>(inventoryJson);
            //输出
            localDataList = packageLocalData.localDataList;
            return localDataList;
        }
        //找不到本地文件则创建新列表
        else
        {
            localDataList = new List<PackageLocalItem>();
            return localDataList;
        }
    }

}


[System.Serializable]
public class PackageLocalItem
{
    public string uid;
    public int id;
    public string name;
    public int count;

   public override string ToString()
    {
        return string.Format("[Id]:{0}, [Name]:{1}, [Count]:{2}", id, name, count);
    }
}
