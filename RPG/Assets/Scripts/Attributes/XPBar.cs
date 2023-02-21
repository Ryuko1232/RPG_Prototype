using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class XPBar : MonoBehaviour
    {
        [SerializeField] RectTransform foreground = null;
        [SerializeField] RectTransform background = null;
        [SerializeField] Canvas rootCanvas = null;

        GameObject player;
        BaseStats stats;
        Experience experience;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            stats = player.GetComponent<BaseStats>();
            experience= player.GetComponent<Experience>();
            
        }

        private void Update()
        {
            if (Mathf.Approximately(GetXPToLevelUpFraction(), 0f))
            {
                if (background != null)
                {
                    background.gameObject.SetActive(false);
                }
                if (rootCanvas != null)
                {
                    rootCanvas.enabled = false;
                }
                foreground.localScale = new Vector3(0f, 0f, 0f);
                return;
            }
            else if (Mathf.Approximately(GetXPToLevelUpFraction(), 1f))
            {
                if (rootCanvas != null)
                {
                    rootCanvas.enabled = false;
                }
                foreground.localScale = new Vector3(1f, 0f, 0f);
                return;
            }

            if (rootCanvas != null)
            {
                rootCanvas.enabled = true;
            }
            foreground.localScale = new Vector3(GetXPToLevelUpFraction(), 1f, 1f);
        }

        private float GetXPToLevelUpFraction()
        {

            return experience.GetPoints() / stats.GetStat(Stat.ExperienceToLevelUp);
        }
    }
}
