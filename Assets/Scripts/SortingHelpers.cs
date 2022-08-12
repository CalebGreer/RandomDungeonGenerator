using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class SortingHelpers
{
    private static System.Random rng;
    public static void ShuffleVector2IntList(List<Vector2Int> list)
    {
        rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Vector2Int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
