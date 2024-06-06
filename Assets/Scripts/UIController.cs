using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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
    public List<Button> colorButtons;
    public Transform operationButtonPanel;
    public Transform colorButtonPanel;
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

    [Header("Rubik")]
    public GameObject rubikPrefab;

    [Header("Property")]
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

        foreach(Button colorButton in colorButtons)
        {
            colorButton.onClick.AddListener(() => { OnClickColorButtonHandler(colorButton.transform.name); });
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
        operationButtonPanel.localScale = Vector3.zero;
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
        Destroy(rubik.gameObject);
        rubik = Instantiate(rubikPrefab).GetComponent<Rubik>();
        operationController.SetRubik(rubik);
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

    private void OnClickColorButtonHandler(string color)
    {
        Debug.Log(color);
        int materialIndex = 0;
        switch (color)
        {
            case "blue":
                materialIndex = 0;
                break;
            case "green":
                materialIndex = 1;
                break;
            case "red":
                materialIndex = 2;
                break;
            case "yellow":
                materialIndex = 3;
                break;
        }

        // 设置材质
        rubik.GetComponent<PatchColorManager>().SetPatchColor(materialIndex);
        // 面板缩小
        colorButtonPanel.transform.DOScale(0f, 0f);
    }
}
