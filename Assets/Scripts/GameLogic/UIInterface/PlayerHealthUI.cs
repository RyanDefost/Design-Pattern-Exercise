using System.Globalization;
using System.Numerics;
using Project.GameLogic.ServiceLocator;
using Project.Player;
using TMPro;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Project.GameLogic.UIInterface
{
    public class PlayerHealthUI
    {
        private readonly UserInterface userInterface;
        private readonly Player.Player player;
        
        private TextMeshProUGUI textElement;
        private TextMeshProUGUI deathTextElement;
        
        public PlayerHealthUI(Player.Player player, Vector2 screenPosition)
        {
            this.userInterface = MultiServiceLocator.GetService<UserInterface>();
            
            this.player = player;
            this.player.HealthSystem.OnHit += UpdateHealthUI;
            this.player.HealthSystem.OnDie += DisplayDeathMessage;
            
            SetupUI(screenPosition);
        }

        private void SetupUI(Vector2 screenPosition = default(Vector2))
        {
            //Health status UI
            this.textElement = this.userInterface.AddTextElement(
                player.GameObject.name + "\n" + player.Health,
                screenPosition,
                Vector3.one
            );
            
            TextMeshProUGUI text = this.textElement;
                text.fontSize = 60;
                text.color = player.Team;
                text.fontStyle = FontStyles.Bold;
                text.alignment = TextAlignmentOptions.Center;
                text.textWrappingMode = TextWrappingModes.NoWrap;
            
            //Death message UI
            this.deathTextElement = this.userInterface.AddTextElement(
                (player.GameObject.name + " LOST").ToUpper(),
                new Vector2(960f, 540f),
                Vector3.one
            );
            
            text = this.deathTextElement;
                text.enabled = false;
                text.fontSize = 50;
                text.color = player.Team;
                text.fontStyle = FontStyles.Bold;
                text.alignment = TextAlignmentOptions.Center;
                text.textWrappingMode = TextWrappingModes.NoWrap;
        }
        
        private void UpdateHealthUI() => textElement.text = player.GameObject.name + "\n" + player.Health;

        private void DisplayDeathMessage() => deathTextElement.enabled = true;
        
    }
}