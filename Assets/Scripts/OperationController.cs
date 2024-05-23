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

    private bool isExecuting = false;

    private void Start()
    {
        uiController = GameObject.Find("UI").GetComponent<UIController>();
        rubik = GameObject.Find("Rubik").GetComponent<Rubik>();

        InitOperation();
    }

    private void InitOperation()
    {
        // 初始化操作名
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
        // 寻找下一个结点
        if(operationNode.Next == null)
        {
            Utils.PrintLog("没有下一步操作啦");
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
        if (operationNode.Previous == null)
        {
            Utils.PrintLog("没有上一步操作啦");
        }
        else
        {
            operationNode = operationNode.Previous;
            addOperationNode = operationNode;
            rubik.ExecuteOperation(operationNode.Value);
        }
        UpdateOperationsText();
    }

    public void RemoveOperationAfterNode()
    {
        while(operationList.Last != operationNode)
        {
            operationList.RemoveLast();
        }
    }

    public void ClearOperation()
    {
        operationList.Clear();
        UpdateOperationsText();
    }

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

    public void ExecuteOperation(Operation operation)
    {
        isExecuting = true;

        switch (operation)
        {
            case Operation.UpCW:
                break;
            case Operation.UpCCW:
                break;
            case Operation.DownCW:
                break;
            case Operation.DownCCW:
                break;
            case Operation.LeftCW:
                break;
            case Operation.LeftCCW:
                break;
            case Operation.RightCW:
                break;
            case Operation.RightCCW:
                break;
            case Operation.FrontCW:
                break;
            case Operation.FrontCCW:
                break;
            case Operation.BehindCW:
                break;
            case Operation.BehindCCW:
                break;
            case Operation.Error:
                break;
        }
    }

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

    public Operation GetReverseOperation(Operation operation)
    {
        Operation reverseOperation = Operation.Error;

        switch (operation)
        {
            case Operation.UpCW:
                reverseOperation = Operation.UpCCW;
                break;
            case Operation.UpCCW:
                reverseOperation = Operation.UpCW;
                break;
            case Operation.DownCW:
                reverseOperation = Operation.DownCCW;
                break;
            case Operation.DownCCW:
                reverseOperation = Operation.DownCW;
                break;
            case Operation.LeftCW:
                reverseOperation = Operation.LeftCCW;
                break;
            case Operation.LeftCCW:
                reverseOperation = Operation.LeftCW;
                break;
            case Operation.RightCW:
                reverseOperation = Operation.RightCCW;
                break;
            case Operation.RightCCW:
                reverseOperation = Operation.RightCW;
                break;
            case Operation.FrontCW:
                reverseOperation = Operation.FrontCCW;
                break;
            case Operation.FrontCCW:
                reverseOperation = Operation.FrontCW;
                break;
            case Operation.BehindCW:
                reverseOperation = Operation.BehindCCW;
                break;
            case Operation.BehindCCW:
                reverseOperation = Operation.BehindCW;
                break;
            case Operation.Error:
                reverseOperation = Operation.Error;
                break;
        }

        return reverseOperation;
    }
}
