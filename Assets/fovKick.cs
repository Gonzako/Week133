using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class fovKick : MonoBehaviour
{
    #region Public Fields
    public Ease kickUpEase;
    public Ease fallbackEase;

    [Header("TestValues")]
    public float kickStrengh;
    public float kickTime, fallbackDur;
    #endregion

    #region Private Fields
    Tween kickUpTween;
    Tween fallbackTween;
    CinemachineVirtualCamera cam;
    Sequence fovKickSequence;
    float defSize;
    #endregion

    #region Public Methods
    public void FovKick(float kickStrengh, float kickTime, float fallbackDuration)
    {
        fovKickSequence = DOTween.Sequence();
        kickUpTween = DOTween.To(() => cam.m_Lens.FieldOfView, x => cam.m_Lens.FieldOfView = x, cam.m_Lens.FieldOfView + kickStrengh, kickTime).SetEase(kickUpEase);
        fovKickSequence.Append(kickUpTween)
            .Append(fallbackTween = DOTween.To(() => cam.m_Lens.FieldOfView, x => cam.m_Lens.FieldOfView = x, defSize, fallbackDuration).SetEase(fallbackEase));

    }
    #endregion

    #region Private Methods
    #endregion


#if true
    #region Unity API

    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        defSize = cam.m_Lens.FieldOfView;
    }

    void FixedUpdate()
    {
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "fovKickTest"))
        {
            //playsound
            FovKick(kickStrengh, kickTime, fallbackDur);
        }
    }
    #endregion
#endif

}