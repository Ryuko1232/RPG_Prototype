using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace RPG.Control
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] CinemachineFreeLook freeLookCamera;
        [SerializeField] float sensitivity = .1f;
        [SerializeField] float minDistance = 5f;
        [SerializeField] float maxDistance = 20f;

        private void Update()
        {
            if(freeLookCamera == null)
            {
                return;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) //up
            {
                for(int i = 0; i< freeLookCamera.m_Orbits.Length; i++)
                {
                    float scrollAmount = 0f;
                    scrollAmount = Mathf.Clamp(freeLookCamera.m_Orbits[i].m_Radius - sensitivity, minDistance, maxDistance);
                    freeLookCamera.m_Orbits[i].m_Radius = scrollAmount;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f) //down
            {
                for (int i = 0; i < freeLookCamera.m_Orbits.Length; i++)
                {
                    float scrollAmount = 0f;
                    scrollAmount = Mathf.Clamp(freeLookCamera.m_Orbits[i].m_Radius + sensitivity, minDistance, maxDistance);
                    freeLookCamera.m_Orbits[i].m_Radius = scrollAmount;
                }
            }
        }
    }
}
