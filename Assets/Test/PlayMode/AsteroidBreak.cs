using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AsteroidBreak
{

    [UnityTest]
    public IEnumerator AsteroidBreakWithEnumeratorPasses()
    {
        var asteroidObject = new GameObject();
        var enemySpawner = new GameObject().AddComponent<AsteroidSpawner>();

        asteroidObject = enemySpawner.Spawn();

        if (asteroidObject != null)
        {
            asteroidObject.GetComponent<Asteroid>().TakeDamage();
        }

        yield return new WaitForSeconds(1f);

        var allAsteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        Assert.AreEqual(2, allAsteroids.Length);
    }
}



