using UnityEngine.EventSystems;

public class CustomBaseInput : BaseInput
{
    public override float GetAxisRaw(string axisName)
    {
        // Implement your custom input logic here
        // Example: return your custom input values for "Horizontal" and "Vertical"
        if (axisName == "Horizontal")
        {
            // Your custom horizontal input logic
            return 0f; // Replace this with your actual horizontal input value
        }
        else if (axisName == "Vertical")
        {
            // Your custom vertical input logic
            return 0f; // Replace this with your actual vertical input value
        }
        return 0f;
    }

    public override bool GetButtonDown(string buttonName)
    {
        // Implement your custom button input logic here
        // Example: return true when "Submit" button is pressed
        if (buttonName == "Submit")
        {
            // Your custom submit button input logic
            return false; // Replace this with your actual submit button input condition
        }
        return false;
    }

    // More overrides as needed for other input methods
}