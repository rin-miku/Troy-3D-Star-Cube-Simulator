using UnityEngine;

public static class DownCCWOperation
{
    public static Operation operation;
    public static Vector3 rotationEuler;

    public static void Init()
    {
        operation = Operation.DownCCW;
        rotationEuler = new Vector3(0f, 90f, 0f);
    }
}
