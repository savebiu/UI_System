using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;//������:


//�洢���ݵ�����Json���ļ���
public class PackageLocalData
{
    //����
    private static PackageLocalData _instance;

    //���õ���ģʽ��Ϊȫ�ֱ����Թ����������
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

    //���������б�localDataList
    public List<PackageLocalItem> localDataList;


    //�洢����
    //1.���л�
    //2.�洢
    //3.����
    public void SaveData()
    {
        string json = JsonUtility.ToJson(this);//JsonUtility�����Ϣ���л�Ϊ�ַ���
        PlayerPrefs.SetString("PackageLocalData", json);//PlayerPrefs��Ϸ�浵,�����ݴ洢��ע��� SetString ������д�뵽����
        PlayerPrefs.Save();
    }

    //��ȡ����
    //1.�жϻ��������Ƿ����
    //2.��ȡ
    //3.�����л�
    //4.�ر�
    public List<PackageLocalItem> LocalPackage() 
    {
        //�л���
        if(localDataList != null)
        {
            return localDataList;
        }

        //�޻���,�������ļ��ж�ȡ
        if (PlayerPrefs.HasKey("PackageLocalData"))
        {
            string inventoryJson = PlayerPrefs.GetString("PackageLocalData");//GetString ��ȡ��������
            //�����л�
            PackageLocalData packageLocalData = JsonUtility.FromJson<PackageLocalData>(inventoryJson);
            //���
            localDataList = packageLocalData.localDataList;
            return localDataList;
        }
        //�Ҳ��������ļ��򴴽����б�
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
