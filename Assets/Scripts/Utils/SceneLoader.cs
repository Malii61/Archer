public static class SceneLoader
{
    public enum Scene
    {
        MenuScene,
        GameScene,
    }

    public static void LoadScene(Scene scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
    }

    public static void LoadScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }
}