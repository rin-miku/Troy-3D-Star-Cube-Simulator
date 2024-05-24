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
    public Button autoExecute;
    public Button resetRubik;
    public Button randomRubik;
    public List<Button> operationButtons;
    public Transform operationButtonPanel;
    public Transform otherButtons;
    public Sprite play;
    public Sprite pause;

    [Header("MessagePanel")]
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI operationQueueText;

    [Header("AboutPanel")]
    public Button aboutButton;
    public Button backButton;
    public GameObject aboutPanel;

    public bool isAutoExecute = false;
    private bool operationButtonPanelIsOpen = false;
    private Rubik rubik;
    private OperationController operationController;

    void Start()
    {
        rubik = GameObject.Find("Rubik").GetComponent<Rubik>();
        operationController = GameObject.Find("GameController").GetComponent<OperationController>();

        Utils.showOnUIAction += SetMessageText;

        lastOperation.onClick.AddListener(OnClickLastOperationHandler);
        nextOperation.onClick.AddListener(OnClickNextOperationHandler);
        clearOperation.onClick.AddListener(OnClickClearOperationHandler);
        addOperation.onClick.AddListener(OnClickAddOperationHandler);
        autoExecute.onClick.AddListener(OnClickAutoExecuteHandler);
        resetRubik.onClick.AddListener(OnClickResetRubikHandler);       
        randomRubik.onClick.AddListener(OnClickRandomRubikHandler);

        aboutButton.onClick.AddListener(OnClickAboutHandler);
        backButton.onClick.AddListener(OnClickBackHandler);

        foreach(Button operationButton in operationButtons)
        {
            operationButton.onClick.AddListener(() => { OnClickOperationButtonHandler(operationButton.transform.name); });
        }
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
        Utils.PrintLog("已清空所有操作", true);
        operationController.ClearOperation();
    }

    private void OnClickAddOperationHandler()
    {
        operationButtonPanel.localScale = operationButtonPanelIsOpen ? Vector3.zero : Vector3.one;
        operationButtonPanelIsOpen = !operationButtonPanelIsOpen;
    }

    public void OnClickAutoExecuteHandler()
    {
        Utils.PrintLog("自动执行剩余操作...", true);
        isAutoExecute = !isAutoExecute;
        otherButtons.localScale = isAutoExecute ? Vector3.zero : Vector3.one;
        autoExecute.GetComponent<Image>().sprite = isAutoExecute ? pause : play;
    }

    private void OnClickRandomRubikHandler()
    {
        Utils.PrintLog("正在打乱...", true);
        rubik.RandomRubik();
        OnClickAutoExecuteHandler();
    }

    private void OnClickResetRubikHandler()
    {
        rubik.ResetRubik();
    }

    private void OnClickAboutHandler()
    {
        aboutPanel.transform.localScale = Vector3.one;
    }

    private void OnClickBackHandler()
    {
        aboutPanel.transform.localScale = Vector3.zero;
    }

    private void OnClickOperationButtonHandler(string opName)
    {
        Operation operation = operationController.GetOperationType(opName);
        operationController.UpdateOperation(operation);
    }
}
