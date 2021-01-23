using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class Level
    {
        public List<Tile> Tiles { get; set; }
        public Level()
        {
            Tiles = new List<Tile>();
        }
    }
}
