using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Gamev2 : MonoBehaviour
{
    [SerializeField]
    Ball ball;

    [SerializeField]
    Paddle paddleRight, paddleLeft;

    [SerializeField, Min(0f)]
    Vector2 arenaExtents = new Vector2(10f, 10f);

    [SerializeField]
    TextMeshPro countdownText;

    [SerializeField]
    float newGameDelay = 3f;

    float countdownUntilNewGame;



    void Awake() => countdownUntilNewGame = newGameDelay;
    void StartNewGame()
    {
        ball.StartNewGame();
        paddleLeft.StartNewGame();
        paddleRight.StartNewGame();
    }


    private void Update()
    {
        paddleRight.Move(ball.Position.x, arenaExtents.x);
        paddleLeft.Move(ball.Position.x, arenaExtents.x);
        if (countdownUntilNewGame <= 0f)
        {
            Update();

        }

        else

        {
            UpdateCountdown();
        }

        void UpdateCountdown()
        {
            countdownUntilNewGame -= Time.deltaTime;
            if (countdownUntilNewGame <= 0f)
            {
                countdownText.gameObject.SetActive(false);
                StartNewGame();
            }
            else
            {
                float displayValue = Mathf.Ceil(countdownUntilNewGame);
                if (displayValue < newGameDelay)
                {
                    countdownText.SetText("{0}", displayValue);
                }
            }

            countdownText.setText("{0}", countdownUntilNewGame);
        }
        void BounceYIfNeeded()
        {
            float yExtents = arenaExtents.y - ball.Extents;

            if (ball.Position.y < -yExtents)
            {
                BounceY(-yExtents, paddleLeft);
            }

            else if (ball.Position.y > yExtents)
            {
                BounceY(yExtents, paddleRight);
            }



        }
        void BounceY(float boundary, Paddle defender)
        {
            float durationAfterBounce = (ball.Position.y - boundary) / ball.Velocity.y;
            float bounceX = ball.Position.x - ball.Velocity.x * durationAfterBounce;

            BounceXIfNeeded(bounceX);
            bounceX = ball.Position.x - ball.Velocity.x * durationAfterBounce;

            ball.BounceY(boundary);

            if (defender.HitBall(bounceX, ball.Extents, out float hitFactor))
            {
                ball.SetXPositionAndSpeed(bounceX, hitFactor, durationAfterBounce);
            }
        }

        void BounceXIfNeeded(float x)
        {
            float xExtents = arenaExtents.x - ball.Extents;
            if (x < -xExtents)
            {
                ball.BounceX(-xExtents);
            }
            else if (x > xExtents)
            {
                ball.BounceX(xExtents);
            }

        }
    }
}
