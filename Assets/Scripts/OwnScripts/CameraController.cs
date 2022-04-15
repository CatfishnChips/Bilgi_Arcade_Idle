using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private List<Transform> Cameras;
    [SerializeField] private float cameraPitch, cameraRadius, angle;

    #endregion

    #region Private Variables

    private Vector3 cameraForward, cameraRight;

    #endregion

    #endregion

    void LateUpdate()
    {
        OrbitCamera();
    }

    private void OrbitCamera()
    {
        float rad = (angle - 90) * Mathf.Deg2Rad;
        Vector3 direction = Vector3.zero;
        direction.x = Mathf.Cos(rad);
        direction.z = Mathf.Sin(rad);

        Vector3 finalPosition = transform.position + direction * cameraRadius;
        Vector3 offset = new Vector3(0, cameraPitch, 0);

        foreach (Transform camera in Cameras)
        {
            camera.position = finalPosition + offset;

            camera.LookAt(transform.position, Vector3.up);
        }

        cameraForward = direction;
        cameraRight = CalculateCameraRight();
    }

    private Vector3 CalculateCameraRight()
    {
        float rad = (angle) * Mathf.Deg2Rad;
        Vector3 direction = Vector3.zero;
        direction.x = Mathf.Cos(rad);
        direction.z = Mathf.Sin(rad);
        return direction;
    }

    public void RotateCamera(float angle)
    {
        this.angle += angle;
    }

    public Vector3 GetCameraForward() => cameraForward;

    public Vector3 GetCameraRight() => cameraRight;
}