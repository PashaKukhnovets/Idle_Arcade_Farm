using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseController : MonoBehaviour
{

    [SerializeField] private ParticleSystem mouseBlood;
    private GameObject player;

    public float mouseHealth = 100.0f;
    public float damage = 7.0f;

    public bool isMouseCount = true;

    public event UnityAction MouseAttack;
    public event UnityAction MouseAttackFalse;
    public event UnityAction MouseDeath;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CheckDeath();
        EscapeAfterEating();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<WheatBehaviour>())
            MouseAttack?.Invoke();

        if (other.gameObject.CompareTag("Weapon") && player.GetComponent<PlayerController>().CheckAttackingClick())
        {
            this.mouseHealth -= player.GetComponent<PlayerController>()._playerDamage;
            Instantiate(mouseBlood, new Vector3(this.transform.position.x, 0.3f, this.transform.position.z), Quaternion.identity);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.GetComponent<WheatBehaviour>())
            MouseAttackFalse?.Invoke();

    }

    private void EscapeAfterEating() {
        if (this.gameObject.GetComponent<MousePursue>().CheckChangeWheat()) {
            StartCoroutine(DeathCoroutine());
        }
    }

    public void CheckDeath()
    {
        if (this.mouseHealth <= 0.0f)
        {
            if (isMouseCount)
                GameBehaviour._ratKills++;
            isMouseCount = false;
            MouseDeath?.Invoke();
            this.gameObject.GetComponent<MousePursue>().enabled = false;
            this.gameObject.GetComponent<MouseFace>().enabled = false;
            StartCoroutine(DeathCoroutine());
        }
    }

    private IEnumerator DeathCoroutine()
    {
        this.transform.FindChild("GatheringWheatEffect").gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        GameBehaviour.wheatCreateCount--;
        GameBehaviour.ratCreateCount--;
        Destroy(this.gameObject);
    }
}
