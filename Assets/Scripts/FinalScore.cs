using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    private Text _label;
    private void Awake()
    {
        _label = GetComponent<Text>();
    }
    private void Start()
    {
        _label.text = "SCORE: " + ScoreSystem.Instance.Score.ToString();
    }
}
