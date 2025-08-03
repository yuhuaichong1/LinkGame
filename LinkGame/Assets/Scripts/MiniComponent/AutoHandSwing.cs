using UnityEngine;
using XrCode;

public class AutoHandSwing : MonoBehaviour
{
    private AnimationCurve curve;
    private RectTransform rectTrans;
    private STimer sTimer;
    void OnEnable()
    {
        if (curve == null)
            curve = UIManager.Instance.GetAnimCurve("UIGuideHand");

        rectTrans = this.GetComponent<RectTransform>();

        sTimer = STimerManager.Instance.CreateSTimer(2, -1, false, true, null, (detalTime) =>
        {
            rectTrans.localRotation = Quaternion.Euler(0, 0, 15 * curve.Evaluate(detalTime / 2));
        });
    }

    void OnDisable()
    {
        if(sTimer != null)
            sTimer.Stop();
    }

    public void Reset()
    {
        sTimer.ReStart();
    }
}
