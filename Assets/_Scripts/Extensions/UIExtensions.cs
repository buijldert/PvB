using UnityEngine.UI;

/// <summary>
/// This class contains several extensions methods for our UI
/// </summary>
public static class UIExtensions 
{
    /// <summary>
    /// Clears the input field text.
    /// </summary>
    /// <param name="inputField">InputField we want to clear.</param>
    public static void Clear(this InputField inputField)
    {
        inputField.text = string.Empty;
    }
}
