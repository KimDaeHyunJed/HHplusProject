using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime = 1.0f;
    [SerializeField] private float enemySpawnRate = 50.0f;
    [SerializeField] private float itemSpawnRate = 50.0f;
    [SerializeField] private List<GameObject> enemy = new List<GameObject>();
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject Finish;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    private float elapsed = 0;

    void Start()
    {
        GameManager.Instance.OnChangeStatEvent.AddListener(HandleOnChangeState);
    }

    void Update()
    {
        if (GameManager.Instance.gameState == EGameState.Play)
        {
            elapsed += Time.deltaTime;

            if (spawnTime <= elapsed)
            {
                var stage = GameManager.Instance.Stage;
                if (stage > enemy.Count)
                {
                    stage = enemy.Count;
                }

                var enemyIndex = Random.Range(0, stage);


                elapsed = 0;

                var spawnEnemy = SpawnObject(enemySpawnRate, enemy[enemyIndex]);
                var spawnItem = SpawnObject(itemSpawnRate, item);

                if (spawnEnemy == null && spawnItem != null)
                {
                    var pos = spawnItem.transform.localPosition;
                    pos.y = 0;

                    spawnItem.transform.localPosition = pos;
                }

                if (spawnEnemy != null && enemyIndex == 2)
                {
                    // Enmey
                    var ePos = spawnEnemy.transform.localPosition;
                    var randY = Random.Range(0, 3);
                    ePos.y = randY;

                    spawnEnemy.transform.localPosition = ePos;

                    if (spawnItem != null)
                    {
                        // Item
                        var pos = spawnItem.transform.localPosition;
                        if (randY == 2)
                        {
                            pos.y = 3.5f;
                        }
                        else if (randY == 1)
                        {
                            pos.y = 0;
                        }
                        else if (randY == 0)
                        {
                            pos.y = 1.5f;
                        }

                        spawnItem.transform.localPosition = pos;
                    }
                }
            }
        }
    }

    void HandleOnChangeState(EGameState state)
    {
        if (state == EGameState.Play)
        {

        }
        else if (state == EGameState.FinishGame)
        {
            // 피니시 소환
            SpawnObject(100, Finish);
        }
        else if (state == EGameState.Ready)
        {
            // 소환되어 있는 몹들 모두 제거
            foreach (var obj in spawnedObjects)
            {
                GameObject.Destroy(obj);
            }

            spawnedObjects.Clear();
        }
        else if (state == EGameState.NextStage)
        {
            if (enemySpawnRate < 80.0f)
            {
                enemySpawnRate += 5;
            }

            if (spawnTime > 0.6f)
            {
                spawnTime -= 0.1f;
            }

            // 소환되어 있는 몹들 모두 제거
            foreach (var obj in spawnedObjects)
            {
                GameObject.Destroy(obj);
            }

            spawnedObjects.Clear();
        }
        else if (state == EGameState.End)
        {
            enemySpawnRate = 50.0f;
            spawnTime = 1.0f;
        }
    }

    GameObject SpawnObject(float rate, GameObject spawn)
    {
        var rand = Random.Range(0, 100);
        if (rand <= rate)
        {
            var obj = GameObject.Instantiate(spawn, transform);
            var pos = obj.transform.localPosition;
            pos.x = 12.0f;

            obj.transform.localPosition = pos;

            spawnedObjects.Add(obj);

            return obj;
        }

        return null;
    }
}
