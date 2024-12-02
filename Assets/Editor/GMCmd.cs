using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class GMCmd
{
    //�����˵���
    [MenuItem("GMCmd/��ȡ���")]
    public static void ReadTable()
    {
        // ��ȡ�ļ�
        PackageTable packageTable = Resources.Load<PackageTable>("TableData/PackageTable");//������Resources�ļ����´��TableData/PackageTable
        foreach (PackageItem packageItem in packageTable.DataList)
        {
            Debug.Log(string.Format("[Id]:{0}, [Name]:{1}, [Decriptsion]:{2}", packageItem.id, packageItem.name, packageItem.description));
        }
    }

    [MenuItem("GMCmd/��̬����������������")]
    public static void CreateLocalPackageData()
    {
        PackageLocalData.Instance.localDataList =  new List<PackageLocalItem>();
        for (int i = 0; i < 10; i++)
        {
            PackageLocalItem packageLocalItem = new()
            {
                uid = Guid.NewGuid().ToString(),
                id = i,
                name = "��Ʒ" + i,
                count = i
            };
            PackageLocalData.Instance.localDataList.Add(packageLocalItem);
        }
        PackageLocalData.Instance.SaveData();

        //��ȡ����
        List<PackageLocalItem> readItems= PackageLocalData.Instance.LocalPackage();
        foreach (PackageLocalItem item in readItems)
        {
            Debug.Log(item);
        }
    }
    [MenuItem("GMCmd/��ȡ������������")]
    public static void ReadLocalPackageData()
    {
        List<PackageLocalItem> readItems = PackageLocalData.Instance.LocalPackage();
        foreach (PackageLocalItem item in readItems)
        {
            Debug.Log(item);
        }
    }
}
