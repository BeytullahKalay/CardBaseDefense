using UnityEngine;

public static class Utilities
{
    public static Vector2 GetMouseToWorldPos2D(Camera rayCam)
    {
        var pos = rayCam.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(pos.x, pos.y);
    }
}
