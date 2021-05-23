using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanInPerilManager : MonoBehaviour
{
    public float Health;
    public float decreasingAmount;
    public Slider HealthSlider;
    public GameObject pauseMenu;
    public EnemyManager enemyManager;
    public MedicineManager medicineManager;
    public PlayerController playerController;
    public GameObject Failed;
    public GameObject NextLevel;
    public GameObject LevelCompleted;
    public Text timeText;
    public List<AudioClip> sounds;
    public bool played = false;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.Play();

        HealthSlider.minValue = 0;
        HealthSlider.maxValue = 100;
        HealthSlider.value = 50;
    }
    void FixedUpdate()
    {
        Health -= Time.fixedDeltaTime * decreasingAmount;
        HealthSlider.value = Mathf.Lerp(HealthSlider.value, Health, Time.deltaTime * 4);
        if (HealthSlider.value == 0)
        {
            if (!played)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(sounds[0]);
                played = true;
            }
            timeText.gameObject.SetActive(false);
            LevelCompleted.gameObject.SetActive(false);
            NextLevel.GetComponent<Button>().interactable = false;
            enemyManager.gameObject.SetActive(false);
            medicineManager.gameObject.SetActive(false);
            playerController.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
        if (HealthSlider.value == 100)
        {
            timeText.text = string.Format("Time: {0:0.00}", Time.timeSinceLevelLoad.ToString());
            if (!played)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(sounds[1]);
                played = true;
            }
            enemyManager.gameObject.SetActive(false);
            Failed.gameObject.SetActive(false);
            medicineManager.gameObject.SetActive(false);
            playerController.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
    }
    public void EnemyCollide()
    {
        Health -= 8;
    }
    public void MedicineCollide()
    {
        Health += 5f;
    }
    public void Combo(float h)
    {
        Health += h;
    }
}
