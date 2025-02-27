using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField, Min(0f)] float minExtents = 4f, maxExtents = 4f, speed = 10f, maxTargetingBias = 0.75f;
    float extents, targetingBias;
    public bool isAI;
    public bool isP1;
    public bool isP2;
    [SerializeField] TMP_Text _bluePlayerText;
    [SerializeField] TMP_Text _redPlayerText;
    int _score = 0;

    void Awake() => SetScore(0);
    void SetExtents(float newExtents)
    {
        extents = newExtents;
        Vector3 s = transform.localScale;
        s.x = 2f * newExtents;
        transform.localScale = s;
    }
    void ChangeTargetingBias() => targetingBias = Random.Range(-maxTargetingBias, maxTargetingBias);
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
        target += targetingBias * extents;
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
        ChangeTargetingBias();
        hitFactor = (_ballX - transform.localPosition.x) / (extents + _ballExtents);
        return -1f <= hitFactor && hitFactor <= 1f;
    }

    public void StartNewGame()
    {
        SetScore(0);
        ChangeTargetingBias();
    }

    public bool ScorePoint(int _pointsToWin)
    {
        SetScore(_score + 1, _pointsToWin);
        return _score >= _pointsToWin;
    }
    void SetScore(int newScore, float pointsToWin= 1000f)
    {
        _score = newScore;
        _bluePlayerText.SetText("{0}", newScore);
        SetExtents(Mathf.Lerp(maxExtents, minExtents, newScore / (pointsToWin - 1f)));
    }
}
