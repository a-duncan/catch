using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongLevelManager : MonoBehaviour
{
    public Rigidbody playerRB;
    public Rigidbody ballRB;

    Vector3 playerLoc;
    Vector3 ballLoc;

    int playersLost;
    int ballsLost;


    void Start()
    {
        playerLoc = playerRB.position;
        ballLoc = ballRB.position;

        playersLost = 0;
        ballsLost = 0;
    }

    public void ResetPlayer()
    {
        playerRB.velocity = Vector3.zero;
        playerRB.angularVelocity = Vector3.zero;
        playerRB.position = playerLoc;
        playerRB.rotation = Quaternion.identity;
    }

    public void ResetBall()
    {
        ballRB.velocity = Vector3.zero;
        ballRB.angularVelocity = Vector3.zero;
        ballRB.position = ballLoc;
        ballRB.rotation = Quaternion.identity;

    }

    public void IncrPlayersLost()
    {
        playersLost++;
        Debug.Log("Players lost: " + playersLost.ToString());
    }

    public void IncrBallsLost()
    {
        ballsLost++;
        Debug.Log("Balls lost: " + ballsLost.ToString());
    }
}
