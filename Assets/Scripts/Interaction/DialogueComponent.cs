using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueComponent : InteractableComponent
{
    [SerializeField] string[] dialogueString;
    [SerializeField] GameObject textMesh;

    private GameObject spawnedDialogue;
    public override void OnInteraction()
    {
        HideUI();
        DisplayDialogue();
    }

    private void DisplayDialogue()
    {
        spawnedDialogue = Instantiate(textMesh, GetUISpawnPosition(), Quaternion.identity, transform);
        string dialogueText = "";
        foreach(string dialogueLine in dialogueString)
        {
            dialogueText += dialogueLine + "\n";
        }
        spawnedDialogue.GetComponent<TextMeshPro>().text = dialogueText;

    }

    public override void HideUI()
    {
        base.HideUI();
        Destroy(spawnedDialogue);
        spawnedDialogue = null;
    }
}
