using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Cached Components
    private Rigidbody cc_rb;
    #endregion

    #region Cached References
    private Transform cr_player;
    #endregion

    #region Editor Variables
    [SerializeField]
    [Tooltip("Health of the enemy.")]
    private int m_maxHealth;
    
    [SerializeField]
    [Tooltip("Speed of the enemy.")]
    private float m_speed;

    [SerializeField]
    [Tooltip("Damage of the enemy.")]
    private float m_damage;

    [SerializeField]
    [Tooltip("The explosion when enemy dies.")]
    private ParticleSystem m_deathExplosion;

    [SerializeField] 
    [Tooltip("Drop rate for health pill (0 to 1).")]
    private float m_healthPillDropRate;

    [SerializeField] 
    [Tooltip("Type of health pill dropped.")]
    private GameObject m_healthPill;
    #endregion

    #region Private Variables
    private float p_curHealth;
    #endregion

    #region Initialization
    private void Awake()
    {
        p_curHealth= m_maxHealth;
        cc_rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        cr_player = FindAnyObjectByType<PlayerController>().transform;
    }
    #endregion

    #region Main Updates
    private void Update() {

    }
    private void FixedUpdate()
    {
        Vector3 dir = cr_player.position - transform.position;
        dir.Normalize();
        cc_rb.MovePosition(cc_rb.position + dir * m_speed * Time.fixedDeltaTime);
    }
    private void LateUpdate() {

    }
    #endregion

    #region Collision Methods
    private void OnCollisionStay(Collision collision) {
        GameObject other = collision.collider.gameObject;
        if (other.CompareTag("Player")) {
            // DecreaseHealth(m_damage);
            other.GetComponent<PlayerController>().DecreaseHealth(m_damage);
        }
    }
    #endregion

    #region Health Methods
    public void DecreaseHealth(float damage) {
        p_curHealth -= damage;
        if (p_curHealth <= 0){
            if (Random.value < m_healthPillDropRate) { 
                Instantiate(m_healthPill, transform.position, Quaternion.identity); 
            }
            Instantiate(m_deathExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    #endregion

}
