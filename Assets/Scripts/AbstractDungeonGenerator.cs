using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [Header("Dungeon Generator Properties")]
    [SerializeField] protected TilemapVisualizer m_tilemapVisualizer = null;
    [SerializeField] protected Vector2Int m_startPosition = Vector2Int.zero;

    public void GenerateDungeon()
    {
        m_tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}
