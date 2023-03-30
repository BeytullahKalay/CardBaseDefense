using UnityEngine;

public static class Utilitles
{
    public static Vector2 GetMouseToWorldPos2D()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(pos.x, pos.y);
    }
}
