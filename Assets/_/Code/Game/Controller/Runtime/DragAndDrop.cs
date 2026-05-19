using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller.Runtime
{
    public class DragAndDrop : MonoBehaviour
    {
        #region Unity API
        private void Awake()
        {
            
            _clickInput = InputSystem.actions.FindAction("Attack");
            _camera = Camera.main;
        }
        
        private void Update()
        {
            OnStayState();
        }

        #endregion


        #region StateMachine

        private void OnStayState()
        {
            switch (_currentState)
            {
                case State.Rest:
                    
                    if (_clickInput.WasPressedThisFrame())
                    {
                        Vector3 mousePosition = GetMousePosition();

                        Collider2D hit = Physics2D.OverlapPoint(mousePosition);

                        if (hit && hit.transform.CompareTag("Deviation"))
                        {
                            _currentClickedObject = hit.transform.gameObject;
                            BoxCollider2D hitCollider = _currentClickedObject.GetComponent<BoxCollider2D>();
                    
                            Vector2 width = Vector2.Scale(hitCollider.size, _currentClickedObject.transform.lossyScale);
                    
                            if (Vector3.Distance(_currentClickedObject.transform.position, GetMousePosition()) > (width.x * (0.6 * 0.5)))
                            {
                                ChangeState(State.Resize);
                            }
                            else
                            {
                                ChangeState(State.Drag);
                            }
                        }
                    }
                    break;
                
                case State.Drag:
                    if (_currentClickedObject)
                    {
                        _currentClickedObject.transform.position = GetMousePosition();
                    }
                    CheckClickRelease();
                    break;
                case State.Resize:
                    Resize();
                    CheckClickRelease();
                    break;
            }
        }
        private void OnEnterState()
        {
            switch (_currentState)
            {
                case State.Rest:
                    _currentClickedObject = null;
                    break;
                case State.Drag:
                    break;
                case State.Resize:
                    break;
            }
        }
        private void OnExitState()
        {
            switch (_currentState)
            {
                case State.Rest:
                    break;
                case State.Drag:
                    break;
                case State.Resize:
                    break;
            }
        }

        private void ChangeState(State newState)
        {
            OnExitState();
            _currentState = newState;
            OnEnterState();
        }

        #endregion

        #region Main API

        private Vector3 GetMousePosition()
        {
            Vector3 mousePosition = Pointer.current.position.ReadValue();
            mousePosition.z = Mathf.Abs(_camera.transform.position.z);

            Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(mousePosition);
            return mouseWorldPosition;

        }

        private void Resize()
        {
            _horizontalInput = Mouse.current.delta.x.ReadValue();
            _currentClickedObject.transform.localScale *= 1 + (_horizontalInput * 0.1f);
        }

        private void CheckClickRelease()
        {
            if (_clickInput.WasReleasedThisFrame())
            {
                ChangeState(State.Rest);
            }
        }

        #endregion


        #region Private and Protected

        private InputAction _clickInput;
        private float _horizontalInput;
        private Camera _camera;
        private GameObject _currentClickedObject;
        private enum State
        {
            Rest,
            Drag,
            Resize
        }

        private State _currentState;

        #endregion
    }
}