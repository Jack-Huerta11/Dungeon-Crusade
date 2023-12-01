using UnityEngine;
using UnityEngine.UI;

public class LevelingUIManager : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerStats playerStats;
    public GameObject levelingUI; // Reference to the parent GameObject of the leveling UI elements
    public Text pointsText;
    public UpgradeOption[] upgradeOptions;

    void Start()
    {
        // Assuming the PlayerController is attached to the same GameObject
        playerController = GetComponent<PlayerController>();
        // Hide the UI at the start
        ToggleUI(false);
    }

    void UpdatePointsUI()
    {
        if (playerStats != null && pointsText != null)
        {
            pointsText.text = "Points: " + playerStats.Points;
        }
    }

    public void UpgradeOption(int optionIndex)
    {
        if (optionIndex < 0 || optionIndex >= upgradeOptions.Length) 
            return;

        UpgradeOption selectedOption = upgradeOptions[optionIndex];

        if (playerStats != null && playerStats.Points >= selectedOption.cost)
        {
            playerStats.IncreasePoints(-selectedOption.cost);
            ApplyUpgrade(selectedOption);
            UpdatePointsUI();
        }
    }

    void ApplyUpgrade(UpgradeOption option)
    {
        if (playerController != null)
        {
            playerController.ApplyUpgrade(option.speedIncrease, option.jumpIncrease, option.healthIncrease, option.attackDamageIncrease);
        }
        else
        {
            Debug.LogError("PlayerController script not found on the player GameObject.");
        }
    }

    // Toggle the visibility of the leveling UI elements based on the 'show' parameter
    public void ToggleUI(bool show)
    {
        // Toggle the parent GameObject of the leveling UI elements based on the 'show' parameter
        if (levelingUI != null)
        {
            levelingUI.SetActive(show);
        }
    }

    void Update()
    {
        // Check for the "L" key press
        if (Input.GetKeyDown(KeyCode.L))
        {
            // Toggle the visibility of the leveling UI
            ToggleUI(!levelingUI.activeSelf);
        }
    }
}

[System.Serializable]
public class UpgradeOption
{
    public string upgradeName;
    public int cost;
    public float speedIncrease;
    public float jumpIncrease;
    public int healthIncrease;
    public float attackDamageIncrease;
}
