using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalDialog : MonoBehaviour
{
    public static ModalDialog Instance { get; set; }
    public delegate void Callback();

    private Callback OnYesEvent;
    private Callback OnNoEvent;

    private Text question;
    private Button yesBtn;
    private Button noBtn;

    private GameObject panel;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance != null)
            Destroy(gameObject);
        if (Instance == null)
            Instance = gameObject.GetComponent<ModalDialog>();

        panel = transform.GetChild(0).gameObject;
        question = panel.transform.GetChild(0).GetComponent<Text>();
        yesBtn = panel.transform.GetChild(1).GetComponent<Button>();
        noBtn = panel.transform.GetChild(2).GetComponent<Button>();

        yesBtn.onClick.AddListener(OnYes);
        noBtn.onClick.AddListener(OnNo);
    }

    private void OnYes()
    {
        OnYesEvent?.Invoke();
        Hide();
    }

    private void OnNo()
    {
        OnNoEvent?.Invoke();
        Hide();
    }

    public static void Show(string Question, Callback YesHandler = null, Callback NoHandler = null)
    {
        Instance.question.text = Question;
        Instance.OnYesEvent = YesHandler;
        Instance.OnNoEvent = NoHandler;
        Instance.panel.SetActive(true);
    }

    private void Hide()
    {
        panel.SetActive(false);
    }
}
