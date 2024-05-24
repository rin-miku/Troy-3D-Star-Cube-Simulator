using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// z轴顺时针旋转 
/// 0 -1 0
/// 1 0 0
/// 0 0 1
/// z轴逆时针旋转
/// 0 1 0
/// -1 0 0
/// 0 0 1
/// </summary>
public static class FrontSurface
{
    public static Matrix4x4 cwRotationMatrix = new Matrix4x4();
    public static Matrix4x4 ccwRotationMatrix = new Matrix4x4();
    public static List<Vector3Int> piecesCoord = new List<Vector3Int>();

    public static void Init()
    {
        InitMatrix();
        InitPieces();
    }

    private static void InitMatrix()
    {
        cwRotationMatrix.SetRow(0, new Vector4(0, 1, 0, 0));
        cwRotationMatrix.SetRow(1, new Vector4(-1, 0, 0, 0));
        cwRotationMatrix.SetRow(2, new Vector4(0, 0, 1, 0));
        cwRotationMatrix.SetRow(3, new Vector4(0, 0, 0, 0));

        ccwRotationMatrix.SetRow(0, new Vector4(0, -1, 0, 0));
        ccwRotationMatrix.SetRow(1, new Vector4(1, 0, 0, 0));
        ccwRotationMatrix.SetRow(2, new Vector4(0, 0, 1, 0));
        ccwRotationMatrix.SetRow(3, new Vector4(0, 0, 0, 0));
    }

    private static void InitPieces()
    {
        piecesCoord.Add(new Vector3Int(0, 0, -1));

        piecesCoord.Add(new Vector3Int(-1, 1, -1));
        piecesCoord.Add(new Vector3Int(1, 1, -1));
        piecesCoord.Add(new Vector3Int(-1, -1, -1));
        piecesCoord.Add(new Vector3Int(1, -1, -1));

        piecesCoord.Add(new Vector3Int(0, 1, -1));
        piecesCoord.Add(new Vector3Int(0, -1, -1));
        piecesCoord.Add(new Vector3Int(-1, 0, -1));
        piecesCoord.Add(new Vector3Int(1, 0, -1));
    }
}
