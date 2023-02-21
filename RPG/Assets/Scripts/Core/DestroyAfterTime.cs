using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] GameObject containerToDestroy = null;

    public void DestroyAfterTimer(float time)
    {
        if(containerToDestroy != null)
        {
            Destroy(containerToDestroy, time);
        }
        else
        {
            Destroy(gameObject, time);
        }
    }
}
