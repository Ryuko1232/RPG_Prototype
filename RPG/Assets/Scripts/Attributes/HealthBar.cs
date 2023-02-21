using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent = null;
        [SerializeField] RectTransform foreground = null;
        [SerializeField] RectTransform background = null;
        [SerializeField] Canvas rootCanvas = null;

        private void Update()
        {
            if(healthComponent == null)
            {
                return;
            }
            if (Mathf.Approximately(healthComponent.GetFraction(), 0f))
            {
                if (background != null)
                {
                    background.gameObject.SetActive(false);
                }
                if (rootCanvas != null)
                {
                    rootCanvas.enabled = false;
                }
                return;
            }
            else if (Mathf.Approximately(healthComponent.GetFraction(), 1f))
            {
                if(rootCanvas != null)
                {
                    rootCanvas.enabled = false;
                }
                return;
            }

            if (rootCanvas != null)
            {
                rootCanvas.enabled = true;
            }
            foreground.localScale = new Vector3(healthComponent.GetFraction(), 1f, 1f);
        }
    }
}
