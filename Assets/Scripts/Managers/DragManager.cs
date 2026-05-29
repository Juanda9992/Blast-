using UnityEngine;
using UnityEngine.InputSystem;

public class DragManager : MonoBehaviour
{
    [SerializeField] private InputActionReference mousePos, mouseDown;
    [SerializeField] private Camera _camera;
    private CanonDrag currentCanon = null;
    void Awake()
    {
        mousePos.action.Enable();
        mouseDown.action.Enable();
    }
    void Update()
    {
        if (mouseDown.action.WasPressedThisFrame())
        {
            Ray ray = _camera.ScreenPointToRay(mousePos.action.ReadValue<Vector2>());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50))
            {
                if(hit.collider.TryGetComponent<CanonDrag>(out currentCanon))
                {
                    currentCanon.OnCanonClicked();
                }
            }
        }
    }
}
