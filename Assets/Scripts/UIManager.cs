using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour {

    public static string subjectCode;
    public static string subjectAge;
    public static string subjectSex;
    public static string subjectHandedness;
    public static string subjectTennisExp;
    public static string subjectVRExp;


    public TMP_InputField codeInput;
    public TMP_InputField ageInput;
    public TMP_Dropdown sexDropdown;
    public TMP_Dropdown handednessDropdown;
    public TMP_Dropdown TennisDropdown;
    public TMP_Dropdown VRDropdown;



    [SerializeField] private bool _codeIsFilled;
    [SerializeField] private bool _ageIsFilled;
    [SerializeField] private bool _sexIsSelected;
    [SerializeField] private bool _handednessIsSelected;
    [SerializeField] private bool _tennisExpIsSelected;
    [SerializeField] private bool _vrExpIsSelected;

    void Update ()
    {
	    if (_codeIsFilled && _ageIsFilled && _sexIsSelected && _handednessIsSelected && _tennisExpIsSelected && _vrExpIsSelected)
        {
            subjectCode = codeInput.text;
            subjectAge = ageInput.text;
            subjectTennisExp = TennisDropdown.options[TennisDropdown.value].text;
            subjectVRExp = VRDropdown.options[VRDropdown.value].text;
            subjectSex = sexDropdown.options[sexDropdown.value].text;
            subjectHandedness = handednessDropdown.options[handednessDropdown.value].text;
            SceneManager.LoadScene("Main");
        }
	}

    public void UpdateCode()
    {
        _codeIsFilled = !string.IsNullOrEmpty(codeInput.text);
    }

    public void UpdateAge()
    {
        _ageIsFilled = !string.IsNullOrEmpty(ageInput.text);
    }

    public void UpdateSex()
    {
        _sexIsSelected = sexDropdown.value > 0;
    }
    public void UpdateHandedness()
    {
        _handednessIsSelected = handednessDropdown.value > 0;
    }

    public void UpdateTennisExp()
    {
        _tennisExpIsSelected = TennisDropdown.value > 0;
    }

    public void UpdateVRExp()
    {
        _vrExpIsSelected = VRDropdown.value > 0;
    }

}
