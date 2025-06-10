using Project.GameLogic.ServiceLocator;

namespace Project.GameLogic
{
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

        public virtual void Update() { }
    }
}