using System.Collections.Generic;
using Project.GameLogic.ServiceLocator;
using Project.GameLogic.Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.GameLogic.UIInterface
{
    public class UserInterface : GameBehaviour
    {
        private readonly Canvas canvasUI; 
        private List<GameObject> UIElements = new();
        
        private StaticUI staticUI;
        private SpellBookUI spellBookUI;
        
        public UserInterface()
        {
            MultiServiceLocator.Provide<UserInterface>(this);
            canvasUI = SetupCanvas();
            
            this.staticUI = new StaticUI();
            this.spellBookUI = new SpellBookUI();
        }

        private Canvas SetupCanvas()
        {
            //Setup
            GameObject canvasObject =  new GameObject("UserInterface"); 
            Canvas canvas =  canvasObject.AddComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            
            //Canvas
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera.main;
            canvas.sortingOrder = 2;
            
            //CanvasScaler
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            
            return canvas;
        }

        public TextMeshProUGUI AddTextElement(string text, Vector2 localPosition, Vector3 localScale, Vector4 anchorPoints = default(Vector4))
        { 
            TextMeshProUGUI textElement = AddUIElement<TextMeshProUGUI>(
                this.canvasUI,
                localPosition,
                localScale,
                new Vector2(anchorPoints.x, anchorPoints.y), 
                new Vector2(anchorPoints.z, anchorPoints.w)
            );
            
            textElement.text = text;
            
            return textElement;
        }

        public Image AddPanel(Vector2 localPosition, Vector3 localScale, Vector4 anchorPoints = default(Vector4))
        {
            Image image = AddUIElement<Image>(
                this.canvasUI, 
                localPosition, 
                localScale, 
                new Vector2(anchorPoints.x, anchorPoints.y), 
                new Vector2(anchorPoints.z, anchorPoints.w)
            );
            
            image.sprite = Resources.Load<Sprite>("Sprites/Default_Square");;
            image.color = new Color(1f, 1f, 1f, 0.5f);
            return image;
        }
        
        public void RemoveUIElement(GameObject uiElement)
        {  
            this.UIElements.Remove(uiElement);
            Object.Destroy(uiElement);
        }
        
        public void RemoveUIElement(Component uiComponent)
        {  
            GameObject uiElement = uiComponent.gameObject;
            this.UIElements.Remove(uiElement);
            
            //
            Object.Destroy(uiElement);
        }
        
        private T AddUIElement<T>(Canvas parent, Vector2 localPosition, Vector3 localScale, Vector2 anchorMin = default(Vector2) , Vector2 anchorMax = default(Vector2))
        {
            GameObject uiElement = new();
            uiElement.AddComponent(typeof(T));
            
            uiElement.name = typeof(T).Name;
            uiElement.transform.SetParent(parent.transform);

            RectTransform rectTransform = uiElement.GetComponent<RectTransform>();
            
            //Transform
            rectTransform.localPosition = localPosition;
            rectTransform.localScale = localScale;
            //Anchor
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
            
            UIElements.Add(uiElement);
            return uiElement.GetComponent<T>();
        }
    }
}