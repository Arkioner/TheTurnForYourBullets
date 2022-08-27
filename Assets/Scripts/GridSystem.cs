using System;
using UnityEngine;

public class GridSystem
{
    private readonly float cellSize;
    private readonly GridObject[,] gridObjects;
    private readonly int height;
    private readonly int width;

    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        gridObjects = new GridObject[width, height];

        for (var x = 0; x < width; x++)
        for (var z = 0; z < height; z++)
        {
            var gp = new GridPosition(x, z);
            gridObjects[x, z] = new GridObject(this, gp);
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
    private readonly GridPosition gridPosition;
    private readonly GridSystem gridSystem;
    private Action debugAction;
    private Unit unit;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public void SetDebugAction(Action debugAction)
    {
        this.debugAction = debugAction;
    }

    public void SetUnit(Unit unit)
    {
        this.unit = unit;
        debugAction?.Invoke();
    }

    public Unit GetUnit()
    {
        return this.unit;
    }
}