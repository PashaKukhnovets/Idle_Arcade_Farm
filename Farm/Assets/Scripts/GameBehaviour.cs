using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _coins;
    [SerializeField] private TextMeshProUGUI _items;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _wheatPrefab;
    [SerializeField] private GameObject _ratPrefab;
    [SerializeField] private GameObject _healthBonusPrefab;
    [SerializeField] private GameObject _doubleCoinsBonusPrefab;
    [SerializeField] private GameObject _DCTimer;
    [SerializeField] private GameObject _player;

    private GameObject[] _wheats;
    private GameObject[] _rats;
    private GameObject _currentWheat;
    private bool _isHealthBonusInstance = true;
    private bool _isDCoinsBonusInstance = true;
    private bool _isDCTimer = false;
    private bool _isRatCreate = false;
    public static int ratCreateCount = 0;
    public static int wheatCreateCount = 0;

    public static int _ratKills = 0;

    public int _wheatCount = 20;
    public float _timeStart = 60f;

    private int _balance = 0;
    public int Balance
    {
        get
        {
            return _balance;
        }
        set
        {
            _balance = value;
            _coins.text = _balance.ToString();
        }
    }

    private int _itemsCollected = 0;
    public int Items
    {
        get {
            return _itemsCollected;
        }
        set {
            _itemsCollected = value;
            _items.text = _itemsCollected.ToString();
        }
    }

    private void Start()
    {
        ratCreateCount = 0;
        wheatCreateCount = 0;
        Time.timeScale = 1.0f;
        _restartButton.SetActive(false);
        _timerText.text = _timeStart.ToString();
        _coins.text = _balance.ToString();
        _items.text = _itemsCollected.ToString();
        _player = GameObject.FindGameObjectWithTag("Player");
        _player.GetComponent<PlayerController>().PlayerDeath += PlayerDeathRestart;

        _rats = GameObject.FindGameObjectsWithTag("Rat");

        //while (_rats.Length < 3) { 
        //    Instantiate(_ratPrefab, new Vector3(Random.Range(-2.9f, 1.9f), 0.048f,
        //        Random.Range(-1.4f, 18.0f)), Quaternion.identity);

        //    _rats = GameObject.FindGameObjectsWithTag("Rat");
        //}

        StartCoroutine(IsCreatingRat());
    }

    private void Update()
    {
        GameTimer();

        InstanceWheat();

        if (_isRatCreate)
        {
            InstanceRat();
        }

        InstanceHealthBonus();
        InstanceDoubleCoinsBonus();
        EnableDCTimer();
    }

    public void Restart() {
        SceneManager.LoadScene(0);
    }

    public void GameTimer() {
        _timeStart -= Time.deltaTime;
        _timerText.text = Mathf.Round(_timeStart).ToString();
        if (Mathf.Round(_timeStart) <= 0)
        {
            Time.timeScale = 0.0f;
            _restartButton.SetActive(true);
        }
    }

    public void InstanceWheat() {
        _wheats = GameObject.FindGameObjectsWithTag("Wheat");

        if (_wheats.Length < 20 && wheatCreateCount < 20)
        {
            wheatCreateCount++;
            StartCoroutine(CreateWheat());
        }
    }

    public void InstanceRat() {
        _rats = GameObject.FindGameObjectsWithTag("Rat");

        if (_rats.Length < 3 && ratCreateCount < 3)
        {
            ratCreateCount++;
            StartCoroutine(CreateRat());
        }
    }

    public void InstanceHealthBonus()
    {
        if (_ratKills % 10 == 0 && _ratKills != 0 && _isHealthBonusInstance)
        {
            Instantiate(_healthBonusPrefab, new Vector3(Random.Range(-2.9f, 1.9f), 1.0f,
                Random.Range(-1.4f, 18.0f)), Quaternion.identity);
            _isHealthBonusInstance = false;
        }

        if (_ratKills % 10 != 0)
            _isHealthBonusInstance = true;
    }

    public void InstanceDoubleCoinsBonus()
    {
        if (_ratKills % 20 == 0 && _ratKills != 0 && _isDCoinsBonusInstance)
        {
            Instantiate(_doubleCoinsBonusPrefab, new Vector3(Random.Range(-2.9f, 1.9f), 1.0f,
                Random.Range(-1.4f, 18.0f)), Quaternion.identity);
            _isDCoinsBonusInstance = false;
        }

        if (_ratKills % 20 != 0)
            _isDCoinsBonusInstance = true;
    }

    private void EnableDCTimer()
    {
        if (_isDCTimer)
        {
            _DCTimer.SetActive(true);
            _isDCTimer = false;
        }
    }

    public void SetDDTimer(bool isTimer)
    {
        this._isDCTimer = isTimer;
    }

    public void PlayerDeathRestart() {
        _restartButton.SetActive(true);
    }

    public IEnumerator CreateRat() {
        yield return new WaitForSeconds(5.0f);
        Instantiate(_ratPrefab, new Vector3(Random.Range(-2.9f, 1.9f), 0.048f,
                Random.Range(-1.4f, 18.0f)), Quaternion.identity);
    }

    public IEnumerator CreateWheat()
    {
        yield return new WaitForSeconds(5.0f);

        if (SceneManager.GetActiveScene().name == "Level_2")
        {
            _currentWheat = Instantiate(_wheatPrefab, new Vector3(Random.Range(6, 9), 0.048f,
                    Random.Range(0.5f, 13.5f)), Quaternion.Euler(270.0f, 0.0f, 0.0f));
        }
        if (SceneManager.GetActiveScene().name == "Level_1") {
            _currentWheat = Instantiate(_wheatPrefab, new Vector3(Random.Range(-9, -6), 0.048f,
                    Random.Range(4.0f, 17.0f)), Quaternion.Euler(270.0f, 0.0f, 0.0f));
        }
    }

    public IEnumerator IsCreatingRat() {
        yield return new WaitForSeconds(3.0f);
        _isRatCreate = true;
    }

}
