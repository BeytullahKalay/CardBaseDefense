using UnityEngine;

public class MouseSelectManager : MonoBehaviour
{
    [SerializeField] private LayerMask selectableLayers;
    
    private IClickable _selectedClickable;
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Mouse0)) return;
        
        var hit = Physics2D.Raycast(_cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,selectableLayers);
        
        _selectedClickable?.OnDeselected();
        _selectedClickable = null;
        
        if (hit.collider == null) return;
        
        print(hit.collider.gameObject.name);
        _selectedClickable = hit.collider.GetComponent<IClickable>();
        _selectedClickable.OnSelected();
    }
}
