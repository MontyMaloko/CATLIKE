using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballv2 : MonoBehaviour
{
    [SerializeField, Min(0f)]
    float
        maxXSpeed = 20f,
       startXSpeed = 8f,
        ConstantYSpeed = 10f,
        extents = 0.5f;


    private void Awake() => gameObject.SetActive(false);

    public float Extents => extents;                            // This line means that its basically returning any value of extents 
    public Vector2 Position => position;

    Vector2 position, velocity;

    public void UpdateVizualization() =>

       transform.localPosition = new Vector3(position.x, 0f, position.y);

    public void Move() =>
        position += velocity * Time.deltaTime;


    public void StartNewGame()
    {
        position = Vector2.zero;
        UpdateVizualization();
        velocity = new Vector2(startXSpeed, -ConstantYSpeed);
        gameObject.SetActive(true);
    }

    public void SetXPositionAndSpeed(float start, float speedFactor, float deltaTime)
    {
        velocity.x = maxXSpeed * speedFactor;
        position.x = start + velocity.x * deltaTime;
    }

    public void BounceX(float boundary)
    {
        position.x = 2f * boundary - position.x;
        velocity.x = -velocity.x;
    }

    public void BounceY(float boundary)
    {
        position.y = 2f * boundary - position.y;
        velocity.y = -velocity.y;
    }

    public Vector2 Velocity => velocity;

    public void EndGame()
    {
        position.x = 0f;
        gameObject.SetActive(false);
    }

}
