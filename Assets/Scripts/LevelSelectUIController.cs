using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUIController : MonoBehaviour
{
    public Dropdown levelsDropdown;
    public event Action<int> LevelSelected;


    void Start()
    {
        levelsDropdown.onValueChanged.AddListener(OnLevelSelectChange);
    }

    public void AddDropdownOptions(List<string> options)
    {
        levelsDropdown.AddOptions(options);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        levelsDropdown.value = 0;
        levelsDropdown.enabled = true;
    }

    private void OnLevelSelectChange(int levelIndex)
    {
        if (levelIndex == 0)
        {
            return;
        }

        LevelSelected(levelIndex);
    }

}
