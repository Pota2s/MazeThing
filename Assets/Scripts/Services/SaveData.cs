using System;
using UnityEngine;

[Serializable]
public class SaveData
{
	public int version;
	public Vector2Int largestMazeSolved;
	public int mazesSolved;

	public SaveData(Vector2Int largestMazeSolved, int mazesSolved, int version = 0)
    {
        this.version = version;
        this.largestMazeSolved = largestMazeSolved;
        this.mazesSolved = mazesSolved;
    }

    public SaveData(int version = 0)
    {
        this.version = version;
        this.largestMazeSolved = new(0, 0);
        this.mazesSolved = 0;
    }
}
