using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Alphas.Tiled
{
    public class TileMapCompnent : Entity
    {
        TiledMap mapInfo;
        Dictionary<uint, TiledObject> items;
        public int Width{
            get{ return mapInfo.m_Width;}
        }
        public int Height{
            get{return mapInfo.m_Height; }
        }
        public int TileWidth
        {
            get { return mapInfo.m_TileWidth; }
        }
        public int TileHeight
        {
            get { return mapInfo.m_TileHeight; }
        }
        public Dictionary<string, TiledLayer> Layers
        {
            get { return mapInfo.tileLayers; }
        }
        public TileMapCompnent()
        {
            
        }
        public void Parse(string text)
        {
            mapInfo = new TiledMap();
            XDocument doc = XDocument.Parse(text);
            TmxParser tmxParser = new TmxParser();
            mapInfo = tmxParser.Parse(doc);
        }
        public UnityEngine.Vector3 GetPos(int index)
        {
            return GetPos(GetLineColPos(index,Width));
        }
        public UnityEngine.Vector3 GetPos(UnityEngine.Vector2Int vec2)
        {
            return GetPos(vec2.x, vec2.y);
        }
        public UnityEngine.Vector3 GetPos(int x, int y)//x col 列 
        {
            int height = TileHeight;
            int col = x;
            int line = y;
            float lineX = -line * TileWidth / 2;
            float lineY = (line) * height / 2; //(line + 1) * height / 2;
            float colX = col * TileWidth / 2;
            float colY = (col) * height / 2;//(col + 1) * height / 2;
            float posx = (lineX + colX);
            float posy = (lineY + colY);
            return new UnityEngine.Vector3(posx, posy, 0);

        }
        public int GetIndexByPos(UnityEngine.Vector2Int pos)
        {
            //index == 0 重建
            int index = pos.y * Width + pos.x + 1;
            return index;
        }
        public static UnityEngine.Vector2Int GetLineColPos(int index,int mapWidth = 1201)
        {
            //index == 0 重建
            if (index == 0) return new UnityEngine.Vector2Int(0, 0);
            index = index - 1;
            int col = index % mapWidth;
            int line = index / mapWidth;
            return new UnityEngine.Vector2Int(col, line);
        }
        public UnityEngine.Vector2Int GetLineColPos(UnityEngine.Vector3 vec3)
        {
            //x =  col * TileWidth / 2 - line * TileWidth / 2
            //y =  col * TileHeight / 2 + line * TileHeight / 2 
            //联立方程组
            int col = (int)(vec3.x / (TileWidth * 1.0f) + vec3.y/ (TileHeight * 1.0f));
            int line = (int)(vec3.y / (TileHeight * 1.0f) - vec3.x / (TileWidth * 1.0f));
            return new UnityEngine.Vector2Int(col, line);
        }
        List<int> tmplist = new List<int>();
        public List<int> GetRoundPoint(int index, int linewidth, int colheight )
        {
            tmplist.Clear();
            UnityEngine.Vector2Int pos = GetLineColPos(index, Width);
            int col = pos.x;
            int line = pos.y;

            for (int i = -linewidth; i <= linewidth; i++)
            {
                int realLine = line + i;
                if (realLine < 0)
                    continue;
                for (int j = -colheight; j <= colheight; j++)
                {
                    int realcol = col + j;
                    if (realcol < 0)
                        continue;
                    int indexa = realLine * Width + realcol+1;
                    if (indexa > 0)
                    {
                        tmplist.Add(indexa);
                    }
                }
            }
            return tmplist;
        }
    }
}
