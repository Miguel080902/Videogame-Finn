using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class TiendaUI : MonoBehaviour
{
    [Header("Doble Salto")]
    public int precioDobleSalto = 50;
    public Button botonDobleSalto;
    public TextMeshProUGUI textoPrecioDobleSalto;
    public GameObject iconoComprado;

    private void Start()
    {
        ActualizarUI();

        if (botonDobleSalto != null)
            botonDobleSalto.onClick.AddListener(ComprarDobleSalto);
    }

    void ActualizarUI()
    {
        if (textoPrecioDobleSalto != null)
            textoPrecioDobleSalto.text = precioDobleSalto.ToString();

        PlayerController player = FindObjectOfType<PlayerController>();

        if (player != null && player.dobleSaltoDesbloqueado)
        {
            if (botonDobleSalto != null)
                botonDobleSalto.interactable = false;
            if (iconoComprado != null)
                iconoComprado.SetActive(true);
            if (textoPrecioDobleSalto != null)
                textoPrecioDobleSalto.text = "COMPRADO";
        }
        else
        {
            if (botonDobleSalto != null && GameManager.Instance != null)
            {
                botonDobleSalto.interactable = GameManager.Instance.monedas >= precioDobleSalto;
            }
        }
    }

    public void ComprarDobleSalto()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.monedas >= precioDobleSalto)
        {
            GameManager.Instance.RestarMoneda(precioDobleSalto);

            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                player.DesbloquearDobleSalto();
            }

            ActualizarUI();
        }
    }
}
