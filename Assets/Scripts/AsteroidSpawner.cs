using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    public Asteroid asteroidPrefab;

    public float variacaoDeTrajeto = 15.0f;
    public float taxaDeSpawns = 5.0f;
    public int quantidadeDeSpawns = 1;
    public float distanciaDeSpawn = 15.0f;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.taxaDeSpawns, this.taxaDeSpawns);
    }

    public GameObject Spawn()
    {
        return Spawn(false);
    }

    public GameObject Spawn(bool test) // (bool test = false)
    {
        var asteroidObject = new GameObject();
        for (int i = 0; i < this.quantidadeDeSpawns; i++)
        {
            Vector3 DirecaoDeSpawn = Random.insideUnitCircle.normalized * distanciaDeSpawn;
            Vector3 PontoDespawn = this.transform.position + DirecaoDeSpawn;

            float variação = Random.Range(-this.variacaoDeTrajeto, this.variacaoDeTrajeto);
            Quaternion rotacao = Quaternion.AngleAxis(variação, Vector3.forward);
            var prefab = Resources.Load("Asteroid") as GameObject;

            GameObject asteroid = Instantiate(prefab, PontoDespawn, rotacao);
            var asteroidComponent = asteroid.GetComponent<Asteroid>();

            float asteroidMin = asteroidComponent.tamanhoMin;

            if (test) // só entra se for true
                asteroidMin = 1.0f;

            asteroidComponent.tamanho = Random.Range(asteroidMin, asteroidComponent.tamanhoMax);

            asteroidComponent.Trajetoria(rotacao * -DirecaoDeSpawn);
            asteroidObject = asteroid.gameObject;
        }

        return asteroidObject;
    }

}



