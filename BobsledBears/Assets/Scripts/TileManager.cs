using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1000)]
    int maxTiles = 500;

    [SerializeField]
    [Range(0, 4)]
    int blankTilesBetweenObstacles = 1;

    [SerializeField]
    List<GameObject> tiles = new List<GameObject>();

    [SerializeField]
    List<GameObject> blankTiles = new List<GameObject>();

    [SerializeField]
    List<GameObject> placedTiles = new List<GameObject>();

    Vector3 firstTilePos = new Vector3(425, -314, -419);
    Vector3 firstTileRot = new Vector3(239.854f, -45, -90);
    Vector3 firstTileSca = new Vector3(3600, 3600, 5701);
    Vector3 nTileAdjustment = new Vector3(222, -182, -222);

    bool placedObstacle = false;
    int blanksPlaced = 0;

    // Start is called before the first frame update
    void Start()
    {
        //These tiles are blank because the player is still in the air from the jump
        for (int i = 0; i < 4; i++)
        {
            PlaceBlankTile(i);
        }
        //Place the obstacle tiles with n blanks in between
        for (int i = 4; i < maxTiles; i++)
        {
            if (placedObstacle)
            {
                PlaceBlankTile(i);
                blanksPlaced++;
                if (blanksPlaced == blankTilesBetweenObstacles)
                {
                    placedObstacle = false;
                    blanksPlaced = 0;
                }
            }
            else
            {
                PlaceObstacleTile(i);
                placedObstacle = true;
            }
            //TODO: Log the obstacles in a way that can be referenced and checked easily
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void PlaceBlankTile(int i)
    {
        int pick = Random.Range(0, blankTiles.Count - 1);
        GameObject newTile = Instantiate(blankTiles[pick]);
        newTile.transform.position = firstTilePos + (nTileAdjustment * i);
        newTile.transform.eulerAngles = firstTileRot;
        newTile.transform.localScale = firstTileSca;
        placedTiles.Add(newTile);
    }

    void PlaceObstacleTile(int i)
    {
        int pick = Random.Range(0, tiles.Count - 1);
        GameObject newTile = Instantiate(tiles[pick]);
        newTile.transform.position = firstTilePos + (nTileAdjustment * i);
        newTile.transform.eulerAngles = firstTileRot;
        newTile.transform.localScale = firstTileSca;
        placedTiles.Add(newTile);
    }
}
