using UnityEngine;

namespace Scripts.Effects
{
    public sealed class CameraOnTarget : MonoBehaviour
    {
        [SerializeField] private Transform _car;

        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;

        private Transform _camera;

        private void Start()
        {
            CheckValues();

            _camera = transform;
        }

        private void FixedUpdate()
        {
            Vector3 targetPosition = _car.TransformPoint(_offset);
            _camera.position = Vector3.Lerp(_camera.position, targetPosition, _moveSpeed * Time.fixedDeltaTime);

            Vector3 direction = _car.position - _camera.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            _camera.rotation = Quaternion.Lerp(_camera.rotation, targetRotation, _rotateSpeed * Time.fixedDeltaTime);
        }

        private void CheckValues()
        {
            if (_car == null)
            {
                Debug.LogError("In CameraOnTarget.cs: _car is null!");
                this.enabled = false;
            }
            /*
             * I do not turn off the script and do not log it as an error,
             * because the zero values of these fields can be used to create
             * a screensaver for the game, there is no need to look after the car.
             */
            if (_offset.magnitude == 0.0f)
                Debug.LogWarning("In CameraOnTarget.cs: _offset.magnitude is 0.0f!");
            if (_moveSpeed == 0.0f)
                Debug.LogWarning("In CameraOnTarget.cs: _moveSpeed is 0.0f!");
            if (_rotateSpeed == 0.0f)
                Debug.LogWarning("In CameraOnTarget.cs: _rotateSpeed is 0.0f!");
        }
    }
}
