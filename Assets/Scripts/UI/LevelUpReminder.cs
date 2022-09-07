using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpReminder : MonoBehaviour
{
    [SerializeField] private GameObject _levelUpReminderPanel;
    [SerializeField] private UIShakeAnimation _uIShakeAnimation;

    public void Init(UIAppearance uIAppearance)
    {
        _levelUpReminderPanel.SetActive(SaveSystem.IsRestarted());

        if(SaveSystem.IsRestarted())
            _uIShakeAnimation.Shake();

        SaveSystem.DeleteRestarted();

        uIAppearance.AnimationEnded -= Init;
    }

    public void Disable()
    {
        _levelUpReminderPanel.SetActive(false);
        _uIShakeAnimation.Stop();
    }
}
