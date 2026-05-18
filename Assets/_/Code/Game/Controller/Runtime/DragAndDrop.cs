using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller.Runtime
{
    public class DragAndDrop : MonoBehaviour
    {
        private InputAction _clickAction;
        private Camera _camera;
        private GameObject _currentDragableObject;
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

                RaycastHit2D hit = Physics2D.Raycast(_camera.transform.position, direction);

                if (hit.collider.gameObject.CompareTag("Deviation"))
                {
                    Debug.Log("hit");
                    _currentDragableObject = hit.collider.gameObject;
                }
            }

            if (_clickAction.WasReleasedThisFrame())
            {
                _currentDragableObject = null;
            }
        }
        private void LateUpdate()
        {
            if (_currentDragableObject)
            {
                _currentDragableObject.transform.position = GetMousePosition();
            }
        }
        private Vector3 GetMousePosition()
        {
            Vector2 mousePosition = Pointer.current.position.ReadValue();
            float cameraDistance = Vector3.Distance(_camera.transform.position, Vector3.zero);

            Vector3 mousePositionZCam = new Vector3(mousePosition.x, mousePosition.y, cameraDistance);

            Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(mousePositionZCam);
            return mouseWorldPosition;

        }
    }
}
