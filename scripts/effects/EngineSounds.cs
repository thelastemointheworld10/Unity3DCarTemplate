using UnityEngine;
using Scripts.Car;

namespace Scripts.Effects
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(CarController))]
    public sealed class EngineSounds : MonoBehaviour
    {
        private AudioSource _audioSource;
        private CarController _carController;

        [SerializeField] private float _speedRatio;
        [SerializeField] private float _maxGear;

        private float _minSpeed = 0.0f;
        private float _maxSpeed = 0.0f;
        private float _currentGear = 0.0f;

        public void Initialize()
        {
            _audioSource = GetComponent<AudioSource>();
            _carController = GetComponent<CarController>();

            CheckValues();
        }

        private void SwitchGear()
        {
            float carSpeed = _carController.GetSpeed();
            float maxSpeedValue = _speedRatio * _currentGear;

            if (carSpeed >= _maxSpeed)
            {
                if (_currentGear < _maxGear)
                {
                    ++_currentGear;

                    _minSpeed = _maxSpeed;
                    _maxSpeed += maxSpeedValue;
                }
            }
            else if (carSpeed < _minSpeed)
            {
                _maxSpeed -= maxSpeedValue;
                --_currentGear;
                _minSpeed -= maxSpeedValue;
            }

            if (_maxSpeed != 0.0f)
                _audioSource.pitch = 1 + carSpeed / _maxSpeed;
        }

        private void Update() => SwitchGear();

        private void CheckValues()
        {
            if (_speedRatio <= 0.0f) // _audioSource.pitch will be infinity or something like this
            {
                Debug.LogWarning("In EngineSounds.cs: _speedRatio is 0.0f! Setting it to 2.0f");
                _speedRatio = 2.0f;
            }
            if (_maxGear < 1.0f) // same
            {
                Debug.LogWarning("In EngineSounds.cs: _maxGear is 0.0f! Setting it to 4.0f");
                _maxGear = 4.0f;
            }
            if (_carController == null)
            {
                Debug.LogError("In EngineSounds.cs: _carController is null!");
                this.enabled = false;
            }
        }
    }
}