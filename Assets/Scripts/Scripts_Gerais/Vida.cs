using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{

    private Rigidbody2D Vida_rigidbody;
    
    private void Awake()
    {
        Vida_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag == "Player")
        {
            Vida_rigidbody.velocity = Vector3.zero;
            Vida_rigidbody.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().Cura();
        }
    }
}
