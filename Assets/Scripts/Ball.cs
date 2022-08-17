using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] bool debug = true;
    [SerializeField] bool autoBounce;

    public float angle;
    public float distance;
    public float speedLinear;
    float speedAngular;
    float intensity;
    float intensityRamp = 0.01f;

    void Start()
    {
        angle = -90f;
        distance = 0.5f;
        speedLinear = Random.value > 0.5f ? 0.5f : -0.5f; //TODO tie this to a game controller variable
        speedAngular = 0;
        intensity = 1f;
    }

    void Update()
    {
        if (autoBounce)
        {
            if (distance > 1 && speedLinear > 0)
            {
                speedLinear = -0.5f * intensity;
                speedAngular = (Random.value * 2f - 1f) * 50f * intensity;
                intensity += intensityRamp;
            }
            else if (distance < 0 && speedLinear < 0)
            {
                speedLinear = 0.5f * intensity; ;
                speedAngular = (Random.value * 2f - 1f) * 50f * intensity;
                intensity += intensityRamp;
            }
        }

        distance += speedLinear * Time.deltaTime;
        angle += speedAngular * Time.deltaTime;

        SetPosition();

        if (debug)
        {
            print("SL: " + speedLinear + ", SA: " + speedAngular);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("COLLISION");

        /*
        if (distance > 1 && speedLinear > 0)
        {
            speedLinear = -0.5f * intensity;
            speedAngular = (Random.value * 2f - 1f) * 50f * intensity;
            intensity += intensityRamp;
        }
        else if (distance < 0 && speedLinear < 0)
        {
            speedLinear = 0.5f * intensity; ;
            speedAngular = (Random.value * 2f - 1f) * 50f * intensity;
            intensity += intensityRamp;
        }
        */




//        speedLinear = -speedLinear;

  //      speedAngular = (angle - collision.gameObject.GetComponent<Paddle>().GetAngle())/50;
    }

    private void SetPosition()
    {
        transform.position = new Vector3(Mathf.Lerp(2, 10, distance) * Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Lerp(2, 10, distance) * Mathf.Sin(Mathf.Deg2Rad * angle));
        transform.localScale = Vector3.Lerp(Vector3.zero + Vector3.one / 5, Vector3.one, distance);
    }
}
