using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    public Text experienceText;
    public Text goldText;

    private bool questWindowOpen = false;

    // Reference to the player
    public Player player;

    // Reference to a specific quest
    public Quest questToGive;

    private void Start()
    {
        questWindow.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (questWindowOpen)
            {
                CloseQuestWindow();
            }
            else
            {
                OpenQuestWindow();
            }
        }
    }

    public void OpenQuestWindow()
    {
        questWindowOpen = true;
        questWindow.SetActive(true);

        // Access the 'quest' property of the 'questToGive' object
        titleText.text = questToGive.title;
        descriptionText.text = questToGive.description;
        experienceText.text = questToGive.experienceReward.ToString();
        goldText.text = questToGive.goldReward.ToString();
    }

    public void CloseQuestWindow()
    {
        questWindowOpen = false;
        questWindow.SetActive(false);
    }

    // Function to accept the quest and add it to the player's active quests
    public void AcceptQuest()
    {
        player.AcceptQuest(questToGive);
        CloseQuestWindow(); // Close the quest window after accepting
    }
}
