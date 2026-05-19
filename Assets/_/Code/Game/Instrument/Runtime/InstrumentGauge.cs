using UnityEngine;
using UnityEngine.UI;

namespace Instrument.Runtime
{
    public class InstrumentGauge : MonoBehaviour
    {
        #region Unity API

        private void Awake()
        {
            _currentValidationState = 0f;
            _minRateToValidate = 0.4f;
        }
        private void Start()
        {
            _instrument = GetComponent<InstrumentManager>();
            _instrument.OnParticleTrigger += HandleOnTrigger;
        }
        private void Update()
        {
            _gaugeImage.fillAmount = _currentValidationState / _validationMaxLength;
            Debug.Log(_currentValidationState);
            IncrementGauge();
            CheckValidation();
        }

        #endregion


        #region Main API

        private void HandleOnTrigger(float ratio)
        {
            _currentParticuleRatio = ratio;
        }

        private void IncrementGauge()
        {
            if (_currentParticuleRatio > _minRateToValidate)
            {
                _currentValidationState += Time.deltaTime * _gaugeIncrementationSpeed;
                _currentValidationState = Mathf.Clamp(_currentValidationState, 0, _validationMaxLength);
            }
            else
            {
                _currentValidationState -= Time.deltaTime * _gaugeDecrementationSpeed;
                _currentValidationState = Mathf.Clamp(_currentValidationState, 0, _validationMaxLength);
            }
        }
        private void CheckValidation()
        {
            if (_currentValidationState >= _validationMaxLength) Debug.Log("Success");
        }

        #endregion


        #region Private and Protected

        [SerializeField] private float _gaugeIncrementationSpeed;
        [SerializeField] private float _gaugeDecrementationSpeed;
        [SerializeField] private float _validationMaxLength;
        [SerializeField] private float _minRateToValidate;
        [SerializeField] private Image _gaugeImage;

        private InstrumentManager _instrument;
        private float _currentParticuleRatio;
        private float _currentValidationState;
        
        #endregion
    }
}