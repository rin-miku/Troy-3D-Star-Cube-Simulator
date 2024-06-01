using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class OperationController : MonoBehaviour
{
    private Dictionary<string, Operation> operationName = new Dictionary<string, Operation>();
    private Dictionary<Operation, string> operationType = new Dictionary<Operation, string>();
    private LinkedList<Operation> operationList = new LinkedList<Operation>();
    private LinkedListNode<Operation> operationNode = null;
    private LinkedListNode<Operation> addOperationNode = null;

    private UIController uiController;
    private Rubik rubik;

    private void Start()
    {
        uiController = GameObject.Find("UI").GetComponent<UIController>();
        rubik = GameObject.Find("Rubik").GetComponent<Rubik>();

        InitOperation();
    }

    private void Update()
    {
        if (uiController.isAutoExecute)
        {
            DoOperation();
        }
    }

    private void InitOperation()
    {
        // 初始化操作名
        operationName.Add("x", Operation.XCW);
        operationName.Add("x'", Operation.XCCW);
        operationName.Add("y", Operation.YCW);
        operationName.Add("y'", Operation.YCCW);
        operationName.Add("z", Operation.ZCW);
        operationName.Add("z'", Operation.ZCCW);
        operationName.Add("U", Operation.UpCW);
        operationName.Add("U'", Operation.UpCCW);
        operationName.Add("D", Operation.DownCW);
        operationName.Add("D'", Operation.DownCCW);
        operationName.Add("L", Operation.LeftCW);
        operationName.Add("L'", Operation.LeftCCW);
        operationName.Add("R", Operation.RightCW);
        operationName.Add("R'", Operation.RightCCW);
        operationName.Add("F", Operation.FrontCW);
        operationName.Add("F'", Operation.FrontCCW);
        operationName.Add("B", Operation.BehindCW);
        operationName.Add("B'", Operation.BehindCCW);
        operationName.Add("", Operation.Error);
        // 初始化操作类型
        operationType.Add(Operation.XCW, "x");
        operationType.Add(Operation.XCCW, "x'");
        operationType.Add(Operation.YCW, "y");
        operationType.Add(Operation.YCCW, "y'");
        operationType.Add(Operation.ZCW, "z");
        operationType.Add(Operation.ZCCW, "z'");
        operationType.Add(Operation.UpCW, "U");
        operationType.Add(Operation.UpCCW, "U'");
        operationType.Add(Operation.DownCW, "D");
        operationType.Add(Operation.DownCCW, "D'");
        operationType.Add(Operation.LeftCW, "L");
        operationType.Add(Operation.LeftCCW, "L'");
        operationType.Add(Operation.RightCW, "R");
        operationType.Add(Operation.RightCCW, "R'");
        operationType.Add(Operation.FrontCW, "F");
        operationType.Add(Operation.FrontCCW, "F'");
        operationType.Add(Operation.BehindCW, "B");
        operationType.Add(Operation.BehindCCW, "B'");
        operationType.Add(Operation.Error, "");
        // 初始化一个头结点
        operationNode = new LinkedListNode<Operation>(Operation.Error);
        operationList.AddFirst(operationNode);
        addOperationNode = operationNode;
    }

    public void UpdateOperation(Operation operation)
    {
        operationList.AddAfter(addOperationNode, operation);
        addOperationNode = addOperationNode.Next;
  
        UpdateOperationsText();
    }

    public void UpdateOperationList(List<Operation> operations)
    {
        LinkedListNode<Operation> tempNode = operationNode;
        foreach(Operation operation in operations)
        {
            UpdateOperation(operation);
            tempNode = tempNode.Next;
        }
    }

    public void DoOperation()
    {
        if (rubik.isRotation) return;
        // 寻找下一个结点
        if(operationNode.Next == null)
        {
            uiController.OnClickAutoExecuteHandler();
            Utils.PrintLog("没有下一步操作啦", true);
        }
        else
        {
            // 有下一个节点就执行操作
            operationNode = operationNode.Next;
            addOperationNode = operationNode;
            rubik.ExecuteOperation(operationNode.Value);
        }
        UpdateOperationsText();
    }

    public void UndoOperation()
    {
        if (rubik.isRotation) return;
        // 寻找上一个结点
        if (operationNode.Previous == null)
        {
            Utils.PrintLog("没有上一步操作啦", true);
        }
        else
        {
            // fix: 先执行当前结点操作 再移动到上一结点
            rubik.ExecuteOperation(GetReverseOperation(operationNode.Value));
            // 有上一个节点就执行操作
            operationNode = operationNode.Previous;
            addOperationNode = operationNode;
        }
        UpdateOperationsText();
    }

    /// <summary>
    /// 除头结点全部移除 重置魔方状态
    /// </summary>
    public void ClearOperation()
    {
        while(operationList.Count > 1)
        {
            operationList.RemoveLast();
        }
        operationNode = operationList.First;
        addOperationNode = operationNode;

        UpdateOperationsText();
    }

    /// <summary>
    /// 更新操作数组
    /// </summary>
    public void UpdateOperationsText()
    {
        StringBuilder sb = new StringBuilder();

        LinkedListNode<Operation> tempNode = operationList.First;

        while (true)
        {
            if (tempNode == operationNode)
            {
                sb.Append($"<color=#FF0000FF>{GetOperationName(tempNode.Value)}</color> ");
            }
            else
            {
                sb.Append($"{GetOperationName(tempNode.Value)} ");
            }
            if (tempNode == operationList.Last) break;
            tempNode = tempNode.Next;
        }

        uiController.SetOperationQueueText(sb.ToString());
    }

    /// <summary>
    /// 解析操作字符串
    /// </summary>
    /// <param name="cmd"></param>
    public void ParseOperationCommand(string cmd)
    {
        List<Operation> operationList = new List<Operation>();

        // 先去掉空字符转大写 然后转数组
        cmd.Replace(" ", "").ToUpper();
        char[] charArray = cmd.ToCharArray();

        // 检索是否有关键字
        for (int i = 0; i < cmd.Length; i++)
        {
            char c = charArray[i];
            if (c == 'U' || c == 'D' || c == 'L' || c == 'R' || c == 'F' || c == 'B')
            {
                string operationName = c.ToString();
                if(charArray[i+1] == '\'')
                {
                    operationName += "\'";
                }
                operationList.Add(GetOperationType(operationName));
            }
            else if(c == '\'')
            {
                continue;
            }
            else
            {
                operationList.Add(Operation.Error);
                Utils.PrintLog($"未知的操作类型{c}，请检查");
            }
        }

        UpdateOperationList(operationList);
    }

    /// <summary>
    /// 根据操作名获取操作类型
    /// </summary>
    /// <param name="opName"></param>
    /// <returns></returns>
    public Operation GetOperationType(string opName)
    {
        Operation Operation;
        if (operationName.TryGetValue(opName, out Operation))
        {
            return Operation;
        }
        else
        {
            Utils.PrintLog($"未知的操作类型{opName}，请检查");
            return Operation.Error;
        } 
    }

    /// <summary>
    /// 根据操作类型获取操作名
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    public string GetOperationName(Operation operation)
    {
        string opName;
        if(operationType.TryGetValue(operation, out opName))
        {
            return opName;
        }
        else
        {
            Utils.PrintLog($"未知的操作类型{operation}，请检查");
            return opName;
        }
    }

    /// <summary>
    /// 获取对应操作的反操作
    /// TODO: 整合进Operation基类？
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    public Operation GetReverseOperation(Operation operation)
    {
        Operation result = Operation.Error;
        switch (operation)
        {
            case Operation.UpCW:
                result = Operation.UpCCW;
                break;
            case Operation.UpCCW:
                result = Operation.UpCW;
                break;
            case Operation.DownCW:
                result = Operation.DownCCW;
                break;
            case Operation.DownCCW:
                result = Operation.DownCW;
                break;
            case Operation.LeftCW:
                result = Operation.LeftCCW;
                break;
            case Operation.LeftCCW:
                result = Operation.LeftCW;
                break;
            case Operation.RightCW:
                result = Operation.RightCCW;
                break;
            case Operation.RightCCW:
                result = Operation.RightCW;
                break;
            case Operation.FrontCW:
                result = Operation.FrontCCW;
                break;
            case Operation.FrontCCW:
                result = Operation.FrontCW;
                break;
            case Operation.BehindCW:
                result = Operation.BehindCCW;
                break;
            case Operation.BehindCCW:
                result = Operation.BehindCW;
                break;
            case Operation.XCW:
                result = Operation.XCCW;
                break;
            case Operation.XCCW:
                result = Operation.XCW;
                break;
            case Operation.YCW:
                result = Operation.YCCW;
                break;
            case Operation.YCCW:
                result = Operation.YCW;
                break;
            case Operation.ZCW:
                result = Operation.ZCCW;
                break;
            case Operation.ZCCW:
                result = Operation.ZCW;
                break;
            case Operation.Error:
                result = Operation.Error;
                break;
        }

        return result;
    }
}
