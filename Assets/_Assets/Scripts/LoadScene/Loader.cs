
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scenes
    {
        MainMenu,
        LoadScene,
        GameScene
    }

    public static void LoadScenes(Scenes scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

}
