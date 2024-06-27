using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RatController : MonoBehaviour
{

    [SerializeField] private ParticleSystem ratBlood;
    private GameObject player;

    public float ratHealth = 100.0f;
    public float damage = 7.0f;

    public bool isRatCount = true;

    public event UnityAction RatAttack;
    public event UnityAction RatAttackFalse;
    public event UnityAction RatDeath;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CheckDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
            RatAttack?.Invoke();

        if (other.gameObject.CompareTag("Weapon") && player.GetComponent<PlayerController>().CheckAttackingClick())
        {
            this.ratHealth -= player.GetComponent<PlayerController>()._playerDamage;
            Instantiate(ratBlood, new Vector3(this.transform.position.x, 0.3f, this.transform.position.z), Quaternion.identity);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.GetComponent<PlayerController>())
            RatAttackFalse?.Invoke();

    }

    public void CheckDeath() {
        if (this.ratHealth <= 0.0f)
        {
            if (isRatCount)
                GameBehaviour._ratKills++;
            isRatCount = false;
            RatDeath?.Invoke();
            this.gameObject.GetComponent<Pursue>().enabled = false;
            this.gameObject.GetComponent<Face>().enabled = false;
            StartCoroutine(DeathCoroutine());
        }
    }

    private IEnumerator DeathCoroutine()
    {
        this.transform.FindChild("GatheringWheatEffect").gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        GameBehaviour.ratCreateCount--;
        Destroy(this.gameObject);
    }
}
