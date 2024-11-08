using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform objetivo;
    public float velocidadCamara = 0.025f;
    public Vector3 desplazamiento;

    private void LateUpdate()
    {
        //la posicion deseada en y debe tener un limite entre -2 a 2
        Vector3 posicionDeseada = objetivo.position + desplazamiento;

        //limitar la posicion en y
        if (posicionDeseada.y > 2)
        {
            posicionDeseada.y = 2;
        }
        else if (posicionDeseada.y < -1.5)
        {
            posicionDeseada.y = -1.5f;
        }

        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadCamara);

        transform.position = posicionSuavizada;
    }
}
