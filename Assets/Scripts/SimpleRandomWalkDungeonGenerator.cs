using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField] protected SimpleRandomWalkData m_randomWalkParameters;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(m_randomWalkParameters, m_startPosition);
        m_tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, m_tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkData parameters, Vector2Int position)
    {
        Vector2Int currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < m_randomWalkParameters.m_iterations; i++)
        {
            HashSet<Vector2Int> path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, m_randomWalkParameters.m_walkLength);
            floorPositions.UnionWith(path);

            if (m_randomWalkParameters.m_startRandomlyEachIteration)
            {
                int randomNum = Random.Range(0, floorPositions.Count);
                int count = 0;
                foreach (Vector2Int floorPosition in floorPositions)
                {
                    if (count == randomNum)
                    {
                        currentPosition = position;
                        break;
                    }
                    count++;
                }
            }
        }

        return floorPositions;
    }
}
