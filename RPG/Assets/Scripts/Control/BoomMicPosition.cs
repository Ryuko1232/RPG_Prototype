using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class BoomMicPosition : MonoBehaviour
    {
        [SerializeField] GameObject objectToFollow;
        void LateUpdate()
        {
            if(objectToFollow != null)
            {
                transform.position = objectToFollow.transform.position;
            }
        }
    }
}
