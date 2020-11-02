using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphas.Tiled
{
    public class TiledLayer
    {
        public DataEncoding Encoding { get; set; }
        public DataCompression Compression { get; set; }
        public string LayerName { get; set; }
        public int id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<int> firstidlist;
        public List<uint> tileIds = new List<uint>();
        public Dictionary<uint, TiledTile> tiles = new Dictionary<uint, TiledTile>();
        public TiledTile GetTile(uint index)
        {
            if(tiles.ContainsKey(index))
            {
                return tiles[index];
            }
            if(index >= tileIds.Count)
            {
                return null;
            }
            int Id = (int)tileIds[(int)index];
            if (Id <= 0)
            {
                return null;
            }
            int firstid = 1;//需要找到小于图块gid的最大的图块集的firstgid
            for (int j = 0; j < firstidlist.Count; j++)
            {
                int tmpfirstid = firstidlist[j];
                if (Id - tmpfirstid >= 0)
                {
                    firstid = tmpfirstid;
                }
            }
            if (Id - firstid < 0)
            {
                return null;
            }
            uint tileid = (uint)Id - (uint)firstid;
            TiledTile tiledTile = new TiledTile();
            tiledTile.id = (uint)Id;
            tiledTile.tileId = tileid;
            tiledTile.index = (uint)index;

            uint col = index % (uint)Width;
            uint line = index / (uint)Width;
            tiledTile.col = col;
            tiledTile.line = line;
            tiles.Add((uint)index, tiledTile);
            return tiledTile;
        }
    }
}
