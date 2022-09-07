using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpReminder : MonoBehaviour
{
    [SerializeField] private GameObject _levelUpReminderPanel;
    [SerializeField] private UIShakeAnimation _uIShakeAnimation;

    public void Init()
    {
        _levelUpReminderPanel.SetActive(SaveSystem.IsRestarted());
        _uIShakeAnimation.Shake();

        SaveSystem.DeleteRestarted();
    }

    public void Disable()
    {
        _levelUpReminderPanel.SetActive(false);
        _uIShakeAnimation.Stop();
    }
}
