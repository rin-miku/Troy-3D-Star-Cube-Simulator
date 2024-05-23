using UnityEngine;

public static class FrontCWOperation
{
    public static Operation operation;
    public static Vector3 rotationEuler;

    public static void Init()
    {
        operation = Operation.FrontCW;
        rotationEuler = new Vector3(0f, 0f, -90f);
    }
}
