using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
    [SerializeField, Min(0f)] float _constantXSpeed = 8f, _constantYSpeed = 10f, extents = 0.5f; 
    public float Extents =>extents; //?
    public Vector2 Position => position;  //?
    
    Vector2 position, _velocity;

    public void UpdateVisualization() => transform.localPosition = new Vector3(position.x,0f,position.y);

    public void Move() => position += _velocity * Time.deltaTime;

    public void StartNewGame()
    {
        position = Vector2.zero;
        UpdateVisualization();
        _velocity = new Vector2(_constantXSpeed, -_constantYSpeed);
    }

    public void BounceX(float boundary)
    {
        position.x = 2f * boundary - position.x;
        _velocity.x=-_velocity.x;
    }

    public void BounceY(float boundary) 
    {
        position.y=2f*boundary-position.y;
        _velocity.y=--_velocity.y;
    }
}
