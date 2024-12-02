using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//菜单 （菜单名， 文件名）
[CreateAssetMenu(menuName ="护符/PackageTable", fileName = "PackageTable")]
public class PackageTable : ScriptableObject
{
    public List<PackageItem> DataList = new List<PackageItem>();
}

[System.Serializable]
public class PackageItem
{
    public int id;
    public string name;
    public int type;
    public string description;
    public string skillDecription;
    public int count;
}

