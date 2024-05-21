using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// y轴顺时针旋转 
/// 0 0 1
/// 0 1 0
/// -1 0 0
/// y轴逆时针旋转
/// 0 0 -1
/// 0 1 0
/// 1 0 0
/// </summary>
public class UpSurface : SurfaceBase
{
    public override void InitMatrix()
    {
        cwRotationMatrix.SetRow(0, new Vector4(0, 0, 1, 0));
        cwRotationMatrix.SetRow(1, new Vector4(0, 1, 0, 0));
        cwRotationMatrix.SetRow(2, new Vector4(-1, 0, 0, 0));
        cwRotationMatrix.SetRow(3, new Vector4(0, 0, 0, 0));

        ccwRotationMatrix.SetRow(0, new Vector4(0, 0, -1, 0));
        ccwRotationMatrix.SetRow(1, new Vector4(0, 1, 0, 0));
        ccwRotationMatrix.SetRow(2, new Vector4(1, 0, 0, 0));
        ccwRotationMatrix.SetRow(3, new Vector4(0, 0, 0, 0));
    }

    public override void InitPieces()
    {
        piecesCoord.Add(new Vector3Int(0, 1, 0));

        piecesCoord.Add(new Vector3Int(1, 1, 1));
        piecesCoord.Add(new Vector3Int(-1, 1, 1));
        piecesCoord.Add(new Vector3Int(1, 1, -1));
        piecesCoord.Add(new Vector3Int(-1, 1, -1));

        piecesCoord.Add(new Vector3Int(0, 1, 1));
        piecesCoord.Add(new Vector3Int(0, 1, -1));
        piecesCoord.Add(new Vector3Int(1, 1, 0));
        piecesCoord.Add(new Vector3Int(-1, 1, 0));
    }
}
