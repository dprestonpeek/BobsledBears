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

    [SerializeField]
    Transform firstTile;

    [SerializeField]
    Transform secondTile;

    Vector3 firstTilePos = Vector3.zero;
    Vector3 firstTileRot = Vector3.zero;
    Vector3 firstTileSca = Vector3.zero;
    Vector3 nTileAdjustment = Vector3.zero;

    bool placedObstacle = false;
    int blanksPlaced = 0;

    // Start is called before the first frame update
    void Start()
    {
        firstTilePos = firstTile.transform.position;
        firstTileRot = firstTile.transform.eulerAngles;
        firstTileSca = firstTile.transform.localScale;
        nTileAdjustment = secondTile.position - firstTile.position;

        //These tiles are blank because the player is still in the air from the jump
        for (int i = 0; i < 4; i++)
        {
            PlaceBlankTile(i);
        }
        //Place the obstacle tiles with n blanks in between
        for (int i = 4; i < maxTiles; i++)
        {
            if (i == maxTiles / 2)
            {
                blankTilesBetweenObstacles--;
            }
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
