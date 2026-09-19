using UnityEngine;

public class HUDController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The part of the health that decreases.")]
    private RectTransform m_healthBar;
    #endregion

    #region Private Variables
    private float p_originalWidth;
    #endregion
    
    #region Intialization
    private void Awake() { 
        p_originalWidth = m_healthBar.sizeDelta.x; 
    }
    #endregion

    #region Update Health Bar
    public void UpdateHealth(float percent) { 
        m_healthBar.sizeDelta = new Vector2(p_originalWidth * percent, m_healthBar.sizeDelta.y); 
    }
    #endregion
}
