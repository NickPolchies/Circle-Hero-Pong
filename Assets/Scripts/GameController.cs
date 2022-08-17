using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public GameSettings settings;

    [SerializeField] private float ballSize = 0.06f;
    [SerializeField] private Vector2 paddleSize = new Vector2(0.02f, 0.2f);
    [SerializeField] private float velocityScaleFactor;

    public UnityEvent<Vector2> setPaddleScale;
    public UnityEvent<float> setBallScale;

    public UnityEvent<float> moveLeftPaddle;
    public UnityEvent<float> moveRightPaddle;
    public UnityEvent<Vector2> moveBall;

    private Vector2 fieldSize = new Vector2(2, 1);

    private Vector2 ballVelocity;
    public Vector2 BallVelocity
    {
        get { return ballVelocity; }
    }

    private Vector2 ballPosition;
    public Vector2 BallPosition
    {
        get { return ballPosition; }
    }

    private float leftPaddlePosition;
    private float rightPaddlePosition;

    private bool colliding;

    void Start()
    {
        leftPaddlePosition = 0f;
        rightPaddlePosition = 0f;

        colliding = false;

        InitializeVisuals();

        Serve();
    }

    void Update()
    {
        Move();
        UpdateVisual();
    }

    private void InitializeVisuals()
    {
        setBallScale.Invoke(ballSize);
        setPaddleScale.Invoke(paddleSize);
    }

    private void Serve()
    {
        ballPosition = Vector3.zero;
        ballVelocity = Random.insideUnitCircle.normalized * 0.01f;
    }

    private void Move()
    {
        ballPosition += ballVelocity;

        Wrap();
        HandleCollision();
        HandleOutOfBounds();

        //print(position);
    }

    private void Wrap()
    {
        if (ballPosition.y > fieldSize.y)
        {
            ballPosition -= new Vector2(0, fieldSize.y * 2);
        }
        else if (ballPosition.y < -fieldSize.y)
        {
            ballPosition += new Vector2(0, fieldSize.y * 2);
        }
    }

    private void HandleCollision()
    {
        if (!colliding && ballPosition.x <= -fieldSize.x + ballSize / 2 + paddleSize.x / 2)
        {
            print("Close to left");
            if (DetectCollision(true))
            {
                colliding = true;
                ballVelocity.x = Mathf.Abs(ballVelocity.x) * velocityScaleFactor;
            }
        }
        else if (!colliding && ballPosition.x >= fieldSize.x - ballSize / 2 - paddleSize.x / 2)
        {
            print("Close to right");
            if (DetectCollision(false))
            {
                colliding = true;
                ballVelocity.x = -Mathf.Abs(ballVelocity.x) * velocityScaleFactor;
            }
        }
        else
        {
            colliding = false;
        }
    }

    bool DetectCollision(bool leftPaddle)
    {
        float paddleXPos = leftPaddle ? -fieldSize.x : fieldSize.x;
        Vector2 circleDistance = new Vector2();

        circleDistance.x = Mathf.Abs(ballPosition.x - paddleXPos);
        circleDistance.y = Mathf.Abs(ballPosition.y - (leftPaddle ? leftPaddlePosition : rightPaddlePosition));

        if (circleDistance.x > (paddleSize.x / 2 + ballSize / 2)) { return false; }
        if (circleDistance.y > (paddleSize.y / 2 + ballSize / 2)) { return false; }

        if (circleDistance.x <= (paddleSize.x / 2)) { return true; }
        if (circleDistance.y <= (paddleSize.y / 2)) { return true; }

        float cornerDistance_sq = Mathf.Pow(circleDistance.x - paddleSize.x / 2f, 2f) + Mathf.Pow(circleDistance.y - paddleSize.y / 2f, 2f);

        return (cornerDistance_sq <= Mathf.Pow(ballSize / 2, 2));
    }

    private void HandleOutOfBounds()
    {
        if(ballPosition.x > fieldSize.x + paddleSize.x / 2 + ballSize / 2)
        {
            //TODO Score player left
            Serve();
        }
        else if(ballPosition.x < -fieldSize.x - paddleSize.x / 2 - ballSize / 2)
        {
            //TODO Score player right
            Serve();
        }
    }

    private void UpdateVisual()
    {
        moveLeftPaddle.Invoke(leftPaddlePosition);
        moveRightPaddle.Invoke(rightPaddlePosition);
        moveBall.Invoke(ballPosition);
    }

    public void InputPaddlePosition(float pos, bool leftPaddle)
    {
        if (leftPaddle)
        {
            leftPaddlePosition = pos;
        }
        else
        {
            rightPaddlePosition = pos;
        }
    }
}
