using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ComponentTest
{

    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("FirstLevel");
    }


    [UnityTest]
    public IEnumerator HasAllComponentTest()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Assert.NotNull(player.GetComponent<Rigidbody2D>(), "The Rigidbody2D component is missing!");
        Assert.NotNull(player.GetComponent<CircleCollider2D>(), "The CircleCollider2D component is missing!");
        Assert.NotNull(player.GetComponent<BoxCollider2D>(), "The BoxCollider2D component is missing!");
        Assert.NotNull(player.GetComponent<Health>(), "The Health component is missing!");
        Assert.NotNull(player.GetComponent<Animator>(), "The Animator component is missing!");
        Assert.NotNull(player.GetComponent<PlayerCombat>(), "The PlayerCombat component is missing!");
        Assert.NotNull(player.GetComponent<CapsuleCollider2D>(), "The CapsuleCollider2D component is missing!");

        yield return null;
    }

}
