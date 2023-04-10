using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private TileBase tile;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var mousePosVec2 = Helpers.GetWorldPositionOfPointer(Helpers.MainCamera);
            var tileAnchor = groundTilemap.tileAnchor;

            var mousePosVector3 = new Vector3(mousePosVec2.x, mousePosVec2.y, 0) - new Vector3(tileAnchor.x, tileAnchor.y, 0);
            
            
            var vector3IntPos = Vector3Int.RoundToInt(mousePosVector3);

            Vector3Int[] settingTilePositions =
            {
                vector3IntPos + new Vector3Int(-1,1,0), // up left
                vector3IntPos + new Vector3Int(0,1,0), // up
                vector3IntPos + new Vector3Int(1,1,0), // up right
                
                vector3IntPos + new Vector3Int(-1,0,0), // mid left
                vector3IntPos, // mid
                vector3IntPos + new Vector3Int(1,0,0), // mid right
                
                vector3IntPos + new Vector3Int(-1,-1,0), // down left
                vector3IntPos + new Vector3Int(0,-1,0), // down
                vector3IntPos + new Vector3Int(1,-1,0), // down right

            };

            foreach (var t in settingTilePositions)
            {
                groundTilemap.SetTile(t,tile);
            }
            
        }
    }
    
}