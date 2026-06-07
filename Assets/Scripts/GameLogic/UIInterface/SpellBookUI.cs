using Project.GameInput;
using Project.GameLogic.ServiceLocator;
using Project.GameLogic.Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.GameLogic.UIInterface
{
    public class SpellBookUI : GameBehaviour,  IInputReceiver
    {
        private UserInterface userInterface;
        
        private readonly InputHandler inputHandler;
        private readonly SpellUICommand spellUICommand = new();
        
        private Image spellBookBackground;
        private TextMeshProUGUI TextSpellElements;
        
        public SpellBookUI()
        {
            this.userInterface = MultiServiceLocator.GetService<UserInterface>();
            
            this.inputHandler = new InputHandler(this);
            this.inputHandler.BindInputToCommand(KeyCode.RightShift, spellUICommand);
            this.spellUICommand.OnExecute += DisplaySpellUI;
            
            SetupUI();
        }

        public override void Update() => inputHandler.HandleInput();
        
        private void SetupUI()
        {
            this.spellBookBackground = this.userInterface.AddPanel(
                Vector2.zero,
                Vector3.one,
                new Vector4(0.35f, 0, 0.65f, 0.43f)
            );

            var image = this.spellBookBackground;
                image.enabled = false;

            this.TextSpellElements = this.userInterface.AddTextElement(
                "SPELLS",
                Vector2.zero,
                Vector3.one,
                new Vector4(0.35f, 0, 0.65f, 0.38f)
            );
            
            var text = this.TextSpellElements;
                text.color = Color.black;
                text.fontWeight = FontWeight.Bold;
                text.text = "Spell of Healing || U/D/U/U \n \n Spell of Strength || D/L/R/R \n \n Spell of Aerial Damage || L/L/R/L \n \n Spell of Speed || R/R/L/L";
                text.horizontalAlignment = HorizontalAlignmentOptions.Center;
                text.enabled = false;
        }

        private void DisplaySpellUI()
        {
            this.spellBookBackground.enabled = !this.spellBookBackground.IsActive();
            this.TextSpellElements.enabled = !this.TextSpellElements.IsActive(); 
        }
    }
}