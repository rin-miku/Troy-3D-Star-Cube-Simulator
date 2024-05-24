using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    public Transform rubikRoot;
    public Transform rotationRoot;
    public float rotationTime = 0.5f;

    public bool isRotation = false;
    private List<PieceBase> pieces = new List<PieceBase>();

    private OperationController operationController;

    void Start()
    {
        operationController = GameObject.Find("GameController").GetComponent<OperationController>();

        InitRubik();
        InitSurface();
        InitOperation();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ExecuteOperation(Operation.UpCW);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ExecuteOperation(Operation.UpCCW);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ExecuteOperation(Operation.DownCW);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ExecuteOperation(Operation.DownCCW);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ExecuteOperation(Operation.LeftCW);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ExecuteOperation(Operation.LeftCCW);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ExecuteOperation(Operation.RightCW);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ExecuteOperation(Operation.RightCCW);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ExecuteOperation(Operation.FrontCW);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ExecuteOperation(Operation.FrontCCW);
        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            ExecuteOperation(Operation.BehindCW);
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            ExecuteOperation(Operation.BehindCCW);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ExecuteOperation(Operation.XCW);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ExecuteOperation(Operation.XCCW);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ExecuteOperation(Operation.YCW);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ExecuteOperation(Operation.YCCW);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ExecuteOperation(Operation.ZCW);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ExecuteOperation(Operation.ZCCW);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetRubik();
        }
    }

    /// <summary>
    /// 初始化 Rubik
    /// </summary>
    private void InitRubik()
    {
        // 遍历所有块进行初始化
        pieces.AddRange(GetComponentsInChildren<PieceBase>());
        foreach(PieceBase piece in pieces)
        {
            piece.InitPiece();
        }
        // 需要射线检测的面片(patch)添加 MeshCollider 和 PatchMaterial 
        foreach(GameObject patch in GameObject.FindGameObjectsWithTag("Patch"))
        {
            patch.AddComponent<MeshCollider>();
            patch.AddComponent<PatchMaterial>();
        }
    }

    /// <summary>
    /// 初始化面(surface)数据
    /// </summary>
    private void InitSurface()
    {
        UpSurface.Init();
        DownSurface.Init();
        LeftSurface.Init();
        RightSurface.Init();
        FrontSurface.Init();
        BehindSurface.Init();
    }

    /// <summary>
    /// 初始化操作(operation)数据
    /// </summary>
    private void InitOperation()
    {
        UpCWOperation.Init();
        UpCCWOperation.Init();
        DownCWOperation.Init();
        DownCCWOperation.Init();
        LeftCWOperation.Init();
        LeftCCWOperation.Init();
        RightCWOperation.Init();
        RightCCWOperation.Init();
        FrontCWOperation.Init();
        FrontCCWOperation.Init();
        BehindCWOperation.Init();
        BehindCCWOperation.Init();
    }

    /// <summary>
    /// 执行具体旋转操作
    /// </summary>
    /// <param name="operation"></param>
    public void ExecuteOperation(Operation operation)
    {
        switch (operation)
        {
            case Operation.UpCW:
                RubikRotate(UpCWOperation.rotationEuler, UpSurface.piecesCoord,  UpSurface.cwRotationMatrix);
                break;
            case Operation.UpCCW:
                RubikRotate(UpCCWOperation.rotationEuler, UpSurface.piecesCoord, UpSurface.ccwRotationMatrix);
                break;
            case Operation.DownCW:
                RubikRotate(DownCWOperation.rotationEuler, DownSurface.piecesCoord,  DownSurface.cwRotationMatrix);
                break;
            case Operation.DownCCW:
                RubikRotate(DownCCWOperation.rotationEuler, DownSurface.piecesCoord, DownSurface.ccwRotationMatrix);
                break;
            case Operation.LeftCW:
                RubikRotate(LeftCWOperation.rotationEuler, LeftSurface.piecesCoord, LeftSurface.cwRotationMatrix);
                break;
            case Operation.LeftCCW:
                RubikRotate(LeftCCWOperation.rotationEuler, LeftSurface.piecesCoord, LeftSurface.ccwRotationMatrix);
                break;
            case Operation.RightCW:
                RubikRotate(RightCWOperation.rotationEuler, RightSurface.piecesCoord, RightSurface.cwRotationMatrix);
                break;
            case Operation.RightCCW:
                RubikRotate(RightCCWOperation.rotationEuler, RightSurface.piecesCoord, RightSurface.ccwRotationMatrix);
                break;
            case Operation.FrontCW:
                RubikRotate(FrontCWOperation.rotationEuler, FrontSurface.piecesCoord, BehindSurface.cwRotationMatrix);
                break;
            case Operation.FrontCCW:
                RubikRotate(FrontCCWOperation.rotationEuler, FrontSurface.piecesCoord, BehindSurface.ccwRotationMatrix);
                break;
            case Operation.BehindCW:
                RubikRotate(BehindCWOperation.rotationEuler, BehindSurface.piecesCoord, BehindSurface.cwRotationMatrix);
                break;
            case Operation.BehindCCW:
                RubikRotate(BehindCCWOperation.rotationEuler, BehindSurface.piecesCoord, BehindSurface.ccwRotationMatrix);
                break;
            case Operation.XCW:
                RubikRotate(RightCWOperation.rotationEuler, GetAllPiecesCoord(), RightSurface.cwRotationMatrix);
                break;
            case Operation.XCCW:
                RubikRotate(RightCCWOperation.rotationEuler, GetAllPiecesCoord(), RightSurface.ccwRotationMatrix);
                break;
            case Operation.YCW:
                RubikRotate(UpCWOperation.rotationEuler, GetAllPiecesCoord(), UpSurface.cwRotationMatrix);
                break;
            case Operation.YCCW:
                RubikRotate(UpCCWOperation.rotationEuler, GetAllPiecesCoord(), UpSurface.ccwRotationMatrix);
                break;
            case Operation.ZCW:
                RubikRotate(FrontCWOperation.rotationEuler, GetAllPiecesCoord(), FrontSurface.cwRotationMatrix);
                break;
            case Operation.ZCCW:
                RubikRotate(FrontCCWOperation.rotationEuler, GetAllPiecesCoord(), FrontSurface.ccwRotationMatrix);
                break;
        }
        Debug.Log($"执行操作 {operation}");
    }

    /// <summary>
    /// 统一做执行操作
    /// </summary>
    /// <param name="rotationEuler"></param>
    /// <param name="piecesCoord"></param>
    /// <param name="rotationMatrix"></param>
    private void RubikRotate(Vector3 rotationEuler, List<Vector3Int> piecesCoord,  Matrix4x4 rotationMatrix)
    {
        if (isRotation) return;
        isRotation = true;

        RotateSurface(piecesCoord, rotationEuler);
        RotateCoord(piecesCoord, rotationMatrix);
    }

    /// <summary>
    /// 先旋转模型
    /// </summary>
    /// <param name="piecesCoord"></param>
    /// <param name="eulers"></param>
    private void RotateSurface(List<Vector3Int> piecesCoord, Vector3 eulers)
    {
        // 先重置根节点
        rotationRoot.position = Vector3.zero;
        rotationRoot.localScale = Vector3.one;
        rotationRoot.rotation = new Quaternion(0, 0, 0, 0);
        // 同一面的方块放置到根节点下
        foreach (PieceBase piece in GetSurfacePieces(piecesCoord))
        {
            piece.transform.SetParent(rotationRoot);
        }
        // 旋转 完成后释放
        rotationRoot.DOLocalRotate(eulers, rotationTime)
            .SetEase(Ease.Linear)
            .OnComplete(() => 
            {
                foreach (PieceBase piece in GetSurfacePieces(piecesCoord))
                {

                    piece.transform.SetParent(rotationRoot.parent);
                }
                isRotation = false;
            });
    }

    /// <summary>
    /// 再旋转坐标
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
    /// 重置状态
    /// </summary>
    public void ResetRubik()
    {
        rubikRoot.rotation = new Quaternion(0f, 0f, 0f, 0f);

        foreach(GameObject patch in GameObject.FindGameObjectsWithTag("Patch"))
        {
            patch.GetComponent<MeshRenderer>().material = patch.GetComponent<PatchMaterial>().patchMaterial;
        }
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

    /// <summary>
    /// 获取所有块的坐标
    /// </summary>
    /// <returns></returns>
    private List<Vector3Int> GetAllPiecesCoord()
    {
        List<Vector3Int> piecesCoord = new List<Vector3Int>();
        foreach(PieceBase piece in pieces)
        {
            piecesCoord.Add(piece.coord);
        }

        return piecesCoord;
    }

    /// <summary>
    /// 获取随机状态
    /// </summary>
    public void RandomRubik()
    {
        List<Operation> randomOperations = new List<Operation>();
        for(int i = 0; i < 50; i++)
        {
            randomOperations.Add(GetRandomOperation());
        }

        operationController.UpdateOperationList(randomOperations);
    }

    /// <summary>
    /// 获取随机操作值
    /// </summary>
    /// <returns></returns>
    private Operation GetRandomOperation()
    {
        Operation[] values = (Operation[])System.Enum.GetValues(typeof(Operation));
        int index = Random.Range(0, values.Length - 1);

        return values[index];
    }
}
