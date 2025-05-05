namespace Project.GameLogic
{
    public abstract class GameBehaviour
    {
        private GameMaster gameMaster;

        public GameBehaviour()
        {
            this.gameMaster = ISingleton<GameMaster>.Instance();

            AssignToGameMaster();
        }

        private void AssignToGameMaster()
        {
            this.gameMaster.OnUpdate += Update;
        }

        public virtual void Update() { }
    }
}
