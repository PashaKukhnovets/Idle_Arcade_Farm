using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _body;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameBehaviour _gameManager;
    [SerializeField] private GameObject _gatheringButton;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _canvasTransform;
    [SerializeField] private GameObject _playerWeapon;
    [SerializeField] private GameObject _groundEffect;
    [SerializeField] private AudioSource _gatheringAudio;
    [SerializeField] private AudioSource _runningAudio;

    private float _moveSpeed = 4f;
    private float _heightDistance = 0.21f;
    private float _wideDistance = -0.5f;
    private bool _isGatheringClicked = false;
    private bool _isAttackingClicked = false;
    private bool _isRunningAudio = true;

    private List<GameObject> _wheatList = new List<GameObject>();

    public int _wheatPrice = 15;
    public float _playerDamage = 25.0f;
    public float _playerHealth = 100.0f;

    public event UnityAction PlayerIdle;
    public event UnityAction PlayerRun;
    public event UnityAction PlayerGathering;
    public event UnityAction PlayerAttack;
    public event UnityAction PlayerRunAttack;
    public event UnityAction PlayerDeath;

    private void Start()
    {
        _gatheringButton.SetActive(false);
    }

    private void Update()
    {
        ActionLogic();
        CheckDeath();
    }

    private void ActionLogic() {
        _body.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, 0, _joystick.Vertical * _moveSpeed);

        if ((_joystick.Horizontal != 0 || _joystick.Vertical != 0) && !_isAttackingClicked && !_isGatheringClicked)
        {
            transform.rotation = Quaternion.LookRotation(_body.velocity);

            if (_isRunningAudio) {
                _runningAudio.Play();
                _isRunningAudio = false;
            }

            PlayerRun?.Invoke();
            _groundEffect.SetActive(true);
        }
        else if ((_joystick.Horizontal != 0 || _joystick.Vertical != 0) && _isAttackingClicked && !_isGatheringClicked)
        {
            transform.rotation = Quaternion.LookRotation(_body.velocity);
            PlayerRunAttack?.Invoke();
            _runningAudio.Stop();
            _isRunningAudio = true;
            _groundEffect.SetActive(true);
        }
        else if (_joystick.Horizontal == 0 && _joystick.Vertical == 0 && _isAttackingClicked && !_isGatheringClicked) {
            PlayerAttack?.Invoke();
            _runningAudio.Stop();
            _isRunningAudio = true;
            _groundEffect.SetActive(false);
        }
        else if (_joystick.Horizontal == 0 && _joystick.Vertical == 0 && !_isAttackingClicked && _isGatheringClicked)
        {
            PlayerGathering?.Invoke();
            _runningAudio.Stop();
            _isRunningAudio = true;
            _groundEffect.SetActive(false);
        }
        else if (_joystick.Horizontal == 0 && _joystick.Vertical == 0 && !_isAttackingClicked && !_isGatheringClicked)
        {
            PlayerIdle?.Invoke();
            _runningAudio.Stop();
            _isRunningAudio = true;
            _groundEffect.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<GatheredWheatBehaviour>() && 
            collision.gameObject.GetComponent<GatheredWheatBehaviour>().GetComponent<Rigidbody>().isKinematic) {
            if (_wheatList.Count < 40)
            {
                if (_wheatList.Count == 0) {
                    _heightDistance = 0.21f;
                }
                if (_wheatList.Count >= 20)
                {
                    _wideDistance = -1.03f;
                    _heightDistance = 0.21f * (_wheatList.Count - 19);
                }
                else if (_wheatList.Count < 20 && _wheatList.Count != 0)
                {
                    _wideDistance = -0.5f;
                    _heightDistance = (_wheatList.Count + 1) * 0.21f;
                }
                _wheatList.Add(collision.gameObject);
                collision.gameObject.transform.parent = transform;
                collision.gameObject.transform.FindChild("BonusEffect").gameObject.SetActive(false);
                collision.gameObject.transform.localPosition = new Vector3(-0.03f, _heightDistance, _wideDistance);
                collision.gameObject.transform.localRotation = Quaternion.Euler(0, -90, 0);
                _gameManager.Items++;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GroundBehaviour>())
        {
            _gatheringButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<GroundBehaviour>())
        {
            _gatheringButton.SetActive(false);
        }
    }

    public void GatheringStateOnDown() {
        _playerWeapon.SetActive(false);
        _gatheringAudio.Play();
        _isGatheringClicked = true;
    }

    public void GatheringStateOnUp()
    {
        _playerWeapon.SetActive(true);
        _gatheringAudio.Stop();
        _isGatheringClicked = false;
    }

    public void AttackingStateOnDown() {
        _isAttackingClicked = true;
    }

    public void AttackingStateOnUp() {
        _isAttackingClicked = false;
    }

    public bool CheckGatheringClick() {
        return _isGatheringClicked;
    }

    public bool CheckAttackingClick() {
        return _isAttackingClicked;
    }

    public void MinusHP(float damage) {
        _playerHealth -= damage;
    }

    public void CheckDeath()
    {
        if (this._playerHealth <= 0.0f)
        {
            PlayerDeath?.Invoke();
            this.GetComponent<PlayerController>().enabled = false;
        }
    }

    public void Sale() {
        if (_wheatList.Count >= 1)
        {
            StartCoroutine(SaleWheat());
        }
    }

    public void IncreaseWheatPrice(int number) {
        this._wheatPrice *= number;
    }

    private IEnumerator SaleWheat() {
        Instantiate(_coinPrefab, _canvasTransform);
        Destroy(_wheatList[_wheatList.Count - 1]);
        _gameManager.Items--;
        _heightDistance -= 0.21f;
        _wheatList.RemoveAt(_wheatList.Count - 1);

        yield return new WaitForSeconds(0.75f);

        _gameManager.Balance += _wheatPrice;
    }
}
