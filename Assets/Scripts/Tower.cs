using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : MonoBehaviour
{

    private  List<GameObject> monsters = new List<GameObject>();
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Monster")
        {
            monsters.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Monster")
        {
            monsters.Remove(col.gameObject);
        }
    }

    public float attackRateTime = 1; //多少秒攻击一次
    private float timer = 0;

    public GameObject bulletPrefab;//子弹
    public Transform firePosition;
    public Transform head;

    public bool useLaser = false;

    public float damageRate = 70;

    public LineRenderer laserRenderer;

    //public GameObject laserEffect;

    void Start()
    {
        timer = attackRateTime;
    }

    void Update()
    {
        if (monsters.Count > 0 && monsters[0] != null)
        {
            //Vector3 direction = new Vector3(0,head.position.y,0);
            Vector3 targetPosition = monsters[0].transform.position;
            //head.rotation = monsters[0].transform.rotation;
            targetPosition.z = head.position.z;
            Vector3 v = (targetPosition - transform.position).normalized;
            transform.up = v;
            //transform.rotation = Quaternion.LookRotation(direction);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 0.3f);
            //head.LookAt(targetPosition);
        }
        if (useLaser == false)
        {
            timer += Time.deltaTime;
            if (monsters.Count > 0 && timer >= attackRateTime)
            {
                timer = 0;
                Attack();
            }
        }
        else if (monsters.Count > 0)
        {
            if (laserRenderer.enabled == false)
                laserRenderer.enabled = true;
            //laserEffect.SetActive(true);
            if (monsters[0] == null)
            {
                UpdateMonsters();
            }
            if (monsters.Count > 0)
            {
                laserRenderer.SetPositions(new Vector3[] { firePosition.position, monsters[0].transform.position });
                monsters[0].GetComponent<Monster>().TakeDamage(damageRate * Time.deltaTime);
                //laserEffect.transform.position = monsters[0].transform.position;
                //Vector3 pos = transform.position;
                //pos.y = monsters[0].transform.position.y;
                //laserEffect.transform.LookAt(pos);
            }
        }
        else
        {
            //laserEffect.SetActive(false);
            laserRenderer.enabled = false;
        }
    }

    void Attack()
    {
        if (monsters[0] == null)
        {
            UpdateMonsters();
        }
        if (monsters.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.transform.SetParent(transform);
            bullet.GetComponent<Bullet>().SetTarget(monsters[0].transform);
        }
        else
        {
            timer = attackRateTime;
        }
    }

    void UpdateMonsters()
    {
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index < monsters.Count; index++)
        {
            if (monsters[index] == null)
            {
                emptyIndex.Add(index);
            }
        }

        for (int i = 0; i < emptyIndex.Count; i++)
        {
            monsters.RemoveAt(emptyIndex[i] - i);
        }
    }
}
