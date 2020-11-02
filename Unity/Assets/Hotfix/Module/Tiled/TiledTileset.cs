using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Alphas.Tiled
{
    public class TiledTileset
    {
        public int firstgid;//此图块集的第一个图块在全局图块集中的位置。
        public int m_TileWidth;
        public int m_TileHeight;
        public int m_Spacing; //图块的间距，在原图上采样图块时，图块与图块之间的间隔
        public int m_Margin;//图块的边距，在原图上采样图块时，图像左侧与上方采样的剔除边界大小
        public int m_TileCount;
        public int m_TileColumns;
        public UnityEngine.Vector2 m_TileOffset;
        public string imagePath;
        public int imageWidth;
        public int imageHeight;
        public List<int> m_Tiles = new List<int>();
    }
}
