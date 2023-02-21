using GameDevTV.Saving;
using GameDevTV.Utils;
using RPG.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class Mana : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 100f;

        LazyValue<float> manaPoints;

        private void Awake()
        {
            manaPoints = new LazyValue<float>(GetInitialValue);
        }

        public float GetInitialValue()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Mana);
        }

        private void Start()
        {
            manaPoints.ForceInit();
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateValue;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateValue;
        }

        public float GetValue()
        {
            return manaPoints.value;
        }

        public float GetMaxValue()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Mana);
        }

        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return manaPoints.value / GetComponent<BaseStats>().GetStat(Stat.Mana);
        }

        public void RemoveValue(float amount)
        {
            manaPoints.value = Mathf.Max(manaPoints.value - amount, 0);
        }

        public void AddValue(float manaToRestore)
        {
            manaPoints.value = Mathf.Min(manaPoints.value + manaToRestore, GetMaxValue());
        }

        public void RegenerateValue()
        {
            float regenManaPoints = GetComponent<BaseStats>().GetStat(Stat.Mana) * (regenerationPercentage / 100);
            manaPoints.value = Mathf.Max(manaPoints.value, regenManaPoints);
        }

        public object CaptureState()
        {
            return manaPoints.value;
        }

        public void RestoreState(object state)
        {
            manaPoints.value = (float)state;
        }
    }
}
