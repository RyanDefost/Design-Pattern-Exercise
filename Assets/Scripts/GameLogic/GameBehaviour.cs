// Ignore Spelling: Behaviour

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
            this.gameMaster.OnStart += Start;
        }

        public virtual void Update() { }

        public virtual void Start() { } //NOT YET HOOKED UP
    }
}
