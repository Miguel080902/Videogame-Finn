using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : MonoBehaviour
{
    public PlayerSoundController playerSoundController;
    public ParticleSystem particulaSalto;

    public float velocidad = 5f;
    public bool step1 = false;
    public bool fall = false;
    public int vida = 5;
    public int vidaMax = 3;
    public float timeByStep = 0.2f;
    float cont = 0f;

    public float fuerzaSalto = 10f; 
    public float fuerzaRebote = 6f; 
    public float longitudRaycast = 0.1f; 
    public LayerMask capaSuelo; 

    private bool enSuelo; 
    private bool recibiendoDanio;
    private bool atacando;
    public bool muerto;

    float scaleZ;
    private Rigidbody2D rb; 

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        scaleZ = transform.localScale.z;
        rb = GetComponent<Rigidbody2D>();
        vida = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (!muerto)
        {
            if (!atacando)
            {
                Movimiento();

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);
                enSuelo = hit.collider != null;
                if(enSuelo && rb.velocity.y < 0 && fall)
                {
                    playerSoundController.playCaida();
                    fall = false;
                }
                if (enSuelo && Input.GetKeyDown(KeyCode.Space) && !recibiendoDanio)
                {
                    fall = true;
                    playerSoundController.playSaltar();
                    crearParticulaSalto();
                    rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
                }
            }

            if (Input.GetKeyDown(KeyCode.Z) && !atacando && enSuelo)
            {
                Atacando();
            }
        }
        
        animator.SetBool("ensuelo", enSuelo);
        animator.SetBool("recibeDanio", recibiendoDanio);
        animator.SetBool("Atacando", atacando);
        animator.SetBool("muerto", muerto);
    }
       
    void crearParticulaSalto()
    {
        particulaSalto.Play();
    }
    public void Movimiento()
    {
        float velocidadX = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;
       
        
        if (velocidadX != 0 && enSuelo && !recibiendoDanio && !atacando)
        {
            cont += Time.deltaTime;
            if(cont >= timeByStep)
            {
                cont = 0f;
                if (step1)
                {
                    playerSoundController.playMov1();
                }
                else
                {
                    playerSoundController.playMov2();
                }
                step1 = !step1;
            }
        }

        animator.SetFloat("movement", velocidadX * velocidad);

        if (velocidadX < 0)
        {
            
            transform.localScale = new Vector3(-scaleZ, 1, 1);
        }
        if (velocidadX > 0)
        {
            transform.localScale = new Vector3(scaleZ, 1, 1);
        }

        Vector3 posicion = transform.position;

        if (!recibiendoDanio)
            transform.position = new Vector3(velocidadX + posicion.x, posicion.y, posicion.z);
    }
    public void RecibeDanio(Vector2 direccion, int cantDanio)
    {
        if(!recibiendoDanio)
        {
            playerSoundController.playRecibirDanio();
            recibiendoDanio = true;
            vida -= cantDanio;
            if (vida<=0)
            {
                playerSoundController.playMuerte();
                muerto = true;

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.GameOver();
                }
            }
            if (!muerto)
            {
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 0.2f).normalized;
                rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
            }
        }
    }

    public void DesactivaDanio()
    {
        recibiendoDanio = false;
        rb.velocity = Vector2.zero;
    }

    public void Atacando()
    {
        playerSoundController.playAtacar();
        atacando = true;
    }

    public void DesactivaAtaque()
    {
        atacando = false;
    }

    public void CurarVida(int cantidad)
    {
        vida += cantidad;

        if (vida > vidaMax)
        {
            vida = vidaMax;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
    }
}
