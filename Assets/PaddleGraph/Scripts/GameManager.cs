using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] Ball _ball;
    [SerializeField] Paddle _topPaddle, _bottomPaddle;
    [SerializeField, Min(2)] int _pointsToWin = 3;
    
    [SerializeField, Min(0f)] Vector2 arenaExtents = new Vector2(10f, 10f);
    [SerializeField] TMP_Text countdownText;
    [SerializeField, Min(1f)] float newGameDelay = 3f;
    float countdownUntilNewGame;

    private void Awake() => countdownUntilNewGame = newGameDelay;

    void StartNewGame()
    {
        _ball.StartNewGame();
        _bottomPaddle.StartNewGame();
        _topPaddle.StartNewGame();
    }

    private void Update()
    {
        _bottomPaddle.Move(_ball.Position.x,arenaExtents.x);
        _topPaddle.Move(_ball.Position.x,arenaExtents.x);

        if (countdownUntilNewGame<=0f)
        {
            UpdateGame();
        }
        else
        {
            UpdateCountdown();
        }
    }

    void UpdateGame()
    {
        _ball.Move();
        BounceYifNeeded();
        BounceXifNeeded(_ball.Position.x);
        _ball.UpdateVisualization();
    }

    void UpdateCountdown()
    {
        countdownUntilNewGame -= Time.deltaTime;
        if (countdownUntilNewGame<=0f)
        {
            countdownText.gameObject.SetActive(false);
            StartNewGame();
        }
        else
        {
            float displayValue = Mathf.Ceil(countdownUntilNewGame);
            if (displayValue<newGameDelay)
            {
                countdownText.SetText("{0}", displayValue);
            }
        }
            countdownText.SetText("{0}", countdownUntilNewGame);
    }

    
    void BounceYifNeeded()
    {
        float yExtents = arenaExtents.y - _ball.Extents;
        if (_ball.Position.y < -yExtents)
        {
            BounceY(-yExtents, _bottomPaddle, _topPaddle);
        }
        else if (_ball.Position.y > yExtents)
        {
            BounceY(yExtents, _topPaddle, _bottomPaddle);
        }

        
    }

    void BounceY(float boundary, Paddle defender, Paddle attacker)
    {
        float durationAfterBounce = (_ball.Position.y - boundary) / _ball.Velocity.y;
        float bounceX = _ball.Position.x - _ball.Velocity.x * durationAfterBounce;
       
        BounceXifNeeded(bounceX);
        bounceX=_ball.Position.x - _ball.Velocity.x*durationAfterBounce;
        _ball.BounceY(boundary);

        if (defender.HitBall(bounceX,_ball.Extents,out float hitFactor))
        {
            _ball.setXPositionAndSpeed(bounceX, hitFactor, durationAfterBounce);
        }
        else if (attacker.ScorePoint(_pointsToWin))
        {
            EndGame();
        }
    }

    void EndGame()
    {
        countdownUntilNewGame = newGameDelay;
        countdownText.SetText("GAME OVER");
        countdownText.gameObject.SetActive(true);
        _ball.EndGame();
    }

    void BounceXifNeeded(float x)
    {
        float xExtents = arenaExtents.x - _ball.Extents;
        if (x < -xExtents)
        {
            _ball.BounceX(-xExtents);
        }
        else if (x > xExtents)
        {
            _ball.BounceX(xExtents);
        }
    }
}
