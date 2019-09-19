using UnityEngine;
using UnityEngine.UI;

public class DifficultySetter : MonoBehaviour
{
    public GameSettings settings;
    public Toggle easyToggle;
    public Toggle normalToggle;
    public Toggle hardToggle;

    void Awake()
    {
        Debug.Assert(easyToggle);
        Debug.Assert(normalToggle);
        Debug.Assert(hardToggle);
    }

    void Start()
    {
        easyToggle.isOn = false;
        normalToggle.isOn = false;
        hardToggle.isOn = false;

        switch (settings.difficulty)
        {
            case Difficulty.Easy: easyToggle.isOn = true; return;
            case Difficulty.Normal: normalToggle.isOn = true; return;
            case Difficulty.Hard: hardToggle.isOn = true; return;
        }
    }

    public void OnChange()
    {
        if (easyToggle.isOn) settings.difficulty = Difficulty.Easy;
        if (normalToggle.isOn) settings.difficulty = Difficulty.Normal;
        if (hardToggle.isOn) settings.difficulty = Difficulty.Hard;
    }
}
