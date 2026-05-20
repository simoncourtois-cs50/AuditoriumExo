using System;
using UnityEngine;

namespace Instrument.Runtime
{
    public class InstrumentManager : MonoBehaviour
    {
        #region Public

        public event Action<float> OnParticleTrigger;

        #endregion


        #region Unity API

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.Play();
            _audioSource.volume = 0;
        }

        private void Update()
        {
            if(GetNormalizedParticleQuantity() > _particleQuantityTreshold)
            {
                IncrementVolume();
            }
            else
            {
                DecrementVolume();
                OnParticleTrigger?.Invoke(_particleQuantityNormalized);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Particle"))
            {
                _particleQuantity++;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Particle"))
            {
                _particleQuantity--;
            }
        }

        #endregion


        #region Tools and Utilities
    
        private float GetNormalizedParticleQuantity()
        {
            _particleQuantityNormalized = _particleQuantity / _particleQuantityMax;
            return _particleQuantityNormalized;
        }

        private void IncrementVolume()
        { 
            float volume = _fadingInSpeed * Time.deltaTime;
            _audioSource.volume += volume;
        }

        private void DecrementVolume()
        {
            float volume =  _fadingOutSpeed * Time.deltaTime;  
            _audioSource.volume -= volume;
        }

        #endregion


        #region Private and Protected

        private AudioSource _audioSource;
        private float _particleQuantity;
        private float _particleQuantityNormalized;

        [SerializeField] private float _particleQuantityMax;
        [SerializeField] private float _particleQuantityTreshold;
        [SerializeField] private float _fadingInSpeed;
        [SerializeField] private float _fadingOutSpeed;

        #endregion
    }
}