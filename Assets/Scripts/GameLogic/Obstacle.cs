using Project.GameLogic.EntityComponents;
using Project.GameLogic.Systems;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace Project.GameLogic
{
    public class Obstacle :  Entity
    {
        public CollisionComponent CollisionComponent { get; }

        public Obstacle(Vector2 position, Vector2 scale, Color color)
        {
            this.spriteRenderer.color = color;
            this.SetPosition(position);
            this.SetScale(scale);
            
            this.CollisionComponent = new CollisionComponent(this);
            this.CollisionComponent.Activate();
        }
    }
}