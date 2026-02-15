using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaDestructible : MonoBehaviour
{
    public GameObject[] posiblesDrops;
    public float dropChance = 0.7f;

    public bool destruida = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Espada") && !destruida)
        {
            DestruirCaja();
        }
    }
    void DestruirCaja()
    {
        destruida = true;

        if (Random.value <= dropChance && posiblesDrops.Length > 0)
        {
            int index = Random.Range(0, posiblesDrops.Length);
            Instantiate(posiblesDrops[index], transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
