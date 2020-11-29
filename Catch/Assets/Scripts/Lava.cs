using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MarchingBytes;

public class Lava : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherGO = collision.collider.gameObject;
        
        //if (otherGO.tag == "Player")
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}

        if (otherGO.layer == LayerMask.NameToLayer("Objects"))
        {
            if (otherGO.tag == "Hail")
            {
                EasyObjectPool.instance.ReturnObjectToPool(otherGO);
            }
            else
            {
                Destroy(otherGO);
            }
        }
    }
}
