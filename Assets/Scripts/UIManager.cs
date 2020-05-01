using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText = null;
    [SerializeField]
    private Image _livesImage = null;
    [SerializeField]
    private Sprite[] _liveSprites = null;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Scoring(int playerScore)
    {

      _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
      _livesImage.sprite = _liveSprites[currentLives];
    }
}
