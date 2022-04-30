using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ScreenBordersTest
{
    [Test]

    public void NorthBorder()
    {
        var player = new GameObject().AddComponent<Player>();
        Assert.AreEqual(5.4f, player.GetComponent<Player>().topoTela);
    }
    [Test]

    public void SouthBorder()
    {
        var player = new GameObject().AddComponent<Player>();
        Assert.AreEqual(-5.4f, player.GetComponent<Player>().fundoTela);
    }
    [Test]

    public void LeftBorder()
    {
        var player = new GameObject().AddComponent<Player>();
        Assert.AreEqual(-9.3f, player.GetComponent<Player>().EsquerdaTela);
    }
    [Test]

    public void RightBorder()
    {
        var player = new GameObject().AddComponent<Player>();
        Assert.AreEqual(9.3f, player.GetComponent<Player>().direitaTela);
    }

}
