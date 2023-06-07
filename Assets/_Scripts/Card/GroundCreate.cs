using System;
using System.Collections.Generic;
using TMPro;
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

    private int _numberOfGroundToPlace = 0;
    private Action _onCompleteAction;
    private TMP_Text _tmpText;

    private void Awake()
    {
        _groundTilemap = TilemapManager.Instance.GroundTilemap;
        _undergroundTilemap = TilemapManager.Instance.UndergroundTilemap;
        _decorationTilemap = TilemapManager.Instance.DecorationTilemap;
        _bushTilemap = TilemapManager.Instance.BushTilemap;
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


        for (var x = vector3IntPos.x - downIntVal; x < vector3IntPos.x + upIntVal; x++)
        {
            for (var y = vector3IntPos.y - downIntVal; y < vector3IntPos.y + upIntVal; y++)
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
        // if (Input.GetMouseButtonUp(0))
        // {
        //     var startX = vector3IntPos.x - downIntVal;
        //     var endX = vector3IntPos.x + upIntVal;
        //     var startY = vector3IntPos.y - downIntVal;
        //     var endY = vector3IntPos.y + upIntVal;
        //     
        //     _undergroundTilemap.BoxFill(vector3IntPos, groundCreateData.TileBase, startX, startY, endX, endY);
        //     _groundTilemap.BoxFill(vector3IntPos, groundCreateData.TileBase, startX, startY, endX, endY);
        //     _decorationTilemap.BoxFill(vector3IntPos, groundCreateData.DecorationTile, startX, startY, endX, endY);
        //
        //     
        //     AddRandomBushes(vector3IntPos, downIntVal, upIntVal);
        //
        //
        //     NavmeshManager.Instance.UpdateSurfaceData();
        //     _undergroundTilemap.ClearAllEditorPreviewTiles();
        //
        //     var start = _groundTilemap.CellToWorld(new Vector3Int(startX, startY, 0));
        //     var end =_groundTilemap.CellToWorld(new Vector3Int(endX, endY, 0));
        //
        //     CameraController.Instance.UpdateMaxMovePosition(start.x, start.y, end.x, end.y);
        //     
        //     Destroy(gameObject);
        // }
        
        if (Input.GetMouseButtonDown(0))
        {
            var startX = vector3IntPos.x - downIntVal;
            var endX = vector3IntPos.x + upIntVal;
            var startY = vector3IntPos.y - downIntVal;
            var endY = vector3IntPos.y + upIntVal;
            
            _undergroundTilemap.BoxFill(vector3IntPos, groundCreateData.TileBase, startX, startY, endX, endY);
            _groundTilemap.BoxFill(vector3IntPos, groundCreateData.TileBase, startX, startY, endX, endY);
            _decorationTilemap.BoxFill(vector3IntPos, groundCreateData.DecorationTile, startX, startY, endX, endY);

            
            AddRandomBushes(vector3IntPos, downIntVal, upIntVal);


            NavmeshManager.Instance.UpdateSurfaceData();
            _undergroundTilemap.ClearAllEditorPreviewTiles();

            var start = _groundTilemap.CellToWorld(new Vector3Int(startX, startY, 0));
            var end =_groundTilemap.CellToWorld(new Vector3Int(endX, endY, 0));

            CameraController.Instance.UpdateMaxMovePosition(start.x, start.y, end.x, end.y);

            _numberOfGroundToPlace--;
            _tmpText.text = "X" + _numberOfGroundToPlace;

            if (_numberOfGroundToPlace <= 0)
            {
                _onCompleteAction?.Invoke();
                EventManager.SetBlockRaycastStateTo?.Invoke(true);
                Destroy(gameObject);
            }
        }
    }

    private void AddRandomBushes(Vector3Int vector3IntPos, int downIntVal, int upIntVal)
    {
        for (var x = vector3IntPos.x - downIntVal; x <= vector3IntPos.x + upIntVal; x++)
        {
            for (var y = vector3IntPos.y - downIntVal; y <= vector3IntPos.y + upIntVal; y++)
            {
                _bushTilemap.SetTile(new Vector3Int(x, y, 0), groundCreateData.BushTile);
            }
        }
    }

    private Vector3Int SetGroundPosition(out int downIntVal, out int upIntVal)
    {
        var vector3IntPos = Helpers.GetMousePositionForTilemap(_groundTilemap);
        downIntVal = Mathf.FloorToInt(groundCreateData.SizeOfGround * .5f);
        upIntVal = Mathf.CeilToInt(groundCreateData.SizeOfGround * .5f) - 1;

        if (_lastPos != vector3IntPos)
        {
            _undergroundTilemap.CompressBounds();
            _groundTilemap.CompressBounds();

            CheckBoundaries(vector3IntPos, downIntVal, upIntVal);


            _undergroundTilemap.ClearAllEditorPreviewTiles();
            _lastPos = vector3IntPos;

            _undergroundTilemap.EditorPreviewBoxFill(
                vector3IntPos, groundCreateData.TileBase,
                vector3IntPos.x - downIntVal,
                vector3IntPos.y - downIntVal,
                vector3IntPos.x + upIntVal, 
                vector3IntPos.y + upIntVal);
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

    public void SetForPlacing(int placeGroundAmount,Action completeAction,TMP_Text textToUpdate)
    {
        _numberOfGroundToPlace = placeGroundAmount;
        _onCompleteAction = completeAction;
        _tmpText = textToUpdate;
        _tmpText.text = "X" + placeGroundAmount;
    }
}