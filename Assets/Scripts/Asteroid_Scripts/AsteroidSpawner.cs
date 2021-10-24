using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    public Asteroid asteroidPrefab;

    public float variacaoDeTrajeto = 15.0f;
    public float taxaDeSpawns = 5.0f;
    public int quantidadeDeSpawns = 2;
    public float distanciaDeSpawn = 15.0f;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.taxaDeSpawns, this.taxaDeSpawns);
    }

    private void Spawn()
    {
        for (int i = 0; i < this.quantidadeDeSpawns; i++)
        {
            Vector3 DirecaoDeSpawn = Random.insideUnitCircle.normalized * distanciaDeSpawn;
            Vector3 PontoDespawn = this.transform.position + DirecaoDeSpawn;

            float variação = Random.Range(-this.variacaoDeTrajeto, this.variacaoDeTrajeto);
            Quaternion rotacao = Quaternion.AngleAxis(variação, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, PontoDespawn, rotacao);
            asteroid.tamanho = Random.Range(asteroid.tamanhoMin, asteroid.tamanhoMax);
            asteroid.Trajetoria(rotacao * -DirecaoDeSpawn);
        }
    }

}
