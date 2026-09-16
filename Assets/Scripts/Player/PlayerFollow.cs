using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    #region Cached Components
    #endregion

    #region Editor Variables
    [SerializeField]
    [Tooltip("The player to follow.")]
    private Transform m_playerTransform;

    [SerializeField]
    [Tooltip("The offset from the player's origin to the camera.")]
    private Vector3 m_offset;

    [SerializeField]
    [Tooltip("How quickly the camera rotates left to right.")]
    private float m_rotationSpeed;
    #endregion

    #region Private Variables
    #endregion

    #region Main Updates
    private void LateUpdate() {
        Vector3 newPos = m_playerTransform.position + m_offset;
        transform.position = Vector3.Slerp(transform.position, newPos, 1);
        float rotationAmount = m_rotationSpeed * Input.GetAxis("Mouse X");
        transform.RotateAround(m_playerTransform.position, Vector3.up, rotationAmount);
        m_offset = transform.position - m_playerTransform.position;
    }
    #endregion

}
