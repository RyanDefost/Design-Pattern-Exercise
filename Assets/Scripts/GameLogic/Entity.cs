using UnityEngine;

namespace Project.GameLogic
{
    public class Entity
    {
        protected GameObject gameObject = new GameObject();

        protected Sprite sprite = Resources.Load<Sprite>("Sprites/Default_Sprite");
        protected SpriteRenderer spriteRenderer;

        public Entity()
        {
            this.spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
            this.spriteRenderer.sprite = this.sprite;
        }

        public void UpdateEntity()
        {

        }

        public void AddPosition(Vector2 position)
        {
            this.gameObject.transform.position += (Vector3)position;
        }

        public void SetPosition(Vector2 position)
        {
            this.gameObject.transform.position = position;
        }


        public void SetRotation(Vector3 rotation)
        {
            this.gameObject.transform.eulerAngles = rotation;
        }

        public void SetScale(Vector2 scale)
        {
            this.gameObject.transform.localScale = scale;
        }

        public Vector2 GetPosition() => gameObject.transform.position;
        public Vector2 GetScale() => gameObject.transform.lossyScale;
    }
}
