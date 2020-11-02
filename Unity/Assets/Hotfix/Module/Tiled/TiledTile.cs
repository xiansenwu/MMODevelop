using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphas.Tiled
{
    public class TiledTile
    {
        public uint tileId;//
        public uint id;//图块集内部的图块编号  需要找到小于图块gid的最大的图块集的firstgid 得出 tileId
        public uint index;
        public uint col;//列
        public uint line;//行

        //资源图 算法
        /*int x = tileid % tiledTileset.m_TileColumns;
            int y = tileid / tiledTileset.m_TileColumns;

            // Get x source on texture
            int srcx = x * tiledTileset.m_TileWidth;
            srcx += x * tiledTileset.m_Spacing;
            srcx += tiledTileset.m_Margin;

            // Get y source on texture
            int srcy = y * tiledTileset.m_TileHeight;
            srcy += y * tiledTileset.m_Spacing;
            srcy += tiledTileset.m_Margin;

            // In Tiled, texture origin is the top-left. However, in Unity the origin is bottom-left.
            srcy = (tiledTileset.imageHeight - srcy) - tiledTileset.m_TileHeight;

            if (srcy < 0)
            {
                // This is an edge condition in Tiled if a tileset's texture may have been resized
                return;
            }
            */
    }
}
