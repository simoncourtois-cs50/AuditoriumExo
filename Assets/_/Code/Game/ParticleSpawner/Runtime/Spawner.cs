using UnityEngine;

namespace ParticleSpawner.Runtime
{
    public class Spawner : MonoBehaviour
    {
        private void Update()
        {
            SpawnParticle();
        }

        private void SpawnParticle()
        {
            _elapsedTime -= Time.deltaTime;
            if (_elapsedTime <= 0)
            {
                _elapsedTime = _timeBetweenParticle;
                GameObject particle = Instantiate(_particle, transform.position + Random.insideUnitSphere * _spawnerRadius, Quaternion.identity);
                Destroy(particle, _lifeSpanParticle);
            }
        }

        private float _elapsedTime;
        [SerializeField] private float _timeBetweenParticle;
        [SerializeField] private float _spawnerRadius;
        [SerializeField] private float _lifeSpanParticle;
        [SerializeField] private GameObject _particle;
    }
}
