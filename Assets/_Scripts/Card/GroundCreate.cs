using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundCreate : MonoBehaviour, IPlaceable
{
    [SerializeField] private GroundCreateData groundCreateData;


    public bool Placeable { get; set; }


    private Vector3Int _lastPos;

    private Tilemap _groundTilemap;
    private Tilemap _undergroundTilemap;
    private Tilemap _decorationTilemap;
    private Tilemap _bushTilemap;

    private void Awake()
    {
        _groundTilemap = NavmeshManager.Instance.GroundTilemap;
        _undergroundTilemap = NavmeshManager.Instance.UndergroundTilemap;
        _decorationTilemap = NavmeshManager.Instance.DecorationTilemap;
        _bushTilemap = NavmeshManager.Instance.BushTilemap;
    }


    private void Update()
    {
        var vector3IntPos = SetGroundPosition(out var downIntVal, out var upIntVal);

        CheckIsPlaceable();

        if (!Placeable) return;

        CreateGround(vector3IntPos, downIntVal, upIntVal);
    }

    private void CheckIsPlaceable()
    {
        var mousePosVec2 = Helpers.GetWorldPositionOfPointer(Helpers.MainCamera);
        Placeable = !_groundTilemap.HasTile(Vector3Int.RoundToInt(mousePosVec2));

        if (!Placeable) return;

        var vector3IntPos = Helpers.GetMousePositionForTilemap(_groundTilemap);
        var downIntVal = Mathf.FloorToInt(groundCreateData.SizeOfGround * .5f);
        var upIntVal = Mathf.CeilToInt(groundCreateData.SizeOfGround * .5f);

        for (var x = vector3IntPos.x - downIntVal; x <= vector3IntPos.x + upIntVal; x++)
        {
            for (var y = vector3IntPos.y - downIntVal; y <= vector3IntPos.y + upIntVal; y++)
            {
                var tileChecks = new List<bool>()
                {
                    _groundTilemap.HasTile(new Vector3Int(x, y, 0)),
                    _groundTilemap.HasTile(new Vector3Int(x + 1, y, 0)),
                    _groundTilemap.HasTile(new Vector3Int(x - 1, y, 0)),
                    _groundTilemap.HasTile(new Vector3Int(x, y - 1, 0)),
                    _groundTilemap.HasTile(new Vector3Int(x, y + 1, 0)),
                };

                foreach (var tileCheck in tileChecks)
                {
                    if (!tileCheck) continue;
                    
                    Placeable = true;
                    _undergroundTilemap.color = groundCreateData.NotPlaceableColor;
                    return;
                }
            }
        }
        
        Placeable = false;
        _undergroundTilemap.color = groundCreateData.PlaceableColor;
    }

    private void CreateGround(Vector3Int vector3IntPos, int downIntVal, int upIntVal)
    {
        if (Input.GetMouseButtonUp(0))
        {
            _undergroundTilemap.BoxFill(vector3IntPos, groundCreateData.TileBase, vector3IntPos.x - downIntVal,
                vector3IntPos.y - downIntVal, vector3IntPos.x + upIntVal, vector3IntPos.y + upIntVal);

            _groundTilemap.BoxFill(vector3IntPos, groundCreateData.TileBase, vector3IntPos.x - downIntVal,
                vector3IntPos.y - downIntVal, vector3IntPos.x + upIntVal, vector3IntPos.y + upIntVal);
            
            _decorationTilemap.BoxFill(vector3IntPos, groundCreateData.DecorationTile, vector3IntPos.x - downIntVal,
                vector3IntPos.y - downIntVal, vector3IntPos.x + upIntVal, vector3IntPos.y + upIntVal);
            
            
            // TODO: not working correctly
            // _bushTilemap.BoxFill(vector3IntPos, groundCreateData.BushTile, vector3IntPos.x - downIntVal,
            //     vector3IntPos.y - downIntVal, vector3IntPos.x + upIntVal, vector3IntPos.y + upIntVal);

            for (var x = vector3IntPos.x - downIntVal; x < vector3IntPos.x + upIntVal; x++)
            {
                for (var y = vector3IntPos.y - downIntVal; y < vector3IntPos.y + upIntVal; y++)
                {
                   _bushTilemap.SetTile(new Vector3Int(x,y,0),groundCreateData.BushTile); 
                }
            }
            
            

            NavmeshManager.Instance.UpdateSurfaceData();
            _undergroundTilemap.ClearAllEditorPreviewTiles();
            Destroy(gameObject);
        }
    }

    private Vector3Int SetGroundPosition(out int downIntVal, out int upIntVal)
    {
        var vector3IntPos = Helpers.GetMousePositionForTilemap(_groundTilemap);
        downIntVal = Mathf.FloorToInt(groundCreateData.SizeOfGround * .5f);
        upIntVal = Mathf.CeilToInt(groundCreateData.SizeOfGround * .5f);

        if (_lastPos != vector3IntPos)
        {
            _undergroundTilemap.CompressBounds();
            _groundTilemap.CompressBounds();

            CheckBoundaries(vector3IntPos, downIntVal, upIntVal);


            _undergroundTilemap.ClearAllEditorPreviewTiles();
            _lastPos = vector3IntPos;

            _undergroundTilemap.EditorPreviewBoxFill(vector3IntPos, groundCreateData.TileBase,
                vector3IntPos.x - downIntVal,
                vector3IntPos.y - downIntVal, vector3IntPos.x + upIntVal, vector3IntPos.y + upIntVal);
        }

        return vector3IntPos;
    }

    private void OnDestroy()
    {
        _undergroundTilemap.ClearAllEditorPreviewTiles();
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

        if (vector3IntPos.x + upIntVal >
            _undergroundTilemap.size.x - Mathf.Abs(_undergroundTilemap.origin.x - 1)) // right boundaries
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

        if (vector3IntPos.y + upIntVal >
            _undergroundTilemap.size.y - Mathf.Abs(_undergroundTilemap.origin.y - 1)) // upper boundaries
        {
            var underGroundTilemap = _undergroundTilemap;
            var groundTilemap = _groundTilemap;

            var newSize = underGroundTilemap.size;

            var offset = Mathf.Abs(vector3IntPos.y + upIntVal -
                                   (underGroundTilemap.size.y - 1 - Mathf.Abs(underGroundTilemap.origin.y)));

            newSize.y += offset;
            underGroundTilemap.size = newSize;
            groundTilemap.size = newSize;
        }
    }
}