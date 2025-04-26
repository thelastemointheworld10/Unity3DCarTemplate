using UnityEngine;
using Scripts.Car;
using Scripts.Effects;

namespace Scripts
{
    public class BootStrap : MonoBehaviour
    {
        [SerializeField] private CarController _carController;
        [SerializeField] private EngineSounds _engineSounds;

        private void Awake()
        {
            _carController.Initialize();
            _engineSounds.Initialize();
        }
    }
}