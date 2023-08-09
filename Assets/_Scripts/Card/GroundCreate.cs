using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundCreate : MonoBehaviour, IUsable
{
    [SerializeField] private GroundCreateData groundCreateData;
    
    public bool Usable { get; set; }

    private SoundFXManager _soundFXManager;
    
    private Vector3Int _lastPos;

    private Tilemap _groundTilemap;
    private Tilemap _undergroundTilemap;
    private Tilemap _decorationTilemap;
    private Tilemap _bushTilemap;

    private int _numberOfGroundToPlace = 0;
    
    private Action _onCompleteAction;
    
    private TMP_Text _tmpText;

    private AudioClip _placingSoundFX;
    private GameObject _placingVFX;
    
    private MouseStateManager _mouseStateManager;


    private void Awake()
    {
        _soundFXManager = SoundFXManager.Instance;
        _groundTilemap = TilemapManager.Instance.GroundTilemap;
        _undergroundTilemap = TilemapManager.Instance.UndergroundTilemap;
        _decorationTilemap = TilemapManager.Instance.DecorationTilemap;
        _bushTilemap = TilemapManager.Instance.BushTilemap;
        _mouseStateManager = MouseStateManager.Instance;
    }

    private void Start()
    {
        OpenActions();
    }


    private void Update()
    {
        var vector3IntPos = SetGroundPosition(out var downIntVal, out var upIntVal);

        CheckIsPlaceable();

        if (!Usable) return;

        if (Input.GetMouseButtonDown(0))
        {
            CreateGround(vector3IntPos, downIntVal, upIntVal);
            CreatePlacingParticleVFX();
            PlayPlacingClip();
        }
    }

    private void PlayPlacingClip()
    {
        _soundFXManager.PlaySoundFXClip(_placingSoundFX, transform);
    }

    private void CreatePlacingParticleVFX()
    {
        Instantiate(_placingVFX, Helpers.GetWorldPositionOfPointer(Helpers.MainCamera), Quaternion.identity);
    }

    private void CheckIsPlaceable()
    {
        Usable = !_groundTilemap.HasTile(Helpers.GetMousePositionForTilemap(_groundTilemap));

        if (!Usable) return;
        
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
                    
                    Usable = true;
                    _undergroundTilemap.color = groundCreateData.NotPlaceableColor;
                    return;
                }
            }
        }
        
        Usable = false;
        _undergroundTilemap.color = groundCreateData.PlaceableColor;
    }

    private void CreateGround(Vector3Int vector3IntPos, int downIntVal, int upIntVal)
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

        // set camera max position values
        CameraController.Instance.UpdateMaxMovePosition(start,end);
        
        // set spawner spawn position values
        Spawner.Instance.UpdateMaxMovePosition(start,end);

        _numberOfGroundToPlace--;
        _tmpText.text = "X" + _numberOfGroundToPlace;

        if (_numberOfGroundToPlace <= 0)
        {
            _onCompleteAction?.Invoke();
            EventManager.SetBlockRaycastStateTo?.Invoke(true);
            EventManager.CloseBottomUI?.Invoke();
            CardSelectManager.Instance.SelectedCards.Clear();
            //_mouseStateManager.SetMouseBusyStateTo(MouseState.Available);
            Destroy(gameObject);
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
        DestroyActions();
    }

    public void OpenActions()
    {
        _mouseStateManager.SetMouseBusyStateTo(MouseState.Busy);
    }
    

    public void DestroyActions()
    {
        _undergroundTilemap.ClearAllEditorPreviewTiles();
        _mouseStateManager.SetMouseBusyStateTo(MouseState.Available);
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

    public void PrepareForPlacing(int placeGroundAmount,Action completeAction,TMP_Text textToUpdate,AudioClip placingSoundFX, GameObject placingVFX)
    {
        _numberOfGroundToPlace = placeGroundAmount;
        _onCompleteAction = completeAction;
        _tmpText = textToUpdate;
        _tmpText.text = "X" + placeGroundAmount;
        _placingSoundFX = placingSoundFX;
        _placingVFX = placingVFX;
    }
}