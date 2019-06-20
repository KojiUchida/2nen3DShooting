using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField, Header("読み込むCSVファイル名")]
    string fileName;
    [SerializeField, Header("出現する敵のプレファブ")]
    AI[] spawnEnemies;
    [SerializeField, Header("出現時の高さ")]
    float y = 2f;

    private List<EnemySpawnData> spawnDatas;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        spawnDatas = CSVReader.ReadEnemySpawnData(fileName, y);
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if (spawnDatas == null) return;
        var dataList = spawnDatas.FindAll(data => data.spawnTiming <= timeElapsed);
        foreach (var data in dataList)
        {
            AI enemy = spawnEnemies[(int)data.enemyType];

            enemy.SetMove(data.moveType);

            Instantiate(enemy, data.position, Quaternion.identity);
        }
    }
}
