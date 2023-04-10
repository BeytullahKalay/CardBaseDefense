using UnityEngine;

public class GroundCreate : MonoBehaviour
{
    [SerializeField] private GroundCreateData groundCreateData;

    private Vector3Int _lastPos;


    private void Update()
    {
        var mousePosVec2 = Helpers.GetWorldPositionOfPointer(Helpers.MainCamera);
        var tileAnchor = groundCreateData.GroundTilemap.tileAnchor;
        var mousePosVector3 =
            new Vector3(mousePosVec2.x, mousePosVec2.y, 0) - new Vector3(tileAnchor.x, tileAnchor.y, 0);
        var vector3IntPos = Vector3Int.RoundToInt(mousePosVector3);
        var downIntVal = Mathf.FloorToInt(  groundCreateData.SizeOfGround * .5f);
        var upIntVal = Mathf.CeilToInt(groundCreateData.SizeOfGround * .5f);
        
        if (_lastPos != vector3IntPos)
        {
           groundCreateData.UnderGroundTilemap.CompressBounds();
           groundCreateData.GroundTilemap.CompressBounds();

            CheckBoundaries(vector3IntPos, downIntVal, upIntVal);


           groundCreateData.UnderGroundTilemap.ClearAllEditorPreviewTiles();
            _lastPos = vector3IntPos;
            groundCreateData.UnderGroundTilemap.EditorPreviewBoxFill(vector3IntPos,groundCreateData.TileBase, vector3IntPos.x - downIntVal,
                vector3IntPos.y - downIntVal, vector3IntPos.x + upIntVal, vector3IntPos.y + upIntVal);
        }
    }
    
    private void CheckBoundaries(Vector3Int vector3IntPos, int downIntVal, int upIntVal)
    {
        if (vector3IntPos.x - downIntVal < groundCreateData.UnderGroundTilemap.origin.x) // left boundaries
        {
            var underGroundTilemap = groundCreateData.UnderGroundTilemap;
            var groundTilemap = groundCreateData.GroundTilemap;
            
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

        if (vector3IntPos.x + upIntVal > groundCreateData.UnderGroundTilemap.size.x - Mathf.Abs(groundCreateData.UnderGroundTilemap.origin.x - 1)) // right boundaries
        {
            var underGroundTilemap = groundCreateData.UnderGroundTilemap;
            var groundTilemap = groundCreateData.GroundTilemap;

            
            var newSize = underGroundTilemap.size;

            var offset = Mathf.Abs(vector3IntPos.x + upIntVal -
                                   (underGroundTilemap.size.x - 1 - Mathf.Abs(underGroundTilemap.origin.x)));
            
            newSize.x += offset;
            underGroundTilemap.size = newSize;
            groundTilemap.size = newSize;
        }

        if (vector3IntPos.y - downIntVal < groundCreateData.UnderGroundTilemap.origin.y)
        {
            var underGroundTilemap = groundCreateData.UnderGroundTilemap;
            var groundTilemap = groundCreateData.GroundTilemap;

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

        if (vector3IntPos.y + upIntVal > groundCreateData.UnderGroundTilemap.size.y - Mathf.Abs(groundCreateData.UnderGroundTilemap.origin.y - 1)) // upper boundaries
        {
            var underGroundTilemap = groundCreateData.UnderGroundTilemap;
            var groundTilemap = groundCreateData.GroundTilemap;

            var newSize = underGroundTilemap.size;

            var offset = Mathf.Abs(vector3IntPos.y + upIntVal - (underGroundTilemap.size.y - 1 - Mathf.Abs(underGroundTilemap.origin.y)));
            
            newSize.y += offset;
            underGroundTilemap.size = newSize;
            groundTilemap.size = newSize;
        }
    }
}
