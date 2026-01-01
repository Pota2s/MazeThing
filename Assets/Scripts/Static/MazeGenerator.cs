using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGenerator : MonoBehaviour
{
    public Vector2Int mazeSize = new (10, 10);
    private MazeGraph mazeGraph;
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private TileBase wall;
    [SerializeField] private TileBase floor;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject exitPrefab;

    private List<Cell> deadEnds;
    void Awake()
    {
        mazeGraph = new MazeGraph(mazeSize);
        deadEnds = new List<Cell>();
    }
    private void Start()
    {
        RenderMaze();
        Cell start = deadEnds[Random.Range(0,deadEnds.Count - 1)];
        Cell end = deadEnds[Random.Range(0, deadEnds.Count - 1)];
    
        Instantiate(playerPrefab, new Vector3(start.coordinates.x * 2 + 0.5f, start.coordinates.y * 2 + 0.5f, 0), Quaternion.identity);
        Instantiate(exitPrefab, new Vector3(end.coordinates.x * 2 + 0.5f, end.coordinates.y * 2 + 0.5f, 0), Quaternion.identity);
    }

    void RenderMaze()
    {
        CreateFloor();
        CreateWalls();
        CarveWalls();
    }

    void CreateFloor()
    {
        for (int x = 0; x < mazeSize.x * 2; x++)
        {
            for (int y = 0; y < mazeSize.y * 2; y++)
            {
                Vector3Int floorPosition = new (x, y, 0);
                floorTilemap.SetTile(floorPosition, floor);
            }
        }
    }

    void CreateWalls()
    {
        var cells = mazeGraph.cells.Values;
        foreach (Cell cell in cells)
        {
            Vector2Int tileCoordinates = cell.coordinates * 2;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;
                    Vector3Int wallPosition = new (tileCoordinates.x + x, tileCoordinates.y + y, 0);
                    wallTilemap.SetTile(wallPosition, wall);
                }
            }
        }
    }

    void CarveWalls()
    {
        var cells = mazeGraph.cells.Values;
        foreach (Cell cell in cells)
        {
            Vector2Int tileCoordinates = cell.coordinates * 2;
            Vector3Int cellPosition = new (tileCoordinates.x, tileCoordinates.y, 0);  
            if (cell.connectedNeighbours.Count == 1)
            {
                deadEnds.Add(cell);
            }
            foreach (Cell neighbor in cell.connectedNeighbours)
            {
                Vector2Int direction = neighbor.coordinates - cell.coordinates;
                Vector2Int wallPositionOffset = direction;
                Vector3Int wallPosition = new (tileCoordinates.x + wallPositionOffset.x, tileCoordinates.y + wallPositionOffset.y, 0);
                wallTilemap.SetTile(wallPosition, null);
            }
        }
    }

}
