using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] Ball _ball;
    [SerializeField] Paddle _topPaddle, _bottomPaddle;

    [SerializeField, Min(0f)] Vector2 arenaExtents = new Vector2(10f, 10f);


    private void Awake()
    {
        _ball.StartNewGame();
    }

    private void Update()
    {
        _bottomPaddle.Move(_ball.Position.x,arenaExtents.x);
        _topPaddle.Move(_ball.Position.x,arenaExtents.x);
        _ball.Move();
        BounceYifNeeded();
        BounceXifNeeded();
        _ball.UpdateVisualization();
    }

    void BounceYifNeeded()
    {
        float yExtents = arenaExtents.y - _ball.Extents;
        
        if (_ball.Position.y < -yExtents)
        {
            _ball.BounceY(-yExtents);
        }
        else if (_ball.Position.y > yExtents)
        {
            _ball.BounceY(yExtents);
        }
    }

    void BounceXifNeeded()
    {
        float xExtents = arenaExtents.x - _ball.Extents;

        if (_ball.Position.x < -xExtents)
        {
            _ball.BounceX(-xExtents);
        }
        else if (_ball.Position.x > xExtents)
        {
            _ball.BounceX(xExtents);
        }
    }
}
