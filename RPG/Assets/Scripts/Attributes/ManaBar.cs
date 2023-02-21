using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class ManaBar : MonoBehaviour
    {
        [SerializeField] Mana manaComponent = null;
        [SerializeField] RectTransform foreground = null;
        [SerializeField] RectTransform background = null;
        [SerializeField] Canvas rootCanvas = null;

        private void Update()
        {
            if (manaComponent == null)
            {
                return;
            }
            if (Mathf.Approximately(manaComponent.GetFraction(), 0f))
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
            else if (Mathf.Approximately(manaComponent.GetFraction(), 1f))
            {
                if (rootCanvas != null)
                {
                    rootCanvas.enabled = false;
                }
                return;
            }

            if (rootCanvas != null)
            {
                rootCanvas.enabled = true;
            }
            foreground.localScale = new Vector3(manaComponent.GetFraction(), 1f, 1f);
        }
    }
}
