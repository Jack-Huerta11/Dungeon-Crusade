using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public int health = 100;
    public int stamina = 80;
    
    public int level = 1;
    public int gold = 0;
    public int experience = 0;

    // A list to store multiple quests
    public List<Quest> quests = new List<Quest>();

    // Add a method to add a quest to the player's list of quests
    public void AcceptQuest(Quest quest)
    {
        quests.Add(quest);
    }

    // Add a method to remove a quest from the player's list of quests
    public void CompleteQuest(Quest quest)
    {
        quests.Remove(quest);
    }
}
