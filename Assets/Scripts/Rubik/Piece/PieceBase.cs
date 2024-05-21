using UnityEngine;

public abstract class PieceBase : MonoBehaviour
{
    public Vector3Int coord;

    private void Awake()
    {
        InitPiece();
    }

    public Vector3Int GetPieceCoord()
    {
        return coord;
    }

    public void SetPieceCoord(Vector3Int vector3Int)
    {
        coord = vector3Int;
    }

    public abstract void InitPiece();
}
