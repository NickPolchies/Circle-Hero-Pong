using UnityEngine;
using UnityEngine.Events;

public class FlatPaddleController : MonoBehaviour
{
    [SerializeField] private GameController ball;
    [SerializeField] private bool isLeftPaddle;

    public void initializeSize(Vector2 size)
    {
        transform.localScale = size * ball.settings.fieldScale / 2;
    }

    void Update()
    {
        AutoSeekBall();
    }

    private void AutoSeekBall()
    {
        if (isLeftPaddle && ball.BallVelocity.x < 0)
        {
            ball.InputPaddlePosition(ball.BallPosition.y, isLeftPaddle);
        }
        else if (!isLeftPaddle && ball.BallVelocity.x > 0)
        {
            ball.InputPaddlePosition(ball.BallPosition.y, isLeftPaddle);
        }
    }

    public void SetPosition(float positionIn)
    {
        Vector3 temp = new Vector3(transform.position.x, positionIn * ball.settings.fieldScale / 2, 0);
        transform.position = temp;
    }
}
