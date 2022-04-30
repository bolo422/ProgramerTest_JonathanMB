using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class OvniSpawnTest
{

    [UnityTest]
    public IEnumerator OvniSpawnTestWithEnumeratorPasses()
    {
        var gameManager = new GameObject().AddComponent<GameManager>();
        var ovniObject = Resources.Load("Ovni") as GameObject;
        var asteroidObject = new GameObject();

        GameObject.Instantiate(ovniObject);

        var prefab = Resources.Load("Asteroid") as GameObject;
        asteroidObject = GameObject.Instantiate(prefab);

        gameManager.test = true;
        gameManager.inimigo = ovniObject.GetComponent<Ovni>();        
        ovniObject.SetActive(false);

        gameManager.pontuacao = 4999;

        //gameManager.AsteroideDestruido(asteroidObject.GetComponent<Asteroid>());
        asteroidObject.GetComponent<Asteroid>().TakeDamage();

        yield return new WaitForSeconds(0.2f);

        Assert.IsTrue(ovniObject.activeSelf);

    }
}
