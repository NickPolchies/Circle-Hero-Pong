using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    float angle;
    float previousAngle;
    float distance;
    float speed;
    public Ball ball;
    public bool player;

    void Start()
    {
        if (player)
        {
            distance = 10f;
        }
        else
        {
            distance = 2f;
        }
        angle = -90f;
        speed = 0f;
    }

    void Update()
    {
        if (player && ball.speedLinear > 0)
        {
            speed = angle;
            angle = Mathf.Lerp(previousAngle, ball.angle, ball.distance);
            speed = angle - speed;
        }
        else if (player)
        {
            angle += speed;
            speed *= 0.998f;
            previousAngle = angle;
        }
        else if (!player && ball.speedLinear < 0)
        {
            speed = angle;
            angle = Mathf.Lerp(previousAngle, ball.angle, 1 - ball.distance);
            speed = angle - speed;
        }
        else if (!player)
        {
            angle += speed;
            speed *= 0.998f;
            previousAngle = angle;
        }
        
        SetPosition();
    }

    public float GetAngle()
    {
        return angle;
    }

    private void SetPosition()
    {
        transform.position = new Vector3(distance * Mathf.Cos(Mathf.Deg2Rad * angle), distance * Mathf.Sin(Mathf.Deg2Rad * angle));
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + 90));
    }
}
