using UnityEngine;

public static class UpCCWOperation
{
    public static Operation operation;
    public static Vector3 rotationEuler;

    public static void Init()
    {
        operation = Operation.UpCCW;
        rotationEuler = new Vector3(0f, -90f, 0f);
    }
}
