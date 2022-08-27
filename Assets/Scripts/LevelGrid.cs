using System;
using System.Collections.Concurrent;
using System.Timers;
using TMPro;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    private static LevelGrid instance;
    [SerializeField] private Transform gridDebugObjectPrefab;
    private readonly ConcurrentQueue<Action> debugQueue = new();
    private GridSystem gridSystem;

    private void Awake()
    {
        instance = this;
        int width = 50;
        int height = 50;
        gridSystem = new GridSystem(width, height, 2);
        DrawGridDebug(width, height);
    }

    private void Update()
    {
        foreach (var action in debugQueue) action.Invoke();
    }

    private void DrawGridDebug(int width, int height)
    {
        for (var x = 0; x < width; x++)
        for (var z = 0; z < height; z++)
        {
            var gridPosition = new GridPosition(x, z);
            var gridObject = gridSystem.GetGridObject(gridPosition);
            var debugInstance = Instantiate(gridDebugObjectPrefab, gridSystem.GetWorldPosition(gridPosition), Quaternion.identity);
            var debugText = debugInstance.GetComponentInChildren<TextMeshPro>();
            debugInstance.gameObject.SetActive(false);
            gridObject.SetDebugAction(() => debugQueue.Enqueue(() =>
            {
                debugInstance.gameObject.SetActive(gridObject.GetUnit() != null);
                debugText.text = gridPosition + "\n" + gridObject.GetUnit();
            }));
        }
    }

    public static void SetUnitAtGridPosition(Vector3 position, Unit unit)
    {
        instance.gridSystem.GetGridObject(GetGridPosition(position)).SetUnit(unit);
    }

    public static Unit GetUnitAtGridPosition(Vector3 position)
    {
        return instance.gridSystem.GetGridObject(GetGridPosition(position)).GetUnit();
    }

    public static void ClearUnitAtGridPosition(Vector3 position)
    {
        instance.gridSystem.GetGridObject(GetGridPosition(position)).SetUnit(null);
    }

    private static GridPosition GetGridPosition(Vector3 position)
    {
        return instance.gridSystem.GetGridPosition(position);
    }
}