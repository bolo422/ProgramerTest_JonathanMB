using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const float TOPO_TELA = 5.4f;
    public const float FUNDO_TELA = -5.4f;
    public const float ESQUERDA_TELA = -9.3f;
    public const float DIREITA_TELA = 9.3f;   


    public Bala BalaPrefab;

    public float VelocidadeDeImpulso = 1.0f;
    public float VelocidadeDeVirada = 1.0f;


    private Rigidbody2D Player_rigidbody;
    public SpriteRenderer SR;
    public Collider2D PlayerColider;

    private bool impulso;
    private float direcao;
    private bool hyperspace;//true = utilizando hyperspace

    //Estabelecendo referencias
    private void Awake()
    {
        Player_rigidbody = GetComponent<Rigidbody2D>();
        PlayerColider = GetComponent<Collider2D>();
        SR = GetComponent<SpriteRenderer>();
        hyperspace = false;
    }

    private void Update()
    {
        // Verifica��o dos Inputs
        impulso = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direcao = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direcao = -1.0f;
        }
        else
        {
            direcao = 0.0f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Disparo();
        }
        if (Input.GetKeyDown(KeyCode.F) && !hyperspace)
        {
            SR.enabled = false;
            PlayerColider.enabled = false;
            hyperspace = true;
            Invoke("Hyperspace", 1.0f);
        }

        // Loop das coisas na tela

        Vector2 newPos = transform.position;
       
        if (transform.position.y > TOPO_TELA)
        {
            newPos.y = FUNDO_TELA;
        }
        if (transform.position.y < FUNDO_TELA)
        {
            newPos.y = TOPO_TELA;
        }
        if (transform.position.x > DIREITA_TELA)
        {
            newPos.x = ESQUERDA_TELA;
        }
        if (transform.position.x < ESQUERDA_TELA)
        {
            newPos.x = DIREITA_TELA;
        }
        transform.position = newPos;

    }
    
    private void FixedUpdate()
    {
        // Movimenta��o do Player
        if (impulso)
        {
            Player_rigidbody.AddForce(this.transform.up * this.VelocidadeDeImpulso);
        }

        if (direcao != 0.0f)
        {
            Player_rigidbody.AddTorque(direcao * this.VelocidadeDeVirada);
        }

    }

    private void Disparo()
    {
        Bala bala = Instantiate(this.BalaPrefab, this.transform.position, this.transform.rotation);
        bala.Atirar(this.transform.up);
    }

    public void Hyperspace()
    {
        // movimentar para uma posi��o aleatoria

        Vector2 oldPos = transform.position;
        Vector2 newPos = new Vector2(Random.Range(-8.25f, 8.25f), Random.Range(-4.4f, 4.4f));

        while (Vector2.Distance(oldPos, newPos) < 1)
        {
            newPos = new Vector2(Random.Range(-8.25f, 8.25f), Random.Range(-4.4f, 4.4f));
        }

        transform.position = newPos;
        Player_rigidbody.velocity = Vector3.zero;
        Player_rigidbody.angularVelocity = 0.0f;

        SR.enabled = true;
        PlayerColider.enabled = true;
        hyperspace = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica��o da colis�o, destroi o player
        if (collision.gameObject.tag == "Asteroid")
        {
            Player_rigidbody.velocity = Vector3.zero;
            Player_rigidbody.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerMorreu();
        }
    }

}
