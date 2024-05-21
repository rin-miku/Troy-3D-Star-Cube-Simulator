using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SurfaceBase
{
    public static readonly Matrix4x4 xRotationMatrix = new Matrix4x4();
    public static readonly Matrix4x4 yRotationMatrix = new Matrix4x4();
    public static readonly Matrix4x4 zRotationMatrix = new Matrix4x4();
    public static readonly Matrix4x4 cwRotationMatrix = new Matrix4x4();
    public static readonly Matrix4x4 ccwRotationMatrix = new Matrix4x4();
    public static readonly List<Vector3Int> piecesCoord = new List<Vector3Int>();

    public void Init()
    {
        InitMatrix();
        InitPieces();
    }

    public abstract void InitMatrix();

    public abstract void InitPieces();
}
