using UnityEngine;

public static class RightCWOperation
{
    public static Operation operation;
    public static Vector3 rotationEuler;

    public static void Init()
    {
        operation = Operation.RightCW;
        rotationEuler = new Vector3(90f, 0f, 0f);
    }
}