using UnityEngine;

namespace ParticleSpawner.Runtime
{
    public class Spawner : MonoBehaviour
    {
        #region Unity API

        private void Update()
        {
            SpawnParticle();
        }

        #endregion


        #region Main API

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

        #endregion


        #region Private and Protected

        private float _elapsedTime;
        [SerializeField] private float _timeBetweenParticle;
        [SerializeField] private float _spawnerRadius;
        [SerializeField] private float _lifeSpanParticle;
        [SerializeField] private GameObject _particle;

        #endregion
    }
}
