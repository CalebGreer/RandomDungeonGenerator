using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] private int m_corridorLength = 14;
    [SerializeField] private int m_corridorCount = 5;
    [SerializeField, Range(0.1f, 1f)] private float m_roomPercent = 0.8f;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstDungeonGeneration();
    }

    private void CorridorFirstDungeonGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        m_tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, m_tilemapVisualizer);
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        Vector2Int currentPosition = m_startPosition;
        potentialRoomPositions.Add(currentPosition);

        for (int i = 0; i < m_corridorCount; i++)
        {
            List<Vector2Int> corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, m_corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * m_roomPercent);

        List<Vector2Int> randomizedRoomPositions = new List<Vector2Int>(potentialRoomPositions);
        SortingHelpers.ShuffleVector2IntList(randomizedRoomPositions);

        for (int i = 0; i < roomToCreateCount; i++)
        {
            HashSet<Vector2Int> roomFloor = RunRandomWalk(m_randomWalkParameters, randomizedRoomPositions[i]);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();

        foreach (Vector2Int position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (Vector2Int direction in Direction2D.cardinalDirectionList)
            {
                if (floorPositions.Contains(position + direction))
                {
                    neighboursCount++;
                }
            }

            if (neighboursCount == 1)
            {
                deadEnds.Add(position);
            }
        }

        return deadEnds;
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (Vector2Int position in deadEnds)
        {
            if (roomFloors.Contains(position) == false)
            {
                HashSet<Vector2Int> roomFloor = RunRandomWalk(m_randomWalkParameters, position);
                roomFloors.UnionWith(roomFloor);
            }
        }
    }
}
