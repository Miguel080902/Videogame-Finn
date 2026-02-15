using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool checkpointActivo = false;

    public Sprite spriteActivo;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !checkpointActivo)
        {
            ActivarCheckpoint();
        }
    }

    void ActivarCheckpoint()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ActualizarCheckpoint(transform.position, this);
        }

        checkpointActivo = true;

        if (spriteRenderer != null && spriteActivo != null)
        {
            spriteRenderer.sprite = spriteActivo;
        }
    }
}

