using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineController : MonoBehaviour
{
    float sayac = 0;
    public float slowRate = 0;
    void FixedUpdate()
    {
        sayac += Time.deltaTime;
        if (sayac >= 1.5f)
        {
            Destroy(gameObject);
        }

    }
}
