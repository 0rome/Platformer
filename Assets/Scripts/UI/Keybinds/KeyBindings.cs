using UnityEngine;

public static class KeyBindings
{
    public static KeyCode GetKey(string action)
    {
        if (PlayerPrefs.HasKey(action))
        {
            return (KeyCode)PlayerPrefs.GetInt(action);
        }
        else
        {
            return GetDefaultKey(action);
        }
    }

    public static void SetKey(string action, KeyCode key)
    {
        PlayerPrefs.SetInt(action, (int)key);
        PlayerPrefs.Save();
    }

    private static KeyCode GetDefaultKey(string action)
    {
        switch (action)
        {
            case "Jump": return KeyCode.W;
            case "MoveLeft": return KeyCode.A;
            case "MoveRight": return KeyCode.D;
            case "Dash": return KeyCode.LeftShift;
            case "Flashlight": return KeyCode.F;
            case "DropWeapon": return KeyCode.Q;
            case "TakeWeapon": return KeyCode.E;
            default: return KeyCode.None;
        }
    }
}
