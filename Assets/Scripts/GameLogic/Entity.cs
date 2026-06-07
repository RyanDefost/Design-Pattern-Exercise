using UnityEngine;

namespace Project.GameLogic
{
    /// <summary>
    /// Base class to set a create entities inside of the Unity world space.
    /// </summary>
    public class Entity
    {
        protected GameObject gameObject = new GameObject();
        public GameObject GameObject { get => this.gameObject; }


        protected Sprite sprite = Resources.Load<Sprite>("Sprites/Default_Sprite");
        protected SpriteRenderer spriteRenderer;

        public Entity()
        {
            this.spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
            this.spriteRenderer.sprite = this.sprite;
        }

        // Updates the Entity.
        public void UpdateEntity()
        {

        }

        // Adds the given Vector2 to the current position.
        public void AddPosition(Vector2 position)
        {
            this.gameObject.transform.position += (Vector3)position;
        }

        // Sets the current position the given Vector2.
        public void SetPosition(Vector2 position)
        {
            this.gameObject.transform.position = position;
        }

        // Sets the current rotation the given Vector3.
        public void SetRotation(Vector3 rotation)
        {
            this.gameObject.transform.eulerAngles = rotation;
        }

        // Sets the current scale the given Vector2.
        public void SetScale(Vector2 scale)
        {
            this.gameObject.transform.localScale = scale;
        }

        // Sets the current color of the sprite renderer.
        public void SetColor(Color color)
        {
            this.spriteRenderer.color = color;
        }

        public Vector2 GetPosition() => gameObject.transform.position;
        public Vector2 GetScale() => gameObject.transform.lossyScale;
    }
}
