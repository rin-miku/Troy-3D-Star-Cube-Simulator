using UnityEngine;

public static class UpCWOperation
{
    public static Operation operation;
    public static Vector3 rotationEuler;

    public static void Init()
    {
        operation = Operation.UpCW;
        rotationEuler = new Vector3(0f, 90f, 0f);
    }
}
