using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] Ball _ball;
    [SerializeField] Paddle _topPaddle, _bottomPaddle;
    [SerializeField, Min(2)] int _pointsToWin = 3;
    
    

    [SerializeField, Min(0f)] Vector2 arenaExtents = new Vector2(10f, 10f);


    private void Awake() => StartNewGame();

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
        _ball.Move();
        BounceYifNeeded();
        BounceXifNeeded(_ball.Position.x);
        _ball.UpdateVisualization();
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
            StartNewGame();
        }
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
