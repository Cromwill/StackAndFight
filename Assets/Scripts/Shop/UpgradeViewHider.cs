using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpgradeViewHider : MonoBehaviour
{
    public void Hide()
    {
        UpgradeView[] upgradeViews = GetComponentsInChildren<UpgradeView>();

        foreach (var view in upgradeViews)
        {
            view.UIAppearance.Hide();
        }
    }
}
