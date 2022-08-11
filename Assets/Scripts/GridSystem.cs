using System;
using System.Timers;
using TMPro;
using UnityEngine;

public class GridSystem
{
    private readonly int width;
    private readonly int height;
    private readonly float cellSize;
    private readonly GridObject[,] gridObjects;

    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        gridObjects = new GridObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gp = new GridPosition(x, z);
                gridObjects[x, z] = new GridObject(this, gp);
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjects[gridPosition.x, gridPosition.z];
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0.01f, gridPosition.z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition((int)Math.Round(worldPosition.x / cellSize), (int)Math.Round(worldPosition.z / cellSize));
    }
}

public readonly struct GridPosition
{
    public readonly int x;
    public readonly int z;

    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public override string ToString()
    {
        return $"({x},{z})";
    }
}

public class GridObject
{
    public Unit unit;
    private readonly GridSystem gridSystem;
    private readonly GridPosition gridPosition;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }
}