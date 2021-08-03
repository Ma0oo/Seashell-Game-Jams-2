using System.Collections.Generic;

namespace Infrastructure.Data
{
    public class DataProgressGame : IData
    {
        public string Name => "ProgressGame";
        
        public List<string> LevelComplited = new List<string>();
        public int KillEnemy;
    }
}