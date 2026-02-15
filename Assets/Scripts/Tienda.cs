using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tienda : MonoBehaviour
{
    public GameObject promptUI;
    public GameObject tiendaUI;

    private bool jugadorCerca = false;
    private bool tiendaAbierta = false;

    void Start()
    {
        if (promptUI != null)
            promptUI.SetActive(false);
        if (tiendaUI != null)
            tiendaUI.SetActive(false);
    }
    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E) && !tiendaAbierta)
        {
            AbrirTienda();
        }
        else if (tiendaAbierta && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            CerrarTienda();
        }
    }
    void AbrirTienda()
    {
        tiendaAbierta = true;

        if (tiendaUI != null)
            tiendaUI.SetActive(true);
        if (promptUI != null)
            promptUI.SetActive(false);
        Time.timeScale = 0f;
    }
    public void CerrarTienda()
    {
        tiendaAbierta = false;

        if (tiendaUI != null)
            tiendaUI.SetActive(false);
        if (promptUI != null && jugadorCerca)
            promptUI.SetActive(true);
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;
            if (promptUI != null)
                promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;
            if (promptUI != null)
                promptUI.SetActive(false);

            if (tiendaAbierta)
                CerrarTienda();
        }
    }
}
