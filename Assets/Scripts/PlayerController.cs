using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 touchPosition;
    private Vector3 direction;
    private Vector3 mousePosition;
    public float moveSpeed = 0.3f;
    public EnemyManager enemyManager;
    public HumanInPerilManager humanInPerilManager;
    bool enemyCollideExit = true, medicineCollideExit = true;
    public int Combo = 0;
    public float ComboSayac = 0;
    public GameObject floatingTextPlus;
    public GameObject floatingTextMinus;
    public GameObject Combo1X, Combo2X, Combo3X, Combo4X;
    public GameObject dodgedText;
    public GameObject maskedFace;
    public bool masked = false;
    public List<AudioClip> sounds;
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            if (touchPosition.x <= GameManager.Instance.enemyCreatePosLeftBottom.x)
            {
                touchPosition.x = GameManager.Instance.enemyCreatePosLeftBottom.x;
            }
            if (touchPosition.x >= GameManager.Instance.enemyCreatePosRightBottom.x)
            {
                touchPosition.x = GameManager.Instance.enemyCreatePosRightBottom.x;
            }
            direction = (touchPosition - transform.position);
            transform.position = touchPosition;
        }
        if (Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            if (mousePosition.x <= GameManager.Instance.enemyCreatePosLeftBottom.x)
            {
                mousePosition.x = GameManager.Instance.enemyCreatePosLeftBottom.x;
            }
            if (mousePosition.x >= GameManager.Instance.enemyCreatePosRightBottom.x)
            {
                mousePosition.x = GameManager.Instance.enemyCreatePosRightBottom.x;
            }
            if (mousePosition.y <= GameManager.Instance.enemyCreatePosLeftBottom.y)
            {
                mousePosition.y = GameManager.Instance.enemyCreatePosLeftBottom.y;
            }
            if (mousePosition.y >= GameManager.Instance.enemyCreatePosLeftTop.y)
            {
                mousePosition.y = GameManager.Instance.enemyCreatePosLeftTop.y;
            }
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }


    }
    private void FixedUpdate()
    {
        ComboSayac += Time.fixedDeltaTime;
        if (ComboSayac >= 2)
        {
            Combo = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Enemy":
                if (enemyCollideExit)
                {
                    enemyCollideExit = false;
                    if (masked)
                    {
                        masked = false;
                        ShowDodged();
                        GetComponent<SpriteRenderer>().enabled = true;
                        maskedFace.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    else
                    {
                        ShowDamage();
                        humanInPerilManager.EnemyCollide();
                    }
                    Destroy(other.gameObject);
                    EnemyManager.enemyCount--;
                    EnemyManager.sayac = 3;
                }
                break;
            case "Pill":
                if (medicineCollideExit)
                {
                    medicineCollideExit = false;
                    humanInPerilManager.MedicineCollide();
                    GetComponent<AudioSource>().PlayOneShot(sounds[0]);
                    ShowPoint();
                    Destroy(other.gameObject);
                    ComboSayac = 0;
                    Combo++;
                    ShowCombo();
                }
                break;
            case "Needle":
                if (medicineCollideExit)
                {
                    medicineCollideExit = false;
                    humanInPerilManager.MedicineCollide();
                    ShowPoint();
                    enemyManager.SlowEnemies(other.GetComponent<MedicineController>().slowRate);
                    enemyManager.OpenSlowEffect();
                    enemyManager.isSlowEffect = true;
                    Destroy(other.gameObject);
                    ComboSayac = 0;
                    Combo++;
                    ShowCombo();
                }
                break;
            case "Mask":
                if (medicineCollideExit)
                {
                    medicineCollideExit = false;
                    masked = true;
                    GetComponent<AudioSource>().PlayOneShot(sounds[1]);
                    GetComponent<SpriteRenderer>().enabled = false;
                    maskedFace.GetComponent<SpriteRenderer>().enabled = true;
                    Destroy(other.gameObject);
                }
                break;
            default:
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Enemy":
                enemyCollideExit = true;
                break;
            case "Pill":
                medicineCollideExit = true;
                break;
            case "Needle":
                medicineCollideExit = true;
                break;
            case "Mask":
                medicineCollideExit = true;
                break;
            default:
                break;
        }
    }
    void ShowDamage()
    {
        GameObject prefab = Instantiate(floatingTextMinus, transform.position, Quaternion.identity);
        Destroy(prefab, 1f);
    }
    void ShowPoint()
    {
        GameObject prefab = Instantiate(floatingTextPlus, transform.position, Quaternion.identity);
        Destroy(prefab, 1f);
    }
    void ShowDodged()
    {

        GameObject prefab = Instantiate(dodgedText, transform.position, Quaternion.identity);
        Destroy(prefab, 1f);
    }
    void ShowCombo()
    {
        if (ComboSayac <= 2 && Combo > 1)
        {
            switch (Combo)
            {
                case 2:
                    Destroy(Instantiate(Combo1X, transform.position, Quaternion.identity), 1f);
                    humanInPerilManager.Combo(1);
                    break;
                case 3:
                    Destroy(Instantiate(Combo2X, transform.position, Quaternion.identity), 1f);
                    humanInPerilManager.Combo(1);
                    break;
                case 4:
                    Destroy(Instantiate(Combo3X, transform.position, Quaternion.identity), 1f);
                    humanInPerilManager.Combo(1);
                    break;
                case 5:
                    Destroy(Instantiate(Combo4X, transform.position, Quaternion.identity), 1f);
                    humanInPerilManager.Combo(1);
                    break;
                default:
                    Destroy(Instantiate(Combo4X, transform.position, Quaternion.identity), 1f);
                    humanInPerilManager.Combo(1);
                    break;
            }
        }
    }
}
