using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    public static int CountMonsterAlive = 0;
    public Wave[] waves;
    public Transform START;
    public float waveRate = 0.2f;
    private Coroutine coroutine;
    public GameObject map;


    void Start()
    {
        coroutine = StartCoroutine(SpawnMonster());
    }
    public void Stop()
    {
        StopCoroutine(coroutine);
    }
    IEnumerator SpawnMonster()
    {
        foreach (Wave wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject g = GameObject.Instantiate(wave.monsterPrefab, START.position, Quaternion.identity);
                g.transform.SetParent(START, false);
                CountMonsterAlive++;
                if (i != wave.count - 1)
                    yield return new WaitForSeconds(wave.rate);
            }
            while (CountMonsterAlive > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
        while (CountMonsterAlive > 0)
        {
            yield return 0;
        }
        GameManager.Instance.Win();
    }

}
