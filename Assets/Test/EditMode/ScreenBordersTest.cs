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
        Assert.AreEqual(5.4f, Player.TOPO_TELA);
    }
    [Test]
    public void SouthBorder()
    {
        Assert.AreEqual(-5.4f, Player.FUNDO_TELA);
    }
    [Test]
    public void LeftBorder()
    {
        Assert.AreEqual(-9.3f, Player.ESQUERDA_TELA);
    }
    [Test]
    public void RightBorder()
    {
        Assert.AreEqual(9.3f, Player.DIREITA_TELA);
    }

}


