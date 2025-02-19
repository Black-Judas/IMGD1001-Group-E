using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown { get; private set; }

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }

    //Get the selected option from the dropdown
    public string GetSelectedOption()
    {
        return dropdown.options[dropdown.value].text;
    }

    //Add an option to the dropdown
    public void AddOption(string option)
    {
        dropdown.options.Add(new TMP_Dropdown.OptionData(option));
    }

    //Clear all options from the dropdown
    public void ClearOptions()
    {
        dropdown.options.Clear();
    }
}
