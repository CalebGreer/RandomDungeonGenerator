using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_", menuName = "PCG/Simple Random Walk Data")]
public class SimpleRandomWalkData : ScriptableObject
{
    public int m_iterations = 10;
    public int m_walkLength = 10;
    public bool m_startRandomlyEachIteration = true;
}
