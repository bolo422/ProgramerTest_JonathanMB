using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HyperspaceTest
{
    

    [UnityTest]
    public IEnumerator HyperspaceTestWithEnumeratorPasses()
    {
        var prefab = Resources.Load("Player") as GameObject;
        var player = GameObject.Instantiate(prefab);

        Vector3 oldPosition = player.transform.position;
        player.GetComponent<Player>().Hyperspace();

        yield return new WaitForSeconds(0.2f);

        //Assert.AreNotEqual(oldPosition, player.transform.position);
        Assert.Greater(Vector3.Distance(oldPosition, player.transform.position), 1.0f);
    }
}
