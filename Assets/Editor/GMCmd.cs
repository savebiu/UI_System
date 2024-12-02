using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class GMCmd
{
    //创建菜单栏
    [MenuItem("GMCmd/读取表格")]
    public static void ReadTable()
    {
        // 读取文件
        PackageTable packageTable = Resources.Load<PackageTable>("TableData/PackageTable");//必须在Resources文件夹下存放TableData/PackageTable
        foreach (PackageItem packageItem in packageTable.DataList)
        {
            Debug.Log(string.Format("[Id]:{0}, [Name]:{1}, [Decriptsion]:{2}", packageItem.id, packageItem.name, packageItem.description));
        }
    }

    [MenuItem("GMCmd/动态创建背包测试数据")]
    public static void CreateLocalPackageData()
    {
        PackageLocalData.Instance.localDataList =  new List<PackageLocalItem>();
        for (int i = 0; i < 10; i++)
        {
            PackageLocalItem packageLocalItem = new()
            {
                uid = Guid.NewGuid().ToString(),
                id = i,
                name = "物品" + i,
                count = i
            };
            PackageLocalData.Instance.localDataList.Add(packageLocalItem);
        }
        PackageLocalData.Instance.SaveData();

        //读取数据
        List<PackageLocalItem> readItems= PackageLocalData.Instance.LocalPackage();
        foreach (PackageLocalItem item in readItems)
        {
            Debug.Log(item);
        }
    }
    [MenuItem("GMCmd/读取背包测试数据")]
    public static void ReadLocalPackageData()
    {
        List<PackageLocalItem> readItems = PackageLocalData.Instance.LocalPackage();
        foreach (PackageLocalItem item in readItems)
        {
            Debug.Log(item);
        }
    }
}
