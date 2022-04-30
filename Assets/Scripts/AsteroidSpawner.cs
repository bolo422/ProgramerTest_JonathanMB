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
    public bool testMode = false;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.taxaDeSpawns, this.taxaDeSpawns);
    }

    public GameObject Spawn()
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
            if(!testMode)
                asteroidComponent.tamanho = Random.Range(asteroidComponent.tamanhoMin, asteroidComponent.tamanhoMax);
            else
            {
                asteroidComponent.tamanho = Random.Range(1.0f, asteroidComponent.tamanhoMax);
            }
            asteroidComponent.Trajetoria(rotacao * -DirecaoDeSpawn);
            asteroidObject = asteroid.gameObject;
        }

        return asteroidObject;
    }

}
