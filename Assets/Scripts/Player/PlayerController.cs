 using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Cached Components
    private Rigidbody cc_rb;
    #endregion 

    #region Cached References
    private Animator cr_anim;
    private Renderer cr_renderer;
    #endregion

    #region Editor Variables
    [SerializeField]
    [Tooltip("Speed of player.")]
    private float m_speed;

    [SerializeField]
    [Tooltip("Transform of camera following player.")]
    private Transform m_cameraTransform;

    [SerializeField] 
    [Tooltip("List of all attacks.")]
    private PlayerAttackInfo[] m_attacks;
    #endregion
    
    #region Private Variables
    private Vector2 p_velocity;
    // in order to do anything we can't be frozen.
    private float p_frozenTimer;
    private Color p_defaultColor;
    #endregion

    #region Initialization
    private void Awake() {
        p_velocity = Vector2.zero;
        cc_rb = GetComponent<Rigidbody>();
        cr_anim = GetComponent<Animator>();
        cr_renderer = GetComponentInChildren<Renderer>();
        p_defaultColor = cr_renderer.material.color;

        p_frozenTimer = 0;
        for (int i = 0; i < m_attacks.Length; i++) {
            PlayerAttackInfo attack = m_attacks[i];
            attack.Cooldown = 0;
            if (attack.WindupTime > attack.FrozenTime) {
                Debug.LogError(attack.AttackName + "has a wind up time that is larger than the amount of time the player is frozen for.");
            }
        }
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion 

    // Update is called once per frame
    #region Main Updates
    private void Update() {
        if (p_frozenTimer > 0) {
            p_velocity = Vector2.zero;
            p_frozenTimer -= Time.deltaTime;
            return;
        } else {
            p_frozenTimer = 0;
        }
        for (int i = 0; i < m_attacks.Length; i++) { 
            PlayerAttackInfo attack = m_attacks[i];
            if (attack.IsReady()) { 
                if (Input.GetButtonDown(attack.Button)) { 
                    p_frozenTimer = attack.FrozenTime; 
                    StartCoroutine(UseAttack(attack)); 
                    break; 
                }
            } else if (attack.Cooldown > 0) {
                attack.Cooldown -= Time.deltaTime;
            }
        }
        float forward = Input.GetAxis("Vertical");
        float right = Input.GetAxis("Horizontal");

        cr_anim.SetFloat("Speed", Mathf.Clamp01(Mathf.Abs(forward) + Mathf.Abs(right)));

        if (forward < 0.3f && forward > -0.3f){
            forward = 0;
        }
        if (right < 0.3f && right > -0.3f){
            right = 0;
        }
        p_velocity.Set(right, forward);
    }

    private void FixedUpdate() {
        cc_rb.MovePosition(cc_rb.position + m_speed * transform.forward * p_velocity.magnitude * Time.fixedDeltaTime);
        cc_rb.angularVelocity = Vector3.zero;
        if (p_velocity.sqrMagnitude > 0 ){
            float angleToRotCam = Mathf.Deg2Rad * Vector2.SignedAngle(Vector2.up, p_velocity);
            Vector3 camForward = m_cameraTransform.forward;
            Vector3 newRot = new Vector3(Mathf.Cos(angleToRotCam) * camForward.x - Mathf.Sin(angleToRotCam) * camForward.z, 0, Mathf.Cos(angleToRotCam) * camForward.z + Mathf.Sin(angleToRotCam) * camForward.x);
            float theta = Vector3.SignedAngle(transform.forward, newRot, Vector3.up);
            cc_rb.rotation = Quaternion.Slerp(cc_rb.rotation, cc_rb.rotation * Quaternion.Euler(0, theta, 0), 0.2f);
        }
    }
    #endregion 

    #region Health Methods
    public void DecreaseHealth(float damage) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    #region Attack Methods
    private IEnumerator UseAttack(PlayerAttackInfo attack) {
        cc_rb.rotation = Quaternion.Euler(0, m_cameraTransform.eulerAngles.y, 0);
        cr_anim.SetTrigger(attack.TriggerName);
        IEnumerator toColor = ChangeColor(attack.AttackColor, 10);
        StartCoroutine(toColor);
        yield return new WaitForSeconds(attack.WindupTime); 

        Vector3 offset = transform.forward * attack.Offset.z + transform.right * attack.Offset.x + transform.up * attack.Offset.y;
        GameObject go = Instantiate(attack.AbilityGo, transform.position + offset, cc_rb.rotation); 
        go.GetComponent<Ability>().Use(transform.position + offset); 
        StopCoroutine(toColor);
        StartCoroutine(ChangeColor(p_defaultColor, 50));

        yield return new WaitForSeconds(attack.Cooldown); 
        
        attack.ResetCooldown();
    }
    #endregion

    #region Misc Methods
    private IEnumerator ChangeColor(Color newColor, float speed)
    {
        Color curColor = cr_renderer.material.color; 
        while (curColor != newColor) { 
            curColor = Color.Lerp(curColor, newColor, speed/100); 
            cr_renderer.material.color = curColor;
            yield return null; 
        }
    }
    #endregion
}