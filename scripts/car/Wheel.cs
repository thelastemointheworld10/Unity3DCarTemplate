using UnityEngine;

namespace Scripts.Car
{
    [System.Serializable]
    public struct Wheel
    {
        public Transform WheelMesh;
        public WheelCollider WheelColliderInstance;
        public ParticleSystem SmokeEffect;

        public bool IsFrontWheel;

        public void UpdatePositionAndRotation()
        {
            Vector3 position;
            Quaternion rotation;

            WheelColliderInstance.GetWorldPose(out position, out rotation);

            WheelMesh.position = position;
            WheelMesh.rotation = rotation;
        }
    }
}