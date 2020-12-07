using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PongLevelManager : MonoBehaviour
{
    public Rigidbody playerRB;
    public Rigidbody ballRB;

    public Text ballsLostText;
    public Text playersLostText;
    public Text winMsgText;

    public UnityEvent basketsGone;

    Vector3 playerLoc;
    Vector3 ballLoc;

    int playersLost;
    int ballsLost;
    int basketsRemaining;


    void Start()
    {
        playerLoc = playerRB.position;
        ballLoc = ballRB.position;

        playersLost = 0;
        ballsLost = 0;
        basketsRemaining = 10;
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
        playersLostText.text = "LIVES LOST: " + playersLost.ToString();
        Debug.Log("Players lost: " + playersLost.ToString());
    }

    public void IncrBallsLost()
    {
        ballsLost++;
        ballsLostText.text = "BALLS LOST: " + ballsLost.ToString();
        Debug.Log("Balls lost: " + ballsLost.ToString());
    }

    public void decrBasketsRemaining()
    {
        basketsRemaining--;
        Debug.Log("Baskets remaining: " + basketsRemaining.ToString());
        if (basketsRemaining == 0)
        {
            winMsgText.text = $"Price of victory: {playersLost} lives and {ballsLost} balls.";
            basketsGone.Invoke();
        }
    }
}
