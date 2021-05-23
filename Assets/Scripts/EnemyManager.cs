using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public static float sayac = 0;
    public static int enemyCount = 0;
    public int enemyCountMax = 4;
    public bool isSlowEffect = false;
    private void Start()
    {
        sayac = 3f;
    }
    void FixedUpdate()
    {
        CreateEnemy();
        sayac += Time.fixedDeltaTime;
        if (isSlowEffect)
        {
            slowSayac += Time.fixedDeltaTime;
            OpenSlowEffect();
        }
    }
    Vector2 CreateRandomPosition()
    {
        return Vector2.zero;
    }
    void CreateEnemy()
    {
        if (sayac >= 2f)
        {
            if (enemyCount < enemyCountMax + 1)
            {
                int randomArea = Random.Range(1, 3);
                Vector2 pos;
                Vector2 velocity;
                switch (randomArea)
                {
                    case 1:
                        pos = new Vector2(GameManager.Instance.enemyCreatePosLeftTop.x,
                            Random.Range(GameManager.Instance.enemyCreatePosLeftTop.y, GameManager.Instance.enemyCreatePosLeftBottom.y));
                        velocity = new Vector2(2, (Random.Range(0, 2) * 2 - 1) * 2);
                        break;
                    case 2:
                        pos = new Vector2(GameManager.Instance.enemyCreatePosRightTop.x,
                            Random.Range(GameManager.Instance.enemyCreatePosLeftTop.y, GameManager.Instance.enemyCreatePosLeftBottom.y));
                        velocity = new Vector2(-2, (Random.Range(0, 2) * 2 - 1) * 2);
                        break;
                    default:
                        pos = Vector2.zero;
                        velocity = Vector2.zero;
                        break;
                }
                enemyCount++;
                var instantiated = Instantiate(enemy, pos, Quaternion.identity);
                if (isSlowEffect)
                {
                    velocity *= .5f;
                }
                instantiated.GetComponent<EnemyController>().v = velocity;
                sayac = 0;
            }
        }
    }
    public void SlowEnemies(float rate)
    {
        var enemyList = FindObjectsOfType<EnemyController>();
        foreach (var enemy in enemyList)
        {
            enemy.Slow();
        }
    }
    public float slowSayac = 0;
    public void OpenSlowEffect()
    {
        if (slowSayac >= 5)
        {
            isSlowEffect = false;
            slowSayac = 0;
        }
    }
}
