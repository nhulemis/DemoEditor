using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class Level
    {
        public int levelId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Tile> Tiles { get; set; }
        public Level()
        {
            Tiles = new List<Tile>();
        }
    }
}
