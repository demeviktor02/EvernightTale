using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public Vector2 position;
    public ParticleSystem particle;
    public GameObject destroyedVersion;
    public Transform parent;

    public void Destroy()
    {
        particle.Play();
        Instantiate(destroyedVersion, position, transform.rotation, parent);
        Destroy(gameObject);
    }
}
