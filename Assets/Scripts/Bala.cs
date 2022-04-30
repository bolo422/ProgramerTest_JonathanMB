using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{

    public float VeloBala = 500.0f;
    public float TempoDeVida = 10.0f;

    private Rigidbody2D BalaRigidbody;

    //Estabelecendo referencias
    private void Awake()
    {
        BalaRigidbody = GetComponent<Rigidbody2D>();
    }

    public void Atirar(Vector2 direcao)
    {
        BalaRigidbody.AddForce(direcao * this.VeloBala);

        Destroy(this.gameObject, this.TempoDeVida);
    }

    private void  OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

}
