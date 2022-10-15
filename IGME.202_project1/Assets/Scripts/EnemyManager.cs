using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update

    // list containing all enemies that exist
    [SerializeField]
    List<Enemy> enemyList;

    [SerializeField]
    GameObject enemy1Prefab;

    [SerializeField]
    GameObject enemy2Prefab;

    [SerializeField]
    GameObject enemy3Prefab;

    [SerializeField]
    public CollisionManager colManager;


    float timer = 0f;
    // reference to main camera
    [SerializeField]
    Camera cam;
    static float height;
    float width;
    void Start()
    {
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (enemyList.Count == 0)
        {
            SpawnEnemy();
        }
        for(int i = 0; i < enemyList.Count; i++)
        {
            
            if(i< 0 || i >= enemyList.Count)
            {
                i = 0;
            }
            
            if (enemyList[i].position.x <= cam.transform.position.x - width / 2)
            {
                clearEnemy(i);
                
            }
            else if (enemyList[i].position.x >= cam.transform.position.x + width / 2)
            {
                enemyList[i].position.x = cam.transform.position.x + width / 2;
            }

            else if (enemyList[i].position.y < cam.transform.position.y - height / 2)
            {
                enemyList[i].position.y = cam.transform.position.y + height / 2;
            }

            else if (enemyList[i].position.y > cam.transform.position.y + height / 2)
            {
                enemyList[i].position.y = cam.transform.position.y - height / 2;
            }

            if (enemyList.Count != 0 && enemyList[i].Health <= 0)
            {
                clearEnemy(i);
            }

        }
    }

    private void clearEnemy(int index)
    {
        Destroy(enemyList[index].gameObject);
        enemyList.RemoveAt(index);
    }

    private void SpawnEnemy()
    {
        // whenever spawn enemy is called, there is a random chance to spawn between 3 and 7 enemies
        for(int i = 0; i < Random.Range(3,8); i++)
        {
            float spawnHeight = Random.Range(-5f, 5f);
            int enemyType = GetEnemyType();
            GameObject newEnemy;
            if(enemyType == 1)
            {
                newEnemy = Instantiate(enemy1Prefab, new Vector2(cam.transform.position.x + width / 2, spawnHeight), Quaternion.identity, transform);
            }
            else if(enemyType == 2)
            {
                newEnemy = Instantiate(enemy2Prefab, new Vector2(cam.transform.position.x + width / 2, spawnHeight), Quaternion.identity, transform);
            }
            else
            {
                newEnemy = Instantiate(enemy3Prefab, new Vector2(cam.transform.position.x + width / 2, spawnHeight), Quaternion.identity,transform);
                
            }
            Enemy objectEnemyScript = newEnemy.GetComponent<Enemy>();
            enemyList.Add(objectEnemyScript);
            colManager.AddEnemyToList(objectEnemyScript);
        }
    }

    private int GetEnemyType()
    {
        if(timer < 10f)
        {
            return 1;
        }
        else if(timer < 20f)
        {
            return Random.Range(1, 3);
        }
        else
        {
            return Random.Range(1, 4);
        }
    }
}
