using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ovni : MonoBehaviour
{

    public Rigidbody2D Ovni_Rb;
    public Vector2 direcao;
    public float velocidade = 1f;
    public float veloBala = 0f;
    public float DelayDeDisparo = 1f;
    public float UltimoMomentoDisparado = 0f;
    public float colisao = 0;

    public Transform player;
    public GameObject balaIni;


    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (Time.time > UltimoMomentoDisparado + DelayDeDisparo)
        {
            //Atirar
            float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg - 90f;
            Quaternion q = Quaternion.AngleAxis(angulo, Vector3.forward);

            //Criar bala
            GameObject novaBala = Instantiate(balaIni, transform.position, q);
            novaBala.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, veloBala));

            UltimoMomentoDisparado = Time.time;
        }
    }

    private void FixedUpdate()
    {
        //Descobre como aproximar  o ovni do player

        direcao = (player.position - transform.position).normalized;
        Ovni_Rb.MovePosition(Ovni_Rb.position + direcao * velocidade * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificação da colisão
        colisao++;
        if (collision.gameObject.tag == "Bala" && colisao == 4)
        {
            colisao = 0;
            FindObjectOfType<GameManager>().OvniDestruido(this);
            this.gameObject.SetActive(false);
        }
    }

}
