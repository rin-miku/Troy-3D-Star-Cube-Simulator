using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Buttons")]
    public Button lastOperation;
    public Button nextOperation;
    public Button clearOperation;
    public Button addOperation;
    public Button resetRubik;
    public List<Button> operationButtons;

    [Header("MessagePanel")]
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI operationQueueText;

    [Header("InputPanel")]
    public TMP_InputField inputField;

    private OperationController operationController;

    void Start()
    {
        operationController = GameObject.Find("GameController").GetComponent<OperationController>();

        Utils.showOnUIAction += SetMessageText;

        lastOperation.onClick.AddListener(OnClickLastOperationHandler);
        nextOperation.onClick.AddListener(OnClickNextOperationHandler);
        clearOperation.onClick.AddListener(OnClickClearOperationHandler);
        addOperation.onClick.AddListener(OnClickAddOperationHandler);
        resetRubik.onClick.AddListener(OnClickResetRubikHandler);

        foreach(Button operationButton in operationButtons)
        {
            operationButton.onClick.AddListener(() => { OnClickOperationButtonHandler(operationButton.transform.name); });
        }
    }

    void Update()
    {
        
    }

    public void SetMessageText(string msg)
    {
        messageText.text = msg;
    }

    public void SetOperationQueueText(string msg)
    {
        operationQueueText.text = msg;
    }

    private void OnClickLastOperationHandler()
    {
        operationController.UndoOperation();
    }

    private void OnClickNextOperationHandler()
    {
        operationController.DoOperation();
    }

    private void OnClickClearOperationHandler()
    {
        operationController.ClearOperation();
    }

    private void OnClickAddOperationHandler()
    {
        // 打开操作面板
    }

    private void OnClickResetRubikHandler()
    {

    }

    private void OnClickOperationButtonHandler(string opName)
    {
        Operation operation = operationController.GetOperationType(opName);
        operationController.UpdateOperation(operation);
    }
}
