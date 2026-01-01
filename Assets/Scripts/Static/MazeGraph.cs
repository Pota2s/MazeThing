using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Cell
{
	public Vector2Int coordinates;
	public HashSet<Cell> neighbours;
	public HashSet<Cell> connectedNeighbours;

	public Cell(int x, int y)
	{
		coordinates = new Vector2Int(x, y);
		neighbours = new HashSet<Cell>();
		connectedNeighbours = new HashSet<Cell>();
	}

	public void DeclareNeighbourhood(Cell otherCell)
	{
		neighbours.Add(otherCell);
		otherCell.neighbours.Add(this);
	}

	public Vector2Int ToOtherCell(Cell otherCell)
	{
		return otherCell.coordinates - coordinates;
	}

	public void Connect(Cell otherCell)
	{
		connectedNeighbours.Add(otherCell);
		otherCell.connectedNeighbours.Add(this);
	}
}

public class MazeGraph
{
	public Dictionary<Vector2Int, Cell> cells;
	public Vector2Int size;
    
	public MazeGraph(Vector2Int size)
	{
		cells = new Dictionary<Vector2Int, Cell>();
		this.size = size;
		GenerateMap(size);
		GenerateConnections();
    }

    public MazeGraph(int width, int height)
	{
		cells = new Dictionary<Vector2Int, Cell>();
		size = new Vector2Int(width, height);
        GenerateMap(size);
		GenerateConnections();
    }

	public void GenerateMap(int width, int height)
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				Cell currentCell = GetOrCreateCell(x, y);
				if (x > 0)
				{
					Cell leftCell = GetOrCreateCell(x - 1, y);
					currentCell.DeclareNeighbourhood(leftCell);
				}
				if (y > 0)
				{
					Cell bottomCell = GetOrCreateCell(x, y - 1);
					currentCell.DeclareNeighbourhood(bottomCell);
				}
			}
		}
    }

	public void GenerateMap(Vector2Int size)
	{
		GenerateMap(size.x, size.y);
    }

    public void GenerateConnections(int startX = 0,int startY = 0)
	{
		Cell current = cells[new Vector2Int(startX, startY)];
		HashSet<Cell> visited = new HashSet<Cell>();
		Stack<Cell> stack = new Stack<Cell>();

		stack.Push(current);
		Random.InitState(System.DateTime.Now.Millisecond);
		
		while (stack.Count > 0)
		{
			current = stack.Pop();
            visited.Add(current);
			
			var possible = current.neighbours.Except(visited).ToList();
			if (possible.Count == 0)
			{
				continue;
			}

			Cell next = possible[Random.Range(0, possible.Count())];
			current.Connect(next);
			
			stack.Push(current);
			stack.Push(next);

        }

    }

	public void GenerateConnections(Vector2Int start)
	{
		GenerateConnections(start.x, start.y);
    }

	public void GenerateConnections()
	{
		GenerateConnections(Random.Range(0, size.x - 1), Random.Range(0, size.y - 1));
    }

    public Cell GetOrCreateCell(int x, int y)
	{
		Vector2Int coords = new Vector2Int(x, y);
		if (!cells.ContainsKey(coords))
		{
			cells[coords] = new Cell(x, y);
		}
		return cells[coords];
	}
}
