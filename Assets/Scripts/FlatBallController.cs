using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatBallController : MonoBehaviour
{
    [SerializeField] private GameController controller;

    public void SetBallPosition(Vector2 pos)
    {
        transform.position = pos * controller.settings.fieldScale / 2;
    }

    public void SetBallSize(float size)
    {
        transform.localScale = Vector3.one * size * controller.settings.fieldScale / 2;
    }
}
