using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MultiButtonInputHandler : MonoBehaviour
{
    public Button wButton;
    public Button aButton;
    public Button sButton;
    public Button dButton;
    public Button spaceButton;

    void Update()
    {
        // Simulate button press and release for W key
        if (Input.GetKeyDown(KeyCode.W))
        {
            PressButton(wButton);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            ReleaseButton(wButton);
        }

        // Simulate button press and release for A key
        if (Input.GetKeyDown(KeyCode.A))
        {
            PressButton(aButton);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            ReleaseButton(aButton);
        }

        // Simulate button press and release for S key
        if (Input.GetKeyDown(KeyCode.S))
        {
            PressButton(sButton);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            ReleaseButton(sButton);
        }

        // Simulate button press and release for D key
        if (Input.GetKeyDown(KeyCode.D))
        {
            PressButton(dButton);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            ReleaseButton(dButton);
        }

        // Simulate button press and release for Space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PressButton(spaceButton);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ReleaseButton(spaceButton);
        }
    }

    void PressButton(Button button)
    {
        // Trigger the button press animation
        button.OnPointerDown(new PointerEventData(EventSystem.current));
    }

    void ReleaseButton(Button button)
    {
        // Trigger the button release animation and invoke onClick event
        button.OnPointerUp(new PointerEventData(EventSystem.current));
        button.onClick.Invoke(); // Optionally invoke the onClick event
    }
}
