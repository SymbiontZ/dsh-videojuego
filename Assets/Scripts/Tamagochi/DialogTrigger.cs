using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialogue;

    [System.Obsolete]
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogManager>().StartDialogue(dialogue);
    }
}
