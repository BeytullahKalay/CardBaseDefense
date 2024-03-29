using UnityEngine;

public class MouseSelectManager : MonoBehaviour
{
    [SerializeField] private LayerMask selectableLayers;

    private IUnSelect[] _selectedClickable;
    private Camera _cam;
    private MouseStateManager _mouseStateManager;

    private void Awake()
    {
        _cam = Camera.main;
        _mouseStateManager = MouseStateManager.Instance;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Mouse0) || _mouseStateManager.MouseState == MouseState.Busy) return;

        var hit = Physics2D.Raycast(_cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, selectableLayers);


        if (_selectedClickable?.Length > 0)
        {
            foreach (var clickable in _selectedClickable)
            {
                clickable.UnSelected();
            }
        }

        _selectedClickable = null;

        if (hit.collider == null) return;

        _selectedClickable = hit.collider.GetComponents<IUnSelect>();

        if (_selectedClickable?.Length > 0)
        {
            foreach (var clickable in _selectedClickable)
            {
                clickable.OnSelected();
            }
        }
    }
}