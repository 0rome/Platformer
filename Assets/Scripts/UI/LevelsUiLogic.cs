using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelsUiLogic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private int LevelIndex;


   private void Start()
    {
        bestTimeText.text = "Best time : " + PlayerPrefs.GetInt("Level_" + LevelIndex).ToString();
    }
}
