namespace Scripts.Car
{
    public sealed class CarMovement
    {
        private Wheel[] _wheels;
        private float _motorForce;
        private float _verticalInput;

        public CarMovement(ref Wheel[] wheels, float motorForce)
        {
            _wheels = wheels;
            _motorForce = motorForce;
        }

        public void SetVerticalInput(float verticalInput) => _verticalInput = verticalInput;

        public void Move()
        {
            foreach (Wheel wheel in _wheels)
            {
                wheel.WheelColliderInstance.motorTorque = _motorForce * _verticalInput;
                wheel.UpdatePositionAndRotation();
            }
        }
    }
}