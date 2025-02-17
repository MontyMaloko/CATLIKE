using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
    [SerializeField, Min(0f)] float _maxXSpeed=20f, _maxStartXSpeed=2f, _constantYSpeed = 10f, extents = 0.5f; 
    public float Extents =>extents; //Documentation of new way of declarations
    public Vector2 Position => position;  //need to understadn getter and setters better
    public Vector2 Velocity => _velocity;
    
    Vector2 position, _velocity;


    
    public void UpdateVisualization() => transform.localPosition = new Vector3(position.x,0f,position.y);

    public void Move() => position += _velocity * Time.deltaTime;

    void Awake() => gameObject.SetActive(false);
    public void StartNewGame()
    {
        position = Vector2.zero;
        UpdateVisualization();
        //_velocity = new Vector2(_startXSpeed, -_constantYSpeed);
        _velocity.x = Random.Range(-_maxStartXSpeed, _maxStartXSpeed);
        _velocity.y = -_constantYSpeed;
        gameObject.SetActive(true);
    }

    public void EndGame()
    {
        position.x = 0f;
        gameObject.SetActive(false);
    }

    public void setXPositionAndSpeed(float start, float speedFactor, float deltaTime)
    {
        _velocity.x = _maxXSpeed * speedFactor;
        position.x = start + _velocity.x * deltaTime;
    }

    public void BounceX(float boundary)
    {
        position.x = 2f * boundary - position.x;
        _velocity.x = -_velocity.x;
    }

    public void BounceY(float boundary) 
    {
        position.y = 2f * boundary - position.y;
        _velocity.y = -_velocity.y;
    }
}
