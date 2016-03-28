using UnityEngine;
using GlobalEnumType;
public static class Global
{
    public static GameMange instance = null;
    static EnumType currentScene = EnumType.none;
    public static void EnterScene(EnumType et)
    {
        if (currentScene != EnumType.none)
        {

        }
        Application.LoadLevel(et.ToString());
    }
}

