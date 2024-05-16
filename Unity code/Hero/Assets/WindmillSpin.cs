using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillSpin : MonoBehaviour
{

    [SerializeField] private float RotateSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, RotateSpeed * Time.deltaTime, Space.Self);
    }
}
