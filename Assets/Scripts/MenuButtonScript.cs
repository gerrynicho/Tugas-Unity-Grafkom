using TMPro;
using UnityEngine;
using UnityEngine.UI; // Still keeping it here for documentation purposes

public class MenuButtonScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject UIScreen;
    public GameObject SpeedBarUI;
    private Button btn; // Button is redeclared in UIElements, so it might cause confusion


    public void OnButtonPressed()
    {
        SpeedBarUI.SetActive(PlayerController.isGameOver);
        UIScreen.SetActive(false);
        PlayerController.isGameOver = false;
    }

    void Start()
    {
        btn = this.GetComponent<Button>();
        SpeedBarUI.SetActive(PlayerController.isGameOver);
        PlayerController.isGameOver = true; // Stop enemy movement before game start
        btn.onClick.AddListener(OnButtonPressed);
    }
}
