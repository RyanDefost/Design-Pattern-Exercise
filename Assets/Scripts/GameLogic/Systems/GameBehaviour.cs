using Project.GameLogic.ServiceLocator;

namespace Project.GameLogic.Systems
{
    /// <summary>
    /// An base class that assigns itself to the GameMaster to be able to Update itself every frame.
    /// </summary>
    public abstract class GameBehaviour
    {
        private GameMaster gameMaster;

        public GameBehaviour()
        {
            this.gameMaster = MultiServiceLocator.GetService<GameMaster>();

            AssignToGameMaster();
        }

        private void AssignToGameMaster()
        {
            this.gameMaster.OnUpdate += Update;
        }

        // Update that gets called every frame.
        public virtual void Update() { }
    }
}