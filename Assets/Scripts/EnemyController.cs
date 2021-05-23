using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D fizik;
    float sayac = 0;
    float hareketBaslaSayac = 0;
    bool hareketBasladi = false;
    public Vector2 v;
    public float yon;
    bool slowed = false;
    void Start()
    {
        fizik = GetComponent<Rigidbody2D>();

        yon = Random.Range(0, 2) * 2 - 1;
    }
    void FixedUpdate()
    {
        if (hareketBasladi)
        {
            sayac += Time.deltaTime;
            this.transform.Rotate(0, 0, 5 * yon);
            if (sayac >= 5f)
            {
                Destroy(gameObject);
                EnemyManager.enemyCount--;
            }
        }
        else
        {
            hareketBaslaSayac += Time.deltaTime;
            if (hareketBaslaSayac >= .5f)
            {
                hareketBasladi = true;
                fizik.velocity = v;
            }
        }
    }
    public void Slow()
    {
        if (!slowed)
        {
            slowed = true;
            Rigidbody2D rg2d = GetComponent<Rigidbody2D>();
            rg2d.velocity = rg2d.velocity * .7f;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb.velocity.magnitude < 1)
            {
                Debug.Log(rb.velocity);
                rb.AddForce(new Vector2(2, 2), ForceMode2D.Impulse);
            }
        }
    }
}
