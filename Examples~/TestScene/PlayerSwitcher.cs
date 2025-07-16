using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    public PlayerController[] players;
    private int currentIndex = 0;

    void Start()
    {
        if (players == null || players.Length == 0)
        {
            Debug.LogError("Press SHIFT to switch players！");
            return;
        }
        SetActivePlayer(currentIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            currentIndex++;
            if (currentIndex >= players.Length)
                currentIndex = 0;
            SetActivePlayer(currentIndex);
        }
    }

    private void SetActivePlayer(int index)
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].SetActive(i == index);
        }
        Debug.Log("Current Player in Control: " + players[index].name);
    }
}