using System.Collections;
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

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log($"Trigger with {other.gameObject.name}");
    //}

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(talkkey) && !talking)
        {
            talking = true;

            phraseArea.SetActive(true);

            anim.SetBool(talkParameter, talking);

            StartCoroutine(Talk(phrases[talkingIndex]));
        }
        else if(Input.GetKeyDown(nextPhrasekey) && talking && !typingPhrase)
        {
            if(talkingIndex + 1 < phrases.Length)
            {
                talkingIndex += 1;
                StartCoroutine(Talk(phrases[talkingIndex]));
            }
            else
            {
                phraseArea.SetActive(false);

                StopTalking();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopTalking();
    }

    private void StopTalking()
    {
        talking = false;
        talkingIndex = 0;
        typingPhrase = false;
        StopCoroutine(nameof(Talk));
        anim.SetBool(talkParameter, talking);
        phraseArea.SetActive(false);
    }

    IEnumerator Talk(string phrase)
    {
        typingPhrase = true;

        phraseTextArea.text = "";

        if (typeWords)
        {
            string[] words = phrase.Split(" ");

            foreach (string word in words)
            {
                phraseTextArea.text += $" {word}";
                yield return new WaitForSeconds(talkingWorkSpeed) ;
            }
        }
        else
        {
            int length = phrase.Length;

            for (int i = 0; i < length; i++)
            {
                phraseTextArea.text += phrase[i];
                yield return new WaitForSeconds(talkingLettersSpeed);
            }
        }

        if (talkingIndex < phrases.Length - 1)
        {
            phraseTextArea.text += "...";
        }

        typingPhrase = false;
    }


}
