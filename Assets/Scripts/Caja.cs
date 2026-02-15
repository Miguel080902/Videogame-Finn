using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour
{
    public GameObject[] posiblesDrops;
    public float probabilidadDrop = 0.7f;

    private bool destruida = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Espada") && !destruida)
        {
            Destruir();
        }
    }

    void Destruir()
    {
        destruida = true;

        if (Random.value <= probabilidadDrop && posiblesDrops.Length > 0)
        {
            int index = Random.Range(0, posiblesDrops.Length);
            Instantiate(posiblesDrops[index], transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
