using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using UnityEngine;

public class OpenAndStartScenesOnEditor
{
    //START SCENES
    [MenuItem("Tools/Start ConstructionScene #h", priority = 1)]
    public static void StartPreGameScene() => StartGame("ConstructionScene");
    [MenuItem("Tools/Start GameScene #f", priority = 1)]
    public static void StartGameScene() => StartGame("GameScene");

    //OPEN SCENES
    [MenuItem("Tools/Open ConstructionScene #1", priority = 1)]
    public static void OpenPreGameScene() => OpenScene("ConstructionScene");
    [MenuItem("Tools/Open GameScene #2", priority = 2)]
    public static void OpenGameScene() => OpenScene("GameScene");

    //FUNCTIONS
    private static bool OpenScene(string scene)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Scenes/" + scene + ".unity");
            return true;
        }
        else
            return false;
    }
    public static void StartGame(string name)
    {
        if (OpenScene(name))
            EditorApplication.isPlaying = true;
    }
}
