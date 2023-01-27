using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EqupmentShower : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI helmChest;
    [SerializeField]
    private TextMeshProUGUI shoulderText;
    [SerializeField]
    private TextMeshProUGUI chestText;

    [SerializeField]
    private Equipments f;

    private void Update()
    {
        if (f.Helm) helmChest.SetText(f.Helm.Name);
        if (f.Shoulder) shoulderText.SetText(f.Shoulder.Name);
        if (f.Chest) chestText.SetText(f.Chest.Name);
    }
}
