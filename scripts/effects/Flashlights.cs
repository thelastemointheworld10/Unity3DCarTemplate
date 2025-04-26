using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Effects
{
    public sealed class Flashlights : MonoBehaviour
    {
        [SerializeField] private List<Light> _flashlights = new List<Light>();

        private bool _isEnabled = false;

        private void Start()
        {
            CheckValues();

            foreach (Light flashlight in _flashlights)
                flashlight.enabled = _isEnabled;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _isEnabled = !_isEnabled;

                foreach (Light flashlight in _flashlights)
                    flashlight.enabled = _isEnabled;
            }
        }

        private void CheckValues()
        {
            if (_flashlights == null)
            {
                Debug.LogError("In Flashlights.cs: _flashlights is null!");
                this.enabled = false;
            }
        }
    }
}