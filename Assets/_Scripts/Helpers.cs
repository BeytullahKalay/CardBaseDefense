using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helpers
{
    private static Camera _camera;
    public static Camera MainCamera
    {
        get
        {
            if (_camera == null) _camera = Camera.main;
            return _camera;
        }
    }

    /// <summary>
    ///  Returns world position of pointer.
    /// </summary>
    public static Vector2 GetWorldPositionOfPointer(Camera rayCam)
    {
        var pos = rayCam.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(pos.x, pos.y);
    }

    
    /// <summary>
    ///  Returns world position of canvas element.
    /// </summary>
    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element,Camera camera)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, camera, out var result);
        return result;
    }

    #region Mouse Over UI
    /// <summary>
    ///Returns 'true' if pointer touched or hovering on Unity UI element.
    /// </summary>
    public static bool IsPointerOverUIElement(LayerMask searchUILayer = default)
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults(), searchUILayer);
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    private static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaycastResults, LayerMask searchLayer)
    {
        for (int index = 0; index < eventSystemRaycastResults.Count; index++)
        {
            var curRaycastResult = eventSystemRaycastResults[index];
            if (curRaycastResult.gameObject.layer == searchLayer)
                return true;
        }

        return false;
    }


    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        return raycastResults;
    }

    #endregion
}