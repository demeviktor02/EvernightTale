using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using UnityEngine.UIElements;

public class PlayerMovementTest
{
    public float targetSpeed = 8;

    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("FirstLevel");
    }

    [UnityTest]
    public IEnumerator HasRigidbody2DComponentTest()
    {     
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Assert.NotNull(player.GetComponent<Rigidbody2D>());

        yield return null;
    }

    [UnityTest]
    public IEnumerator RunTest()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<PlayerMovement2>().horizontal = 1;

        yield return null;

        Assert.IsTrue(player.GetComponent<PlayerMovement2>().isRunning);
    }

    [UnityTest]
    public IEnumerator SpeedTest()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Assert.AreEqual(targetSpeed,player.GetComponent<PlayerMovement2>().playerSpeed);

        yield return null;
    }

    [UnityTest]
    public IEnumerator JumpTest()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<PlayerMovement2>().jumpPressedRemember = player.GetComponent<PlayerMovement2>().jumpPressedRememberTime;

        yield return new WaitForSeconds(0.5f);

        Assert.IsTrue(player.GetComponent<PlayerMovement2>().isJumping);
    }

}
