using TMPro;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    Animator anim;

    [SerializeField]
    KeyCode talkkey = KeyCode.E;

    [SerializeField]
    KeyCode nextPhrasekey = KeyCode.Return;

    [SerializeField]
    string[] phrases;

    bool talking = false;

    bool typingPhrase = false;

    int talkingIndex = 0;

    int talkParameter = Animator.StringToHash("talk");

    [SerializeField]
    float talkingLettersSpeed = 0.1f;

    [SerializeField]
    float talkingWorkSpeed = 0.5f;

    [SerializeField]
    bool typeWords = false;

    [SerializeField]
    GameObject phraseArea;

    [SerializeField]
    TMP_Text phraseTextArea;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger with {other.gameObject.name}");
    }
}
