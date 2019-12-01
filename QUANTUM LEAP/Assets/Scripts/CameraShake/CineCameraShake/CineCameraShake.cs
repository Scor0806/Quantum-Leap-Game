using UnityEngine;
using Cinemachine;


/// <summary>
/// An add-on module for Cinemachine to shake the camera
/// </summary>
[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class CineCameraShake : CinemachineExtension
{

    [Tooltip("Amplitude of the shake")]
    public float m_Range = 0.5f;
    public bool activated = false;
    CinemachineVirtualCamera vircam;

    protected override void Awake()
    {
        base.Awake();
        vircam = GetComponent<CinemachineVirtualCamera>();
    }
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            if (activated) {
                Vector3 shakeAmount = GetOffset();
                state.PositionCorrection += shakeAmount;
            }
        }
    }

    Vector3 GetOffset()
    {
        // Note: change this to something more interesting!
        return new Vector3(
            Random.Range(-m_Range, m_Range),
            Random.Range(-m_Range, m_Range),
            Random.Range(-m_Range, m_Range));
        
    }

    public void ZoomOut()
    {
        vircam.m_Lens.OrthographicSize = 9;
    }

    public void ZoomIn()
    {
        vircam.m_Lens.OrthographicSize = 5;
    }
}
