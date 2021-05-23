using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject background;
    [HideInInspector]
    public Vector2 enemyCreatePosLeftTop, enemyCreatePosRightTop, enemyCreatePosLeftBottom, enemyCreatePosRightBottom;
    public Vector2 enemyCreateOffset;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        int w = Screen.width;
        int h = Screen.height;
        enemyCreateOffset += new Vector2(50, 50);
        enemyCreatePosLeftTop = Camera.main.ScreenToWorldPoint(new Vector2(enemyCreateOffset.x, Screen.height - enemyCreateOffset.y));
        enemyCreatePosRightTop = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - enemyCreateOffset.x, Screen.height - enemyCreateOffset.y));
        enemyCreatePosLeftBottom = Camera.main.ScreenToWorldPoint(new Vector2(enemyCreateOffset.x, 400 + enemyCreateOffset.y));
        enemyCreatePosRightBottom = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - enemyCreateOffset.x, 400 + enemyCreateOffset.y));
    }
}