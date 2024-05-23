using UnityEngine;

public static class LeftCWOperation
{
    public static Operation operation;
    public static Vector3 rotationEuler;

    public static void Init()
    {
        operation = Operation.LeftCW;
        rotationEuler = new Vector3(-90f, 0f, 0f);
    }
}
