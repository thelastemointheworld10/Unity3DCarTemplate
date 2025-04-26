using UnityEngine;

namespace Scripts.Car
{
    public sealed class CarBrake
    {
        private Wheel[] _wheels;
        private float _brakeForce;
        private float _brakeInput;

        private float _frontBrakeMultiplier;
        private float _backBrakeMultiplier;

        private Transform _carTransform;
        private Rigidbody _carRigidbody;

        public CarBrake(ref Wheel[] wheels,
            Transform carTransform, Rigidbody carRigidbody,
            float brakeForce, float frontBrakeMultiplier, float backBrakeMultiplier)
        {
            _wheels = wheels;

            _carTransform = carTransform;
            _carRigidbody = carRigidbody;

            _brakeForce = brakeForce;
            _frontBrakeMultiplier = frontBrakeMultiplier;
            _backBrakeMultiplier = backBrakeMultiplier;
        }

        public void HandleInput(float verticalInput)
        {
            float moveDirection = Vector3.Dot(_carTransform.forward, _carRigidbody.linearVelocity);

            if (verticalInput != 0.0f)
            {
                if (moveDirection < -0.5f && moveDirection > 0.5f)
                    _brakeInput = Mathf.Abs(verticalInput);
                else
                    _brakeInput = 0.0f;
            }
        }

        public void Brake()
        {
            foreach (Wheel wheel in _wheels)
            {
                float torque = _brakeInput * _brakeForce;

                if (wheel.IsFrontWheel)
                    wheel.WheelColliderInstance.brakeTorque = torque * _frontBrakeMultiplier;
                else
                    wheel.WheelColliderInstance.brakeTorque = torque * _backBrakeMultiplier;
            }
        }
    }
}