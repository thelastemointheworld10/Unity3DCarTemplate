using UnityEngine;

namespace Scripts.Car
{
    public sealed class CarSmoke
    {
        private Wheel[] _wheels;
        private float _effectSensitivity = 1.0f;

        public CarSmoke(ref Wheel[] wheels, float effectSensitivity)
        {
            _wheels = wheels;
            _effectSensitivity = effectSensitivity;
        }

        public void ShowSmoke()
        {
            foreach (Wheel wheel in _wheels)
            {
                WheelHit wheelHit;
                wheel.WheelColliderInstance.GetGroundHit(out wheelHit);

                if (Mathf.Abs(wheelHit.sidewaysSlip) + Mathf.Abs(wheelHit.forwardSlip) > _effectSensitivity)
                {
                    if (wheel.SmokeEffect.isPlaying == false)
                        wheel.SmokeEffect.Play();
                }
                else
                {
                    wheel.SmokeEffect.Stop();
                }
            }
        }
    }
}