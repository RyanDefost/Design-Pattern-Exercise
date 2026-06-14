using Project.GameLogic.ServiceLocator;
using UnityEngine.UI;
using UnityEngine;

namespace Project.GameLogic.UIInterface
{
    public class StaticUI
    {
        private UserInterface userInterface;
        
        private Image backgroundImage;
        private Image elementInfoImage;
        
        public StaticUI()
        {
            this.userInterface = MultiServiceLocator.GetService<UserInterface>();
            
            //SetupUI();
        }

        private void SetupUI()
        {
            this.backgroundImage = this.userInterface.AddPanel(
                Vector2.zero,
                Vector3.one,
                new Vector4(0f, 0.85f, 1f, 1f)
            );
            
            var image = this.backgroundImage;
                image.color = new Color(0f, 0f, 0f, 0.5f);

            this.elementInfoImage = this.userInterface.AddPanel(
                Vector2.zero,
                Vector3.one,
                new Vector4(0.5f, 0f, 0.5f, 0f)
            );
            
            image = this.elementInfoImage;
                image.sprite = Resources.Load<Sprite>("Sprites/ElementType");
                image.rectTransform.localPosition = new Vector3(0f, 0f, 0f);
                image.rectTransform.localScale = new Vector3(6f, 6f, 6f);
        }
    }
}