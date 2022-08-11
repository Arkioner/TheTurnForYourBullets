using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Timers;
using TMPro;
using UnityEngine;
using Object = System.Object;

public class LevelGrid : MonoBehaviour
{
    private static LevelGrid instance;
    [SerializeField] private Transform gridDebugObjectPrefab;
    private GridSystem gridSystem;
    private ConcurrentQueue<Action> debugQueue = new();

    private void Awake()
    {
        instance = this;
        gridSystem = new GridSystem(10, 10, 2);
        DrawGridDebug();
    }

    private void Update()
    {
        foreach (var action in debugQueue)
        {
            action.Invoke();
        }
    }

    private void DrawGridDebug()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                GridObject gridObject = gridSystem.GetGridObject(gridPosition);
                Transform debugInstance = GameObject.Instantiate(gridDebugObjectPrefab, gridSystem.GetWorldPosition(gridPosition), Quaternion.identity);
                TextMeshPro debugText = debugInstance.GetComponentInChildren<TextMeshPro>();
                Timer timer = new Timer(100);
                timer.AutoReset = true;
                timer.Elapsed += (_, _) => debugQueue.Enqueue(() => debugText.text = gridPosition + "\n" + gridObject.unit);
                timer.Enabled = true;
            }
        }
    }

    public static void SetUnitAtGridPosition(Vector3 position, Unit unit)
    {
        instance.gridSystem.GetGridObject(GetGridPosition(position)).unit = unit;
    }

    public static Unit GetUnitAtGridPosition(Vector3 position) =>
        instance.gridSystem.GetGridObject(GetGridPosition(position)).unit;

    public static void ClearUnitAtGridPosition(Vector3 position)
    {
        instance.gridSystem.GetGridObject(GetGridPosition(position)).unit = null;
    }

    private static GridPosition GetGridPosition(Vector3 position) => instance.gridSystem.GetGridPosition(position);
}