using UnityEngine;

namespace Scripts.Car
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(EngineSounds))]
    public sealed class CarController : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        [SerializeField] private Transform _centerOfMass;
        [Space]
        [Header("Wheels")]
        [SerializeField] private Wheel[] _wheels;
        [SerializeField] private float _motorForce;
        [Space]
        [Header("Brake")]
        [SerializeField] private int _brakeForce;
        [SerializeField] private float _breakFrontMultiplier;
        [SerializeField] private float _breakBackMultiplier;
        [Space]
        [Header("Steering")]
        [SerializeField] private int _maxSteerAngle;
        [Space]
        [Header("Smoke Effect")]
        [SerializeField] private float _smokeEffectSensitivity;

        private CarMovement _carMovement;
        private CarBrake _carBrake;
        private WheelRotation _wheelRotation;
        private CarSmoke _carSmoke;

        private float _horizontalInput;
        private float _verticalInput;

        [Space]
        [SerializeField] private FixedJoystick _joystick;
        private bool _isMobilePlatform;

        public void Initialize()
        {
            CheckValues();

            _isMobilePlatform = Application.isMobilePlatform;
            _joystick.gameObject.SetActive(_isMobilePlatform);

            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.centerOfMass = _centerOfMass.position;

            _carMovement = new CarMovement(ref _wheels, _motorForce);
            _carBrake = new CarBrake(ref _wheels,
                transform, _rigidbody,
                _brakeForce, _breakFrontMultiplier, _breakBackMultiplier);
            _wheelRotation = new WheelRotation(ref _wheels, _maxSteerAngle);
            _carSmoke = new CarSmoke(ref _wheels, _smokeEffectSensitivity);
        }

        private void HandleInput()
        {
            if (_isMobilePlatform)
            {
                _horizontalInput = _joystick.Horizontal;
                _verticalInput = _joystick.Vertical;
            }
            else
            {
                _horizontalInput = Input.GetAxis("Horizontal");
                _verticalInput = Input.GetAxis("Vertical");
            }
        }

        public float GetSpeed() => _rigidbody.linearVelocity.magnitude;

        private void Update()
        {
            HandleInput();

            _carMovement.SetVerticalInput(_verticalInput);
            _carBrake.HandleInput(_verticalInput);
            _wheelRotation.SetHorizontalInput(_horizontalInput);

            _carSmoke.ShowSmoke();
            _carBrake.Brake();
            _wheelRotation.Steer();
        }

        private void FixedUpdate() => _carMovement.Move();

        private void CheckValues()
        {
            if (_centerOfMass == null)
            {
                Debug.LogError("In CarController.cs: _centerOfMass is null!");
                this.enabled = false;
            }
            if (_wheels.Length == 0)
            {
                Debug.LogError("In CarController.cs: _wheels is empty!");
                this.enabled = false;
            }
            if (_motorForce == 0.0f)
            {
                Debug.LogWarning("In CarController.cs: _motorForce is 0.0f!");
            }
            if (_brakeForce == 0.0f)
            {
                Debug.LogWarning("In CarController.cs: _brakeForce is 0.0f!");
            }
            if (_breakFrontMultiplier == 0.0f)
            {
                Debug.LogWarning("In CarController.cs: _breakFrontMultiplier is 0.0f!");
            }
            if (_breakBackMultiplier == 0.0f)
            {
                Debug.LogWarning("In CarController.cs: _breakBackMultiplier is 0.0f!");
            }
            if (_maxSteerAngle == 0.0f)
            {
                Debug.LogWarning("In CarController.cs: _maxSteerAngle is 0.0f!");
            }
            if (_smokeEffectSensitivity == 0.0f)
            {
                Debug.LogWarning("In CarController.cs: _smokeEffectSensitivity is 0.0f!");
            }
        }
    }
}
