using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PongBasket : MonoBehaviour
{
    public PongLevelManager levelManager;
    public GameObject basketFX;
    public GameObject basketGO;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            levelManager.decrBasketsRemaining();
            levelManager.ResetBall();

            //Quaternion FXRot = Quaternion.Euler(90f, 0f, 0f) * transform.rotation;
            Instantiate(basketFX, transform.position, transform.rotation);
            
            
            basketGO.SetActive(false);
        }
    }
}
