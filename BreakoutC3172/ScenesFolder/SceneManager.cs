namespace BreakoutC3172.ScenesFolder
{
    internal class SceneManager
    {
        public Scenes ActiveScene { get; private set; }
        private readonly Dictionary<Scenes, Scene> _scenes = [];

        public SceneManager(GameManager gameManager)
        {
            _scenes.Add(Scenes.SceneMenu1, new SceneMenu1(gameManager, this));
            _scenes.Add(Scenes.SceneRoom1, new SceneRoom1(gameManager, this));
            _scenes.Add(Scenes.SceneRoom2, new SceneRoom2(gameManager, this));

            ActiveScene = Scenes.SceneMenu1;
            _scenes[ActiveScene].Activate();

        }

        public void SwitchScene(Scenes scene)
        {
            ActiveScene = scene;
            _scenes[ActiveScene].Activate();
        }

        public void Update()
        {
            _scenes[ActiveScene].Update();
        }

        public RenderTarget2D GetFrame()
        {
            return _scenes[ActiveScene].GetFrame();
        }

    }
}
