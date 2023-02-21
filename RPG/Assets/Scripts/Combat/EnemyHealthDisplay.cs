using RPG.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            if(fighter.GetTarget() == null)
            {
                GetComponent<TextMeshProUGUI>().SetText("N/A");
                return;
            }
            Health health = fighter.GetTarget();
            GetComponent<TextMeshProUGUI>().SetText(String.Format("{0:0}/{1:0}", health.GetValue(), health.GetMaxValue()));
        }
    }
}
