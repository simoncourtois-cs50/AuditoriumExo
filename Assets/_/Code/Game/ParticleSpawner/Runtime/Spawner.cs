using UnityEngine;

namespace ParticleSpawner.Runtime
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float _elapsedTime;
        [SerializeField] private float _timeBetweenParticle;
        [SerializeField] private GameObject _particle;

        private void SpawnParticle()
        {
            _elapsedTime -= Time.deltaTime;
            if (_elapsedTime <= 0)
            {
                _elapsedTime = _timeBetweenParticle;
            }
        }
    }
}
