namespace Scripts.Car
{
    public sealed class WheelRotation
    {
        private Wheel[] _wheels;
        private int _maxRotatingAngle;
        private float _horizontalInput;

        public WheelRotation(ref Wheel[] wheels, int maxRotationAngle)
        {
            _wheels = wheels;
            _maxRotatingAngle = maxRotationAngle;
        }

        public void SetHorizontalInput(float horizontalInput) => _horizontalInput = horizontalInput;

        public void Steer()
        {
            float steeringAngle = _horizontalInput * _maxRotatingAngle;

            foreach (Wheel wheel in _wheels)
            {
                if (wheel.IsFrontWheel)
                    wheel.WheelColliderInstance.steerAngle = steeringAngle;
            }
        }
    }
}
