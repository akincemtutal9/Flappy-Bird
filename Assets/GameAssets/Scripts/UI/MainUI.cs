using System.Collections;
using System.Collections.Generic;
using TMPro;using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    void Start()
    {
        nameText.text = "Welcome " + "<color=yellow>" + PlayerPrefs.GetString("USERNAME") + "</color>";
    }

}
