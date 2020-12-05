using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class BasketEvents : MonoBehaviour
{
    public UnityEvent playerInBasket;
    public UnityEvent ballInBasket;


    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                playerInBasket.Invoke();
                break;
            case "Ball":
                ballInBasket.Invoke();
                break;
            default:
                break;
        }
    }
}
