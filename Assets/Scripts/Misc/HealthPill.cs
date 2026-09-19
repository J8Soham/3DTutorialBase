using UnityEngine;

public class HealthPill : MonoBehaviour {
    
    #region Editor Variables
    [SerializeField]
    [Tooltip("Amount of health this pill restores.")]
    private int m_healthGain;
    public int HealthGain {
        get {
            return m_healthGain;
        }
    }
    #endregion

   
}
