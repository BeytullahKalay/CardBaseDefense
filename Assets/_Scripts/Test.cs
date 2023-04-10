using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap underGroundTilemap;
    [SerializeField] private TileBase tile;
    [SerializeField] private int sizeOfGround = 3;
    [SerializeField] private NavMeshSurface surface;

    private Vector3Int _lastPos;

    private void Update()
    {
        var mousePosVec2 = Helpers.GetWorldPositionOfPointer(Helpers.MainCamera);
        var tileAnchor = groundTilemap.tileAnchor;
        var mousePosVector3 =
            new Vector3(mousePosVec2.x, mousePosVec2.y, 0) - new Vector3(tileAnchor.x, tileAnchor.y, 0);
        var vector3IntPos = Vector3Int.RoundToInt(mousePosVector3);
        var downIntVal = Mathf.FloorToInt(sizeOfGround * .5f);
        var upIntVal = Mathf.CeilToInt(sizeOfGround * .5f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            print(groundTilemap.cellBounds);
            print(groundTilemap.size);
        }

        if (_lastPos != vector3IntPos)
        {
            underGroundTilemap.CompressBounds();
            groundTilemap.CompressBounds();

            CheckBoundaries(vector3IntPos, downIntVal, upIntVal);


            underGroundTilemap.ClearAllEditorPreviewTiles();
            _lastPos = vector3IntPos;
            underGroundTilemap.EditorPreviewBoxFill(vector3IntPos, tile, vector3IntPos.x - downIntVal,
                vector3IntPos.y - downIntVal, vector3IntPos.x + upIntVal, vector3IntPos.y + upIntVal);
        }

        if (Input.GetMouseButtonDown(0))
        {
            underGroundTilemap.BoxFill(vector3IntPos, tile, vector3IntPos.x - downIntVal,
                vector3IntPos.y - downIntVal, vector3IntPos.x + upIntVal, vector3IntPos.y + upIntVal);

            groundTilemap.BoxFill(vector3IntPos, tile, vector3IntPos.x - downIntVal,
                vector3IntPos.y - downIntVal, vector3IntPos.x + upIntVal, vector3IntPos.y + upIntVal);
            
            //surface.bu
            surface.UpdateNavMesh(surface.navMeshData);
        }
    }

    private void CheckBoundaries(Vector3Int vector3IntPos, int downIntVal, int upIntVal)
    {
        if (vector3IntPos.x - downIntVal < underGroundTilemap.origin.x) // left boundaries
        {
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

        if (vector3IntPos.x + upIntVal > underGroundTilemap.size.x - Mathf.Abs(underGroundTilemap.origin.x - 1)) // right boundaries
        {
            var newSize = underGroundTilemap.size;

            var offset = Mathf.Abs(vector3IntPos.x + upIntVal -
                                   (underGroundTilemap.size.x - 1 - Mathf.Abs(underGroundTilemap.origin.x)));
            
            newSize.x += offset;
            underGroundTilemap.size = newSize;
            groundTilemap.size = newSize;
        }

        if (vector3IntPos.y - downIntVal < underGroundTilemap.origin.y)
        {
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

        if (vector3IntPos.y + upIntVal > underGroundTilemap.size.y - Mathf.Abs(underGroundTilemap.origin.y - 1)) // upper boundaries
        {
            var newSize = underGroundTilemap.size;

            var offset = Mathf.Abs(vector3IntPos.y + upIntVal - (underGroundTilemap.size.y - 1 - Mathf.Abs(underGroundTilemap.origin.y)));
            
            newSize.y += offset;
            underGroundTilemap.size = newSize;
            groundTilemap.size = newSize;
        }
    }
}