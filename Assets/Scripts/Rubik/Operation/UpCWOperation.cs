public class UpCWOperation : OperationBase
{
    public override void InitOperation()
    {
        operation = Operation.UpCW;
    }

    public override void InitRotationEuler()
    {
        rotationEuler = new UnityEngine.Vector3(0f, 90f, 0f);
    }
}
