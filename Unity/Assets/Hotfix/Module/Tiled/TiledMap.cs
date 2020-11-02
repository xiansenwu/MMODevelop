using ET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Alphas.Tiled
{
    public class TiledMap
    {
        public string m_Version;
        public string m_TiledVersion;
        public MapOrientation m_Orientation; //地图视角
        public MapRenderOrder m_RenderOrder; //地图坐标方向
        public int m_Width;//多少个图块
        public int m_Height;//多少个图块
        public int m_TileWidth;//一个图块的大小
        public int m_TileHeight;//一个图块的大小
        public bool m_Infinite;
        public int m_NextObjectId;
        public Dictionary<int,TiledTileset> tiledTilesets = new Dictionary<int, TiledTileset>();
        public Dictionary<string, TiledLayer> tileLayers = new Dictionary<string, TiledLayer>();

        
    }


}
