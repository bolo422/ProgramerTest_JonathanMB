using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Vida item;
    public ParticleSystem explosao;

    public Text pontos;
    public Text vida;
    public GameObject gameOverPanel;

    public int vidas = 3;
    public float tempoRespawn = 3.0f;
    public float tempoInvuneravelRespawn = 3.0f;
    public int pontuacao = 0;
    public int multiplicador = 1;

    void Start()
    {
        pontos.text = "Pontos: " + pontuacao.ToString();
        vida.text = vidas.ToString();
    }

    public void AsteroideDestruido(Asteroid asteroid)
    {
        this.explosao.transform.position = asteroid.transform.position;
        this.explosao.Play();

        if (asteroid.tamanho <  0.75f)
        {
            this.pontuacao += 100;
        }
        else if (asteroid.tamanho < 1.0f)
        {
            this.pontuacao += 50;
        }
        else
        {
            this.pontuacao += 25;
        }
        pontos.text = "Pontos: " + pontuacao;

        if(pontuacao >= multiplicador * 1000)
        {
            
            this.item.gameObject.SetActive(true);
            multiplicador++;
        }

    }

    public void PlayerMorreu()
    {
        this.explosao.transform.position = this.player.transform.position;
        this.explosao.Play();

        this.vidas--;
        if (this.vidas <= 0)
        {
            GameOver();
        }
        else 
        {
            Invoke(nameof(Respawn), this.tempoRespawn);
        }
        vida.text = vidas.ToString();
    }

    public void Cura()
    {
        if (this.vidas <= 5)
        {
            this.vidas++;
            vida.text = vidas.ToString();
        }
    }

    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("SemColisao");
        this.player.gameObject.SetActive(true);

        Invoke(nameof(AtivarColisao), this.tempoInvuneravelRespawn);
    }

    private void AtivarColisao()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void JogarNovamente()
    {
        SceneManager.LoadScene("Game");
    }

    private void GameOver()
    {
        CancelInvoke();
        gameOverPanel.SetActive(true);
    }

}
