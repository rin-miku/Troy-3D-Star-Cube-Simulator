using UnityEngine;

public static class BehindCWOperation
{
    public static Operation operation;
    public static Vector3 rotationEuler;

    public static void Init()
    {
        operation = Operation.BehindCW;
        rotationEuler = new Vector3(0f, 0f, -90f);
    }
}
