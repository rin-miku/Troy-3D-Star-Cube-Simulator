using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 左手系(0,0,0)为中心块 通过旋转矩阵计算旋转后的坐标
/// U (0,1,0)       (1,1,1) (-1,1,1) (1,1,-1) (-1,1,-1)         (0,1,1) (0,1,-1) (1,1,0) (-1,1,0)
/// D (0,-1,0)      (-1,-1,-1) (1,-1,1) (-1,-1,1) (1,-1,-1)     (0,-1,-1) (0,-1,1) (-1,-1,0) (1,-1,0)
/// L (-1,0,0)      (-1,1,1) (-1,1,-1) (-1,-1,-1) (-1,-1,1)     (-1,-1,0) (-1,1,0) (-1,0,1) (-1,0,-1)
/// R (1,0,0)       (1,1,1) (1,1,-1) (1,-1,1) (1,-1,-1)         (1,-1,0) (1,1,0) (1,0,-1) (1,0,1)
/// F (0,0,-1)      (-1,1,-1) (1,1,-1) (-1,-1,-1) (1,-1,-1)     (0,1,-1) (0,-1,-1) (-1,0,-1) (1,0,-1)
/// B (0,0,1)       (-1,1,1) (1,1,1) (1,-1,1) (-1,-1,1)         (0,-1,1) (0,1,1) (-1,0,1) (1,0,1)
/// </summary>
public class Rubik : MonoBehaviour
{
    public Transform rotationRoot;

    private Ray ray;
    private RaycastHit hit;
    
    private List<PieceBase> pieces = new List<PieceBase>();

    void Start()
    {
        
    }

    void Update()
    {
        CheckCenterPiece();
    }

    /// <summary>
    /// 遍历所有块进行初始化
    /// </summary>
    private void InitRubik()
    {
        pieces.AddRange(GetComponentsInChildren<PieceBase>());
    }

    /// <summary>
    /// 射线检测 获取中心块
    /// </summary>
    private void CheckCenterPiece()
    {
        if (Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.parent.parent.name);
            }
        }
    }

    private void ExecuteOperation(OperationBase operation)
    {
        switch (operation)
        {
            case UpCWOperation:
                RotateCoord(UpSurface.piecesCoord, UpSurface.cwRotationMatrix);
                RotateSurface(UpSurface.piecesCoord, UpCWOperation.rotationEuler);
                break;
        }
    }

    /// <summary>
    /// 坐标先旋转
    /// </summary>
    /// <param name="piecesCoord"></param>
    /// <param name="matrix"></param>
    private void RotateCoord(List<Vector3Int> piecesCoord, Matrix4x4 matrix)
    {
        foreach (PieceBase piece in GetSurfacePieces(piecesCoord))
        {
            piece.SetPieceCoord(Vector3Int.RoundToInt(matrix.MultiplyPoint3x4(piece.coord)));
        }
    }

    /// <summary>
    /// 旋转模型
    /// </summary>
    /// <param name="piecesCoord"></param>
    /// <param name="matrix"></param>
    private void RotateSurface(List<Vector3Int> piecesCoord, Vector3 eulers)
    {
        // 先重置根节点
        rotationRoot.position = Vector3.zero;
        rotationRoot.localScale = Vector3.one;
        rotationRoot.rotation = new Quaternion(0,0,0,0);
        // 同一面的方块放置到根节点下
        foreach(PieceBase piece in GetSurfacePieces(piecesCoord))
        {
            piece.transform.SetParent(rotationRoot);
        }
        // 旋转
        rotationRoot.Rotate(eulers);
    }

    /// <summary>
    /// 获取指定面的所有方块
    /// </summary>
    /// <param name="piecesCoord"></param>
    /// <returns></returns>
    private List<PieceBase> GetSurfacePieces(List<Vector3Int> piecesCoord)
    {
        List<PieceBase> pieces = new List<PieceBase>();
        foreach (PieceBase piece in transform.GetComponentsInChildren<PieceBase>())
        {
            if (piecesCoord.Contains(piece.coord))
            {
                pieces.Add(piece);
            }
        }
        return pieces;
    }
}
