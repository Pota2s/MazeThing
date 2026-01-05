using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGenerator : MonoBehaviour
{
    private Vector2Int mazeSize;
    private MazeGraph mazeGraph;
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private TileBase wall;
    [SerializeField] private TileBase floor;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject exitPrefab;
    [SerializeField] private List<GameObject> enemyPrefabs;

    private List<Cell> deadEnds;

    private void Start()
    {
        mazeSize = GameState.Instance.mazeSize;
        mazeGraph = new MazeGraph(mazeSize);
        deadEnds = new List<Cell>();

        RenderMaze();
        Cell start = deadEnds[Random.Range(0,deadEnds.Count)];
        Cell end;
        do
        {
            end = deadEnds[Random.Range(0, deadEnds.Count)];
        } while (start == end);

        Instantiate(playerPrefab, new Vector3(start.coordinates.x * 2, start.coordinates.y * 2, 0), Quaternion.identity);
        Instantiate(exitPrefab, new Vector3(end.coordinates.x * 2, end.coordinates.y * 2, 0), Quaternion.identity);
        SpawnEnemies();
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
        deadEnds.Clear();
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

    void SpawnEnemies()
    {
        if (enemyPrefabs.Count == 0) return;

        var cells = mazeGraph.cells.Values.ToList();

        for (int i = cells.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (cells[i], cells[j]) = (cells[j], cells[i]);
        }

        int min = Mathf.RoundToInt(cells.Count * 0.05f) + 1;
        int max = Mathf.RoundToInt(cells.Count * 0.1f) + 1;

        var enemyCells = cells.Take(Random.Range(min,max));

        foreach(Cell cell in enemyCells)
        {
            Instantiate(
                enemyPrefabs[Random.Range(0, enemyPrefabs.Count)],
                new Vector3(cell.coordinates.x * 2,cell.coordinates.y * 2 + 0.5f,0),
                Quaternion.identity);
        }

    }

}
