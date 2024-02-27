using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{

    public GameObject arrow;
    public float startLaunchForce;
    public float launchForce;
    public float maxLaunchForce;
    public float LaunchForceTime;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;
    Vector2 direction;

    private void Start()
    {
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
            points[i].transform.parent = gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 bowPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - bowPosition;
        transform.right = direction;



        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0) && launchForce <= maxLaunchForce)
            {
                launchForce += Time.deltaTime * LaunchForceTime;

            }

            if (Input.GetMouseButtonUp(0))
            {
                Shoot();
                launchForce = startLaunchForce;
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }



        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].transform.position = PointPosition(i * spaceBetweenPoints);
        }

        void Shoot()
        {
            GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
            newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
        }

        Vector2 PointPosition(float t) {
            Vector2 position = (Vector2)shotPoint.position +
            (direction.normalized * launchForce * t) + 0.5f * Physics2D.gravity * (t*t);
            return position;
        }
    }
}
