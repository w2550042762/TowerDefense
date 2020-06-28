using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject towerGo;//保存当前cube身上的炮台
    [HideInInspector]
    public TowerData towerData;
    [HideInInspector]
    public bool isUP = false;

    public GameObject buildEffect;

    //private Renderer renderer;

    
    void Start()
    {
        //renderer = GetComponent<Renderer>();
    }

    public void BuildTower(TowerData towerData)
    {
        //Vector3 v3 = new Vector3(transform.position.x,transform.position.y,-0.51f);
        this.towerData = towerData;
        isUP = false;
        towerGo = GameObject.Instantiate(towerData.towerPrefab, transform.position, Quaternion.identity);
        towerGo.transform.SetParent(transform,false);
        towerGo.transform.position = new Vector3(transform.position.x, transform.position.y, 88);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }

    public void UPTower()
    {
        if (isUP == true) return;

        Destroy(towerGo);
        isUP = true;
        towerGo = GameObject.Instantiate(towerData.towerUPPrefab, transform.position, Quaternion.identity);
        towerGo.transform.SetParent(transform, false);
        towerGo.transform.position = new Vector3(transform.position.x, transform.position.y, 88);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }

    public void DestroyTower()
    {
        Destroy(towerGo);
        isUP = false;
        towerGo = null;
        towerData = null;
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }

    //void OnMouseEnter()
    //{

    //    if (towerGo == null && EventSystem.current.IsPointerOverGameObject() == false)
    //    {
    //        renderer.material.color = Color.red;
    //    }
    //}
    //void OnMouseExit()
    //{
    //    renderer.material.color = Color.yellow;
    //}
}
