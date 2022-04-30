using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] Asteroid_sprites;

    public float tamanho = 1.0f;
    public float tamanhoMin = 0.5f;
    public float tamanhoMax = 1.5f;
    public float velociadeDoAsteroide = 50.0f;
    public float topoTela = 5.4f;
    public float fundoTela = -5.4f;
    public float EsquerdaTela = -9.3f;
    public float direitaTela = 9.3f;

    private SpriteRenderer sprites;
    private Rigidbody2D Asteroid_Rigidbody;

    //Estabelecendo referencias
    private void Awake()
    {
        sprites = GetComponent<SpriteRenderer>();
        Asteroid_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Escolha aleatoria de sprites, escala, rotação e massa dos asteroides
        sprites.sprite = Asteroid_sprites[Random.Range(0, Asteroid_sprites.Length)];
        this.transform.localScale = Vector3.one * this.tamanho;
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        Asteroid_Rigidbody.mass = this.tamanho;
    }

    private void Update()
    {
        // Loop das coisas na tela

        Vector2 newPos = transform.position;

        if (transform.position.y > topoTela)
        {
            newPos.y = fundoTela;
        }
        if (transform.position.y < fundoTela)
        {
            newPos.y = topoTela;
        }
        if (transform.position.x > direitaTela)
        {
            newPos.x = EsquerdaTela;
        }
        if (transform.position.x < EsquerdaTela)
        {
            newPos.x = direitaTela;
        }
        transform.position = newPos;

    }

    public void Trajetoria(Vector2 direcao)
    {
        Asteroid_Rigidbody.AddForce(direcao * this.velociadeDoAsteroide);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificação da colisão e "quebra" dos asteroides
        if (collision.gameObject.tag == "Bala")
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        if (this.tamanho * 0.5f >= this.tamanhoMin)
        {
            CriarDivisão();
            CriarDivisão();
        }
        var gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            FindObjectOfType<GameManager>().AsteroideDestruido(this);
        }
        Destroy(this.gameObject);
    }

    private void CriarDivisão()
    {
        //Criação dos "pedaços do asteroide "
        Vector2 posicao = this.transform.position;
        posicao += Random.insideUnitCircle * 0.5f;

        Asteroid metade = Instantiate(this, posicao, this.transform.rotation);
        metade.tamanho = this.tamanho * 0.5f;
        metade.Trajetoria(Random.insideUnitCircle.normalized * this.velociadeDoAsteroide);
    }

}
