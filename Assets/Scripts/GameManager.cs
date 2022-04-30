using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Vida item;
    public Ovni inimigo;
    public ParticleSystem explosao;
    public Escudo esc;

    public Text pontos;
    public Text vida;
    public GameObject gameOverPanel;
    public GameObject novaPontuacaoPanel;
    public GameObject EscudoSP;
    public InputField melhorPont;
    public Text ListaMelhorPont;

    public int vidas = 3;
    public float tempoRespawn = 3.0f;
    public float tempoInvuneravelRespawn = 3.0f;
    public int pontuacao = 0;
    public int multiplicador = 1;
    public int multiplicadorOV = 1;
    public bool escudo;
    public bool colisao;
    public int melhorPontuação = 0;
    public bool novaPontuacao;
    public bool test = false;

    void Start()
    {
        if (test){
            return;
        }
        pontos.text = "Pontos: " + pontuacao.ToString();
        vida.text = vidas.ToString();
    }

    public void AsteroideDestruido(Asteroid asteroid)
    {
        if (!test){
            this.explosao.transform.position = asteroid.transform.position;
            this.explosao.Play();
        }

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
        if (!test){
            pontos.text = "Pontos: " + pontuacao.ToString();
        }

        if(pontuacao >= multiplicador * 2500 && !test)
        {
               
            this.item.gameObject.SetActive(true);
            multiplicador++;
        }
        if (pontuacao >= multiplicadorOV * 5000)
        {
            this.inimigo.gameObject.SetActive(true);
            multiplicadorOV++;
        }


    }
    public void OvniDestruido(Ovni ovni)
    {
        this.explosao.transform.position = ovni.transform.position;
        this.esc.gameObject.SetActive(true);
        this.explosao.Play();
        this.pontuacao += 1000;
        pontos.text = "Pontos: " + pontuacao.ToString();
    }

    public void PlayerMorreu()
    {
        colisao = true;
        if (!escudo)
        {
            this.explosao.transform.position = this.player.transform.position;
            this.explosao.Play();

            this.vidas--;
            colisao = true;
        }
        else if (colisao == true)
        {
            escudo = false;
            EscudoSP.SetActive(false);
        }
      
        if (this.vidas <= 0 && !escudo)
        {
            GameOver();
        }
        else if (!escudo)
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
    public void Escudo()
    {
        if (!escudo)
        {
            escudo = true;
            EscudoSP.SetActive(true);
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
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void GameOver()
    {
        CancelInvoke();
        if (pontuacao > melhorPontuação)
        {
            novaPontuacao = true;
            MelhorPontuacao();
        }
        else
        {
            gameOverPanel.SetActive(true);
            ListaMelhorPont.text = "Melhor Pontaução :" + "\n" + PlayerPrefs.GetString("NomeMelhorPont") + " " + PlayerPrefs.GetInt("melhorPontuacao");
        }
        
    }
    private void MelhorPontuacao()
    {
        melhorPontuação = PlayerPrefs.GetInt("melhorPontuacao");
        if (novaPontuacao == true)
        {
            novaPontuacaoPanel.SetActive(true);
            
        }
    }

    public void TextoPont()
    {
        string newInput = melhorPont.text;
        novaPontuacaoPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        PlayerPrefs.SetString("NomeMelhorPont", newInput);
        PlayerPrefs.SetInt("melhorPontuacao", pontuacao);
        ListaMelhorPont.text = "Melhor Pontaução :" + "\n" + PlayerPrefs.GetString("NomeMelhorPont") + " " + PlayerPrefs.GetInt("melhorPontuacao");

    }

}
