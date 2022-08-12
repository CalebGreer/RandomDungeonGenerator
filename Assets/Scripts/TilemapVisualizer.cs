using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap m_floorTilemmap;
    [SerializeField] private Tilemap m_wallTilemap;
    [SerializeField] private TileBase m_floorTile;

    [Header("Side Wall Tiles")]
    [SerializeField] private TileBase m_wallTopTile;
    [SerializeField] private TileBase m_wallSideRightTile;
    [SerializeField] private TileBase m_wallSideLeftTile;
    [SerializeField] private TileBase m_wallBottomTile;
    [SerializeField] private TileBase m_wallFullTile;

    [Header("Corner Wall Tiles")]
    [SerializeField] private TileBase m_wallInnerCornerDownLeftTile;
    [SerializeField] private TileBase m_wallInnerCornerDownRightTile;
    [SerializeField] private TileBase m_wallIDiagonalCornerDownRightTile;
    [SerializeField] private TileBase m_wallIDiagonalCornerDownLeftTile;
    [SerializeField] private TileBase m_wallIDiagonalCornerUpRightTile;
    [SerializeField] private TileBase m_wallIDiagonalCornerUpLeftTile;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintFloorTiles(floorPositions, m_floorTilemmap, m_floorTile);
    }

    private void PaintFloorTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (Vector2Int position in positions)
        {
            PaintSingleTile(position, tilemap, tile);
        }
    }

    public void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            tile = m_wallTopTile;
        }
        else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
        {
            tile = m_wallSideRightTile;
        }
        else if (WallTypesHelper.wallBottom.Contains(typeAsInt))
        {
            tile = m_wallBottomTile;
        }
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = m_wallSideLeftTile;
        }
        else if (WallTypesHelper.wallFull.Contains(typeAsInt))
        {
            tile = m_wallFullTile;
        }

        if (tile != null)
        {
            PaintSingleTile(position, m_wallTilemap, tile);
        }
    }

    public void PaintSingleCornerWall(Vector2Int position, string neighboursBinaryType)
    {
        int typeAsInt = Convert.ToInt32(neighboursBinaryType, 2);
        TileBase tile = null;

        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = m_wallInnerCornerDownLeftTile;
        }
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = m_wallInnerCornerDownRightTile;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = m_wallIDiagonalCornerDownLeftTile;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = m_wallIDiagonalCornerDownRightTile;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = m_wallIDiagonalCornerUpLeftTile;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = m_wallIDiagonalCornerUpRightTile;
        }
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = m_wallFullTile;
        }
        else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
        {
            tile = m_wallBottomTile;
        }

        if (tile != null)
        {
            PaintSingleTile(position, m_wallTilemap, tile);
        }
    }

    private void PaintSingleTile(Vector2Int position, Tilemap tilemap, TileBase tile)
    {
        Vector3Int tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        m_floorTilemmap.ClearAllTiles();
        m_wallTilemap.ClearAllTiles();
    }
}
