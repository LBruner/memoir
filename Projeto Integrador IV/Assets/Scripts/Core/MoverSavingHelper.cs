using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Dialogue;
using RPG.Saving;
using UnityEngine;

namespace RPG.Core
{
    public class MoverSavingHelper : MonoBehaviour, ISaveable
    {
        [SerializeField] private bool wasEnabled = false;

        private void Start()
        {
            if (wasEnabled)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        }

        public object CaptureState()
        {
            return wasEnabled;
        }

        public void RestoreState(object state)
        {
            wasEnabled = (bool)state;
        }

        internal void SetWasEnabled(bool state)
        {
            this.wasEnabled = state;
        }
    }
}

