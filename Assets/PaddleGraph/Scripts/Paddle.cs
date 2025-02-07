using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField, Min(0f)] float extents = 4f, speed = 10f;
    public bool isAI;
    public bool isP1;
    public bool isP2;

    public void Move (float target, float arenaExtents)
    {
        Vector3 p = transform.localPosition;

        /*if (isAI)
        {
            AdjustByAI(p.x, target);
        }
        else if (isP1)
        {
            AdjustByPlayer1(p.x);
        }
        else if (isP2)
        {
            AdjustByPlayer2(p.x);
        }*/
        p.x = isP1 ? AdjustByPlayer1(p.x) : AdjustByAI(p.x,target);
        //p.x = isP2 ? AdjustByPlayer2(p.x) : AdjustByAI(p.x,target);


        float limit = arenaExtents - extents;
        p.x = Mathf.Clamp(p.x, -limit, limit);
        transform.localPosition = p;
    }

    float AdjustByAI (float x,float target)
    {
        if (x<target)
        {
            return Mathf.Min(x + speed * Time.deltaTime, target);
        }
        return Mathf.Max(x-speed*Time.deltaTime,target);
    }

    float AdjustByPlayer1(float x)
    {
        bool p1GoRight = Input.GetKey(KeyCode.RightArrow);
        bool p1GoLeft = Input.GetKey(KeyCode.LeftArrow);

        if (p1GoRight && !p1GoLeft)
        {
            return x + speed * Time.deltaTime;
        }
        else if (p1GoLeft && !p1GoRight)
        {
            return x - speed * Time.deltaTime;
        }
        return x;
    }
    float AdjustByPlayer2(float x)
    {
        bool p2GoRight = Input.GetKey(KeyCode.D);
        bool p2GoLeft = Input.GetKey(KeyCode.A);

        if (p2GoRight && !p2GoLeft)
        {
            return x + speed * Time.deltaTime;
        }
        else if (p2GoLeft && !p2GoRight)
        {
            return x - speed * Time.deltaTime;
        }
        return x;
    }

    public bool HitBall(float _ballX, float _ballExtents, out float hitFactor)
    {
        hitFactor = (_ballX - transform.localPosition.x) / (extents + _ballExtents);
        return -1f <= hitFactor && hitFactor <= 1f;
    }
}
