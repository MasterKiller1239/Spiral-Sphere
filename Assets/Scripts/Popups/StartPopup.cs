using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartPopup : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private TMP_Text text;

    public void InitPopup(Action action)
    {
        startButton.onClick.AddListener(() => action?.Invoke());
    }

}
