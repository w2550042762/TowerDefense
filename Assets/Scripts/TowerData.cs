using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//炮台的数据类
public class TowerData
{
    public GameObject towerPrefab;
    public int cost;
    public GameObject towerUPPrefab;
    public int costUP;
    public TowerType type;
    //public int costDestory;

}
public enum TowerType
{
    Tower1,
    Tower2,
    Tower3
}