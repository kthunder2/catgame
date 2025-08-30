using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI textUI; // Assign in inspector
    public TextMeshProUGUI nameUI; // Assign in inspector
    public Button nextButton;      // Assign in inspector
    public Image portraitImage; // Assign in inspector

    [Header("Text Content")]
    [TextArea(2, 5)]
    public List<string> textLines; // Fill in inspector
    public List<string> names; // Fill in inspector
    private int currentIndex = 0;

    [Header("Images")]
    public List<Sprite> portraits; // Assign in inspector

    [Header("Typewriter Settings")]
    public float typeSpeed = 0.05f; // Delay between letters

    private Coroutine typingCoroutine;
    private bool isTyping = false;

    public static TextDisplay instance;
    public bool isActive = true;

    void Start()
    {
        instance = this;
        if (textLines.Count > 0)
        {
            nameUI.text = names[currentIndex];
            StartTyping(textLines[currentIndex]);
        }
            

        nextButton.onClick.AddListener(OnNextClicked);
    }

    void OnNextClicked()
    {
        if (isTyping)
        {
            // If still typing, skip to full line
            StopCoroutine(typingCoroutine);
            textUI.text = textLines[currentIndex];
            isTyping = false;
        }
        else
        {
            // Advance to next line
            currentIndex++;
            if (currentIndex < textLines.Count)
            {
                nameUI.text = names[currentIndex];
                portraitImage.sprite = portraits[currentIndex];
                StartTyping(textLines[currentIndex]);
            }
            else
            {
                isActive = false;
                // Out of lines
                nextButton.gameObject.SetActive(false);
            }
        }
    }

    void StartTyping(string line)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeLine(line));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        textUI.text = "";

        foreach (char c in line)
        {
            textUI.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
    }
}
