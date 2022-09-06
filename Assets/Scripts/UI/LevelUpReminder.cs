using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpReminder : MonoBehaviour
{
    [SerializeField] private GameObject _levelUpReminderPanel;

    public void Init()
    {
        _levelUpReminderPanel.SetActive(SaveSystem.IsRestarted());

        SaveSystem.DeleteRestarted();
    }
}
