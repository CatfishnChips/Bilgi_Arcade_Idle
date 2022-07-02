using UnityEngine;

public struct SpawnPointData
{
    public SpawnPointData(Vector3 position, float distance) {
        Position = position;
        Distance = distance;
    }

    public Vector3 Position;
    public float Distance;
}
