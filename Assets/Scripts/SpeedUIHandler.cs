using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    public PlayerController playerController;
    public UIDocument UIDoc;
    private Label m_SpeedLabel;
    private VisualElement m_SpeedBarMask;


    private void Start()
    {
        m_SpeedLabel = UIDoc.rootVisualElement.Q<Label>("SpeedLabel");
        m_SpeedBarMask = UIDoc.rootVisualElement.Q<VisualElement>("SpeedBarMask");
        playerController.OnSpeedChange += SpeedChanged;
        SpeedChanged();
    }


    void SpeedChanged()
    {
        m_SpeedLabel.text = playerController.velocity.ToString("F2");
        float speedRatio = (float) playerController.velocity / (playerController.speed * Mathf.Sqrt(2f));
        float speedPercent = Mathf.Lerp(8, 88, speedRatio);
        m_SpeedBarMask.style.width = Length.Percent(speedPercent);
    }

}