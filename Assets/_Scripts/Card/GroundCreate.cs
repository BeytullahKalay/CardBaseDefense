using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundCreate : MonoBehaviour,IPlaceable
{
    [SerializeField] private GroundCreateData groundCreateData;
    
    
    public bool Placeable { get; set; }


    private Vector3Int _lastPos;

    private Tilemap _groundTilemap;
    private Tilemap _undergroundTilemap;

    private void Awake()
    {
        _groundTilemap = NavmeshManager.Instance.GroundTilemap;
        _undergroundTilemap = NavmeshManager.Instance.UndergroundTilemap;
    }


    private void Update()
    {
        FindIsPlaceable();
        
        if(!Placeable) return;

        var vector3IntPos = SetGroundPosition(out var downIntVal, out var upIntVal);

        CreateGround(vector3IntPos, downIntVal, upIntVal);
    }

    private void FindIsPlaceable()
    {
        var mousePosVec2 = Helpers.GetWorldPositionOfPointer(Helpers.MainCamera);
        var mousePosVector3 = new Vector3(mousePosVec2.x, mousePosVec2.y, 0);
        var tileSearchPos = Vector3Int.RoundToInt(mousePosVector3);
        Placeable = !_groundTilemap.HasTile(tileSearchPos);
    }

    private void CreateGround(Vector3Int vector3IntPos, int downIntVal, int upIntVal)
    {
        if (Input.GetMouseButtonUp(0))
        {
            _undergroundTilemap.BoxFill(vector3IntPos, groundCreateData.TileBase, vector3IntPos.x - downIntVal,
                vector3IntPos.y - downIntVal, vector3IntPos.x + upIntVal, vector3IntPos.y + upIntVal);

            _groundTilemap.BoxFill(vector3IntPos, groundCreateData.TileBase, vector3IntPos.x - downIntVal,
                vector3IntPos.y - downIntVal, vector3IntPos.x + upIntVal, vector3IntPos.y + upIntVal);

            NavmeshManager.Instance.UpdateSurfaceData();
            _undergroundTilemap.ClearAllEditorPreviewTiles();
            Destroy(gameObject);
        }
    }

    private Vector3Int SetGroundPosition(out int downIntVal, out int upIntVal)
    {
        var mousePosVec2 = Helpers.GetWorldPositionOfPointer(Helpers.MainCamera);
        var tileAnchor = _groundTilemap.tileAnchor;
        var mousePosVector3 = new Vector3(mousePosVec2.x, mousePosVec2.y, 0) - new Vector3(tileAnchor.x, tileAnchor.y, 0);
        var vector3IntPos = Vector3Int.RoundToInt(mousePosVector3);
        downIntVal = Mathf.FloorToInt(groundCreateData.SizeOfGround * .5f);
        upIntVal = Mathf.CeilToInt(groundCreateData.SizeOfGround * .5f);

        if (_lastPos != vector3IntPos)
        {
            _undergroundTilemap.CompressBounds();
            _groundTilemap.CompressBounds();

            CheckBoundaries(vector3IntPos, downIntVal, upIntVal);


            _undergroundTilemap.ClearAllEditorPreviewTiles();
            _lastPos = vector3IntPos;
            _undergroundTilemap.EditorPreviewBoxFill(vector3IntPos, groundCreateData.TileBase, vector3IntPos.x - downIntVal,
                vector3IntPos.y - downIntVal, vector3IntPos.x + upIntVal, vector3IntPos.y + upIntVal);
        }

        return vector3IntPos;
    }


    public void PlaceActions()
    {
    }

    private void CheckBoundaries(Vector3Int vector3IntPos, int downIntVal, int upIntVal)
    {
        if (vector3IntPos.x - downIntVal < _undergroundTilemap.origin.x) // left boundaries
        {
            var underGroundTilemap = _undergroundTilemap;
            var groundTilemap = _groundTilemap;
            
            var newSize = underGroundTilemap.size;
            var newOrigin = underGroundTilemap.origin;

            var offset = Mathf.Abs(vector3IntPos.x - downIntVal - underGroundTilemap.origin.x);

            newSize.x += offset;
            underGroundTilemap.size = newSize;
            groundTilemap.size = newSize;

            newOrigin.x -= offset;
            underGroundTilemap.origin = newOrigin;
            groundTilemap.origin = newOrigin;
        }

        if (vector3IntPos.x + upIntVal > _undergroundTilemap.size.x - Mathf.Abs(_undergroundTilemap.origin.x - 1)) // right boundaries
        {
            var underGroundTilemap = _undergroundTilemap;
            var groundTilemap = _groundTilemap;

            
            var newSize = underGroundTilemap.size;

            var offset = Mathf.Abs(vector3IntPos.x + upIntVal -
                                   (underGroundTilemap.size.x - 1 - Mathf.Abs(underGroundTilemap.origin.x)));
            
            newSize.x += offset;
            underGroundTilemap.size = newSize;
            groundTilemap.size = newSize;
        }

        if (vector3IntPos.y - downIntVal < _undergroundTilemap.origin.y) // down boundaries
        {
            var underGroundTilemap = _undergroundTilemap;
            var groundTilemap = _groundTilemap;

            var newSize = underGroundTilemap.size;
            var newOrigin = underGroundTilemap.origin;
            
            var offset = Mathf.Abs(vector3IntPos.y - downIntVal - underGroundTilemap.origin.y);
            
            newSize.y += offset;
            underGroundTilemap.size = newSize;
            groundTilemap.size = newSize;

            newOrigin.y -= offset;
            underGroundTilemap.origin = newOrigin;
            groundTilemap.origin = newOrigin;
        }

        if (vector3IntPos.y + upIntVal > _undergroundTilemap.size.y - Mathf.Abs(_undergroundTilemap.origin.y - 1)) // upper boundaries
        {
            var underGroundTilemap = _undergroundTilemap;
            var groundTilemap = _groundTilemap;

            var newSize = underGroundTilemap.size;

            var offset = Mathf.Abs(vector3IntPos.y + upIntVal - (underGroundTilemap.size.y - 1 - Mathf.Abs(underGroundTilemap.origin.y)));
            
            newSize.y += offset;
            underGroundTilemap.size = newSize;
            groundTilemap.size = newSize;
        }
    }
}
