using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaIni : MonoBehaviour
{

    public float TempoDeVida = 10.0f;

    private void Update()
    {
        Destroy(this.gameObject, this.TempoDeVida);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
