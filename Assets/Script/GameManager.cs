using System;
using TMPro;
using UnityEngine;
using ScriptableObjects;
using System.Data;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text energyText;

    [SerializeField] private TMP_Text messageText;

    [SerializeField] private GameObject WinUi;
    [SerializeField] private GameObject LoseUi;

    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private NPCWanderer npcWanderer;

    [SerializeField] private int WinCoinTakeIt;

    private int _gold = 0;
    private int _energy = 100;

    [SerializeField] private float hideMessageDelay = 5;

    public void PlayerDidHit(ScriptableObjects.Resources resource)
    {
        switch (resource.Type)
        {
            case ScriptableObjects.Resources.ResourceType.None:
                break;
            case ScriptableObjects.Resources.ResourceType.Gold:
                messageText.text = resource.GrabMessage;
                messageText.gameObject.SetActive(true);
                Invoke("HideMessage", hideMessageDelay);
                _gold += resource.Amount;
                goldText.text = $"COINTS:  {_gold} / {WinCoinTakeIt}";
                break;
            case ScriptableObjects.Resources.ResourceType.Trap:
                messageText.text = resource.GrabMessage;
                messageText.gameObject.SetActive(true);
                Invoke("HideMessage", hideMessageDelay);
                _energy += resource.Amount;
                energyText.text = $"ENERGY:{_energy}";
                break;
            default:
                throw new ArgumentException();
        }
    }

    private void Update()
    {
        if (_gold >= WinCoinTakeIt)
        {
            WinUi.SetActive(true);
        }
    }

    public void PlayerDeath()
    {
        LoseUi.SetActive(true);
    }

    private void HideMessage()
    {
        messageText.gameObject.SetActive(false);
    }

}
