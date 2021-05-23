using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> medicines;
    float sayac = 0;
    Vector2 worldPointScreenSize;
    float spawnAreaXUpper, spawnAreaXLower;
    float spawnAreaYUpper, spawnAreaYLower;
    public EnemyManager enemyManager;
    public PlayerController playerController;
    void Start()
    {
        SpawnArea();
    }

    void FixedUpdate()
    {
        CreateMedicine();
        sayac += Time.deltaTime;
    }
    void SpawnArea()
    {
        int w = Screen.width;
        int h = Screen.height;
        worldPointScreenSize = Camera.main.ScreenToWorldPoint(new Vector2(w, h));

        spawnAreaXLower = -worldPointScreenSize.x + (worldPointScreenSize.x * 10 / 100);
        spawnAreaXUpper = worldPointScreenSize.x - (worldPointScreenSize.y * 10 / 100);

        spawnAreaYLower = worldPointScreenSize.y - (worldPointScreenSize.y * 10 / 100);
        spawnAreaYUpper = 0 - (worldPointScreenSize.y * 10 / 100);
    }

    void CreateMedicine()
    {
        int randomMedicineNumber = Random.Range(0, medicines.Count);
        if (enemyManager.isSlowEffect == true)
        {
            randomMedicineNumber = 1;
        }
        if (playerController.masked == true)
        {
            randomMedicineNumber = 1;
        }
        GameObject med = medicines[randomMedicineNumber];
        if (sayac >= 1f)
        {
            float x = Random.Range(GameManager.Instance.enemyCreatePosLeftTop.x, GameManager.Instance.enemyCreatePosRightTop.x);
            while (Mathf.Abs(x - playerController.transform.position.x) < 1.5f)
            {
                x = Random.Range(GameManager.Instance.enemyCreatePosLeftTop.x, GameManager.Instance.enemyCreatePosRightTop.x);
            }
            float y = Random.Range(GameManager.Instance.enemyCreatePosLeftTop.y, GameManager.Instance.enemyCreatePosLeftBottom.y);
            while (Mathf.Abs(y - playerController.transform.position.y) < 1.5f)
            {
                y = Random.Range(GameManager.Instance.enemyCreatePosLeftTop.y, GameManager.Instance.enemyCreatePosLeftBottom.y);
            }
            Vector2 pos = new Vector2(x, y);
            var instantiated = Instantiate(med, pos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            sayac = 0;
        }
    }
}
