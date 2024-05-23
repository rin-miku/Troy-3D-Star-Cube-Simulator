using UnityEngine;

public static class LeftCCWOperation
{
    public static Operation operation;
    public static Vector3 rotationEuler;

    public static void Init()
    {
        operation = Operation.LeftCCW;
        rotationEuler = new Vector3(90f, 0f, 0f);
    }
}
