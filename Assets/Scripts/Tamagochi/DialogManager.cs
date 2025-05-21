using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Text nameText, dialogText;
    public Animator animator, mouthAnimator;
    public SpriteRenderer mouthSpriteRenderer;
    public Sprite[] mouthSprites;
    public AudioSource audio;

    IEnumerator Start()
    {
        // Espera hasta que la UI esté activa y asignada
        while (UIManager.Instance == null || !UIManager.Instance.gameObject.activeInHierarchy)
        {
            yield return null; // Espera un frame cada vez
        }

        // Asignación automática de componentes
        GameObject uiObject = GameObject.Find("UI");
        Transform tamaDialogo = uiObject.transform.Find("tamaDialogo");

        animator = tamaDialogo.GetComponent<Animator>();
        nameText = tamaDialogo.Find("Nombre")?.GetComponent<Text>();
        dialogText = tamaDialogo.Find("Dialogo")?.GetComponent<Text>();

        sentences = new Queue<string>();
        audio = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && animator.GetBool("IsOpen") && !mouthAnimator.GetBool("IsTalking"))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialog dialogue)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        if (mouthAnimator != null) mouthAnimator.SetBool("IsTalking", true);
        audio.Play();

        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        if (mouthAnimator != null) mouthAnimator.SetBool("IsTalking", false);
        audio.Stop();
    }

    public void EndDialogue()
    {
        Debug.Log("No hay más diálogos.");
        animator.SetBool("IsOpen", false);
        gameObject.SetActive(false);
    }
}