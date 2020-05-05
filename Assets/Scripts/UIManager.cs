using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText = null;
    [SerializeField]
    private Text _gameoverText = null;
    [SerializeField]
    private Text _restartText = null;
    [SerializeField]
    private Image _livesImage = null;
    [SerializeField]
    private Sprite[] _liveSprites = null;

    private GameManager _gameManager = null;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameoverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
          Debug.LogError("GameManager is null.");
        }
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

    public void GameOver()
    {
      _gameManager.GameOver();
      _gameoverText.gameObject.SetActive(true);
      _restartText.gameObject.SetActive(true);
      StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
      while (true)
      {
        yield return new WaitForSeconds(0.5f);
        _gameoverText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _gameoverText.gameObject.SetActive(true);

      }
    }
}
