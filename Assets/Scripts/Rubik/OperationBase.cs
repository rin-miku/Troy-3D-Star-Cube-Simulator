using UnityEngine;

public enum Operation
{
    UpCW,
    UpCCW,
    DownCW,
    DownCCW,
    LeftCW,
    LeftCCW,
    RightCW,
    RightCCW,
    FrontCW,
    FrontCCW,
    BehindCW,
    BehindCCW,
    XCW,
    XCCW,
    YCW,
    YCCW,
    ZCW,
    ZCCW,
    Error
}

public abstract class OperationBase
{
    public static Operation operation;
    public static Vector3 rotationEuler;

    public void Init()
    {
        InitOperation();
        InitRotationEuler();
    }

    public abstract void InitOperation();

    public abstract void InitRotationEuler();
}
