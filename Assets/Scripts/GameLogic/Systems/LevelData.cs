using System.Collections.Generic;
using UnityEngine;

namespace Project.GameLogic.Systems
{
    public class LevelData
    {
        private CollisionSystem collisionSystem;

        private List<Obstacle> obstacles = new();
        
        public LevelData()
        {
            obstacles.Add(new Obstacle(new Vector2(0f,1.5f),  new Vector2(2,8), Color.white));
            obstacles.Add(new Obstacle(new Vector2(-5f,1.5f),  new Vector2(8,2), Color.white));
            obstacles.Add(new Obstacle(new Vector2(5f,-1.5f),  new Vector2(8,2), Color.white));
            obstacles.Add(new Obstacle(new Vector2(5f,3f),  new Vector2(2,2), Color.white));
            obstacles.Add(new Obstacle(new Vector2(-5f,-3f),  new Vector2(2,2), Color.white));
            obstacles.Add(new Obstacle(new Vector2(0f,-5f),  new Vector2(40,2), Color.black));
            obstacles.Add(new Obstacle(new Vector2(0f,5f),  new Vector2(40,2), Color.black));
            obstacles.Add(new Obstacle(new Vector2(-9f,0f),  new Vector2(2,20), Color.black));
            obstacles.Add(new Obstacle(new Vector2(9f,0f),  new Vector2(2,20), Color.black));
        }
    }
}