using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour
{
    private Rigidbody2D Escudo_rigidbody;
    
    private void Awake()
    {
        Escudo_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag == "Player")
        {
            Escudo_rigidbody.velocity = Vector3.zero;
            Escudo_rigidbody.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().Escudo();
        }
    }
}
