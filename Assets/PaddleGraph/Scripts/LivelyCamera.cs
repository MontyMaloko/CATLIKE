using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivelyCamera : MonoBehaviour
{
    [SerializeField, Min(0f)] float jostleStrength = 40f, pushStrength = 1f, springStrength = 100f, dampingStrength = 10f, maxDeltaTime = 1f / 60f;
    Vector3 velocity, anchorPosition;
    void Awake() => anchorPosition = transform.localPosition;
    
    public void JostleY() => velocity.y += jostleStrength;
    public void PushXZ (Vector2 impulse)
    {
        velocity.x += pushStrength * impulse.x;
        velocity.z += pushStrength * impulse.y;
    }

    private void LateUpdate()
    {
        float dt = Time.deltaTime;
        while (dt>maxDeltaTime)
        {
            TimeStep(maxDeltaTime);
            dt -= maxDeltaTime;
        }
        TimeStep(dt);
    }

    void TimeStep(float dt)
    {
        Vector3 displacement = anchorPosition - transform.localPosition;
        Vector3 acceleration = springStrength * displacement - dampingStrength * velocity;
        velocity += acceleration * dt;
        transform.localPosition += velocity * dt;
    }
}
