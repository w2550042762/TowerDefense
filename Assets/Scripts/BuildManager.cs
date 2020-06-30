using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static int blood = 8;

    public TowerData Tower1Data;
    public TowerData Tower2Data;
    public TowerData Tower3Data;

    //表示当前选择的炮台(要建造的炮台)
    private TowerData selectedTowerData;
    //表示当前选择的炮台(场景中的游戏物体)
    private MapCube selectedMapCube;

    public Text moneyText;

    public Animator moneyAnimator;

    public int money = 1000;

    public GameObject UPCanvas;

    private Animator UPCanvasAnimator;

    public Button buttonUP;

    void ChangeMoney(int change = 0)
    {
        money += change;
        moneyText.text = "￥" + money;
    }

    void Start()
    {
        UPCanvasAnimator = UPCanvas.GetComponent<Animator>();
        blood = 8;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("11");
            //if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                //Debug.Log("22");
                //开发炮台的建造
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //Debug.Log(ray);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (selectedTowerData != null && mapCube.towerGo == null)
                    {
                        //可以创建 
                        if (money >= selectedTowerData.cost)
                        {
                            ChangeMoney(-selectedTowerData.cost);
                            mapCube.BuildTower(selectedTowerData);
                            //Debug.Log("33");
                        }
                        else
                        {
                            //提示钱不够
                            moneyAnimator.SetTrigger("Flicker");
                        }
                    }
                    else if (mapCube.towerGo != null)
                    {

                        // 升级处理
                        if (mapCube == selectedMapCube && UPCanvas.activeInHierarchy)
                        {
                            StartCoroutine(HideUPUI());
                        }
                        else
                        {
                            ShowUPUI(mapCube.transform.position, mapCube.isUP);
                        }
                        selectedMapCube = mapCube;
                    }

                }
            }
        }
    }

    public void OnTower1Selected(bool isOn)
    {
        if (isOn)
        {
            selectedTowerData = Tower1Data;
        }
    }

    public void OnTower2Selected(bool isOn)
    {
        if (isOn)
        {
            selectedTowerData = Tower2Data;
        }
    }

    public void OnTower3Selected(bool isOn)
    {
        if (isOn)
        {
            selectedTowerData = Tower3Data;
        }
    }


    void ShowUPUI(Vector3 pos, bool isDisableUP = false)
    {
        StopCoroutine("HideUPUI");
        UPCanvas.SetActive(false);
        UPCanvas.SetActive(true);
        UPCanvas.transform.position = pos;
        buttonUP.interactable = !isDisableUP;
    }

    IEnumerator HideUPUI()
    {
        UPCanvasAnimator.SetTrigger("Hide");
        //upgradeCanvas.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        UPCanvas.SetActive(false);
    }

    public void OnUPButtonDown()
    {
        if (money >= selectedMapCube.towerData.costUP)
        {
            ChangeMoney(-selectedMapCube.towerData.costUP);
            selectedMapCube.UPTower();
        }
        else
        {
            moneyAnimator.SetTrigger("Flicker");
        }

        StartCoroutine(HideUPUI());
    }
    public void OnDestroyButtonDown()
    {
        selectedMapCube.DestroyTower();
        StartCoroutine(HideUPUI());
    }

}
