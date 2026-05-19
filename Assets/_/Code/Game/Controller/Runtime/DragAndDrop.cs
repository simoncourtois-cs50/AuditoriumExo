using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller.Runtime
{
    public class DragAndDrop : MonoBehaviour
    {
        #region Unity API

        private void Start()
        {
            _clickAction = InputSystem.actions.FindAction("Attack");
            _camera = Camera.main;
        }
        private void Update()
        {
            if (_clickAction.WasPressedThisFrame())
            {
                Vector3 mousePosition = GetMousePosition();
                Vector3 direction = mousePosition - _camera.transform.position;

                Collider2D hit = Physics2D.OverlapPoint(mousePosition);

                if (hit && hit.transform.CompareTag("Deviation"))
                {
                    _currentDragableObject = hit.transform.gameObject;
                }
            }

            if (_clickAction.WasReleasedThisFrame())
            {
                _currentDragableObject = null;
            }
            if (_currentDragableObject)
            {
                _currentDragableObject.transform.position = GetMousePosition();
            }
        }

        #endregion


        #region Main API

        private Vector3 GetMousePosition()
        {
            Vector3 mousePosition = Pointer.current.position.ReadValue();
            //float cameraDistance = Vector3.Distance(_camera.transform.position, Vector3.zero);
            mousePosition.z = Mathf.Abs(_camera.transform.position.z);
            //Vector3 mousePositionZCam = new Vector3(mousePosition.x, mousePosition.y, cameraDistance);

            Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(mousePosition);
            return mouseWorldPosition;

        }

        #endregion


        #region Private and Protected

        private InputAction _clickAction;
        private Camera _camera;
        private GameObject _currentDragableObject;

        #endregion
    }
}