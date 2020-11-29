using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public class LavaBurn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGO = other.gameObject;

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
