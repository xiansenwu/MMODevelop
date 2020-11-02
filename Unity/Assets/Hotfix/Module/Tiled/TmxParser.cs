using ET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Alphas.Tiled
{
    public class TmxParser
    {
        TiledMap tiledMap;
        public TiledMap Parse(XDocument doc)
        {
            tiledMap = new TiledMap();
            var xMap = doc.Element("map");
            bool success = true;
            success = success && ProcessMapAttributes(xMap);
            success = success && ProcessTilesetElements(xMap);
            success = success && ProcessMapLayers(xMap);
           // success = success && ProcessMapTiles(xMap);
            return tiledMap;
        }
        private bool ProcessMapAttributes(XElement xMap)
        {
            tiledMap.m_Version = xMap.GetAttributeAs<string>("version");
            tiledMap.m_TiledVersion = xMap.GetAttributeAs<string>("tiledversion");

            tiledMap.m_Orientation = xMap.GetAttributeAs<MapOrientation>("orientation");
            tiledMap.m_RenderOrder = xMap.GetAttributeAs<MapRenderOrder>("renderorder");

            tiledMap.m_Width = xMap.GetAttributeAs<int>("width");
            tiledMap.m_Height = xMap.GetAttributeAs<int>("height");

            tiledMap.m_TileWidth = xMap.GetAttributeAs<int>("tilewidth");
            tiledMap.m_TileHeight = xMap.GetAttributeAs<int>("tileheight");
            tiledMap.m_Infinite = xMap.GetAttributeAs<bool>("infinite");
            tiledMap.m_NextObjectId = xMap.GetAttributeAs<int>("nextobjectid");

            return true;
        }
        private bool ProcessTilesetElements(XElement xMap)
        {
            bool success = true;
            foreach (var xTileset in xMap.Elements("tileset"))
            {
                var firstId = xTileset.GetAttributeAs<int>("firstgid");
                //var name = xTileset.GetAttributeAs<string>("name");
                if (xTileset.Attribute("tilecount") == null || xTileset.Attribute("columns") == null)
                {
                    Log.Warning("Old file format detected. You must save this file with a newer verion of Tiled.");
                    //return false;
                }
                TiledTileset tileset = new TiledTileset();
                tiledMap.tiledTilesets.Add(firstId, tileset);
                tileset.firstgid = firstId;
                XElement imageEle = xTileset.Element("image");
                tileset.imagePath = imageEle.GetAttributeAs<string>("source");
                tileset.imageWidth = imageEle.GetAttributeAs<int>("width");
                tileset.imageHeight = imageEle.GetAttributeAs<int>("height");
                tileset.m_TileWidth = xTileset.GetAttributeAs<int>("tilewidth");
                tileset.m_TileHeight = xTileset.GetAttributeAs<int>("tileheight");
                tileset.m_Spacing = xTileset.GetAttributeAs<int>("spacing");
                tileset.m_Margin = xTileset.GetAttributeAs<int>("margin");
                tileset.m_TileCount = xTileset.GetAttributeAs<int>("tilecount");
                tileset.m_TileColumns = xTileset.GetAttributeAs<int>("columns");

                var xTileOffset = xTileset.Element("tileoffset");
                if (xTileOffset != null)
                {
                    var x = xTileOffset.GetAttributeAs<float>("x", 0.0f);
                    var y = xTileOffset.GetAttributeAs<float>("y", 0.0f);
                    tileset.m_TileOffset = new UnityEngine.Vector2(x, y);
                }
                success = success && ProcessTileElements(xMap, tileset);
            }

            return success;
        }
        private bool ProcessTileElements(XElement xMap, TiledTileset tileset)
        {
            foreach (var xTile in xMap.Elements("tile"))
            {
                int tileid = xTile.GetAttributeAs<int>("id", -1);
                //string m_Type = xTile.GetAttributeAs("type", "");
                if (tileset.m_Tiles.Contains(tileid) == false)
                {
                    tileset.m_Tiles.Add(tileid);
                }

            }
            return true;
        }

        private bool ProcessMapLayers(XElement xMap)
        {
            // Note that this method is re-entrant due to group layers
            foreach (XElement xNode in xMap.Elements())
            {
                if (!xNode.GetAttributeAs<bool>("visible", true))
                {
                    continue;
                }
                if (xNode.Name == "layer")
                {
                    ProcessTileLayer(xNode);
                }
                else if (xNode.Name == "group")
                {
                    Log.Error("不支持 ({0}) ", xNode.Name);
                }
                else if (xNode.Name == "objectgroup")
                {
                    Log.Error("不支持 ({0}) ", xNode.Name);
                }
                else if (xNode.Name == "imagelayer")
                {
                    Log.Error("不支持 ({0}) ", xNode.Name);
                }
            }
            return true;
        }
        private void ProcessTileLayer(XElement xLayer)
        {
            var xData = xLayer.Element("data");
            if (xData != null)
            {
                var chunk = new TiledLayer();
                chunk.id = xLayer.GetAttributeAs<int>("id");
                chunk.LayerName = xLayer.GetAttributeAs<string>("name");
                chunk.Encoding = xData.GetAttributeAs<DataEncoding>("encoding");
                chunk.Compression = xData.GetAttributeAs<DataCompression>("compression");
                chunk.X = 0;
                chunk.Y = 0;
                chunk.Width = tiledMap.m_Width;
                chunk.Height = tiledMap.m_Height;
                List<int> firstidlist = tiledMap.tiledTilesets.Keys.ToList();
                firstidlist.Sort();
                chunk.firstidlist = firstidlist;

                var tileIds = ReadTileIdsFromChunk(chunk, xLayer);
                chunk.tileIds = tileIds;
                tiledMap.tileLayers.Add(chunk.LayerName, chunk);
                //PlaceTiles(goTilemap, chunk, tileIds);
            }
        }
        private bool ProcessMapTiles(XElement xMap)
        {
            List<int> firstidlist = tiledMap.tiledTilesets.Keys.ToList();
            firstidlist.Sort();
            foreach (TiledLayer tiledLayer in tiledMap.tileLayers.Values)
            {
                List<uint> tileIds = tiledLayer.tileIds;
                for (int index = 0; index < tileIds.Count; index++)
                {
                    tiledLayer.GetTile((uint)index);
                }
            }
            return true;
        }

        private List<uint> ReadTileIdsFromChunk(TiledLayer chunk, XElement xLayer)
        {
            List<uint> tileIds = new List<uint>(chunk.Width * chunk.Height);

            if (chunk.Encoding == DataEncoding.Csv)
            {
                ReadTileIds_Csv(xLayer, ref tileIds);
            }
            else if (chunk.Encoding == DataEncoding.Base64)
            {
                ReadTileIds_Base64(xLayer, chunk.Compression, ref tileIds);
            }
            else
            {
                Log.Error("Unhandled encoding type ({0}) used for map layer data.", chunk.Encoding);
            }

            return tileIds;
        }
        private void ReadTileIds_Csv(XElement xElement, ref List<uint> tileIds)
        {
            // Splitting line-by-line reducues out-of-memory exceptions for really large maps
            // (Really large maps should be avoided, however)
            string data = xElement.Value;
            StringReader reader = new StringReader(data);
            string line = string.Empty;
            do
            {
                line = reader.ReadLine();
                if (!String.IsNullOrEmpty(line))
                {
                    var datum = from val in line.Split(',')
                                where !val.IsNullOrWhiteSpace()
                                select Convert.ToUInt32(val);

                    tileIds.AddRange(datum);
                }

            } while (line != null);
        }

        private void ReadTileIds_Base64(XElement xElement, DataCompression compression, ref List<uint> tileIds)
        {
            string data = xElement.Value;

            if (compression == DataCompression.None)
            {
                tileIds = data.Base64ToBytes().ToUInts().ToList();
            }
            else if (compression == DataCompression.Gzip)
            {
                try
                {
                    tileIds = data.Base64ToBytes().GzipDecompress().ToUInts().ToList();
                }
                catch
                {
                    tileIds.Clear();
                    Log.Error("Gzip compression is not supported on your development platform. Change Tile Layer Format to another type in Tiled.");
                }
            }
            else if (compression == DataCompression.Zlib)
            {
                try
                {
                    tileIds = data.Base64ToBytes().ZlibDeflate().ToUInts().ToList();
                }
                catch
                {
                    tileIds.Clear();
                    Log.Error("zlib compression is not supported on your development platform. Change Tile Layer Format to another type in Tiled.");
                }
            }
            else
            {
                Log.Error("Unhandled compression type ({0}) used for map layer data", compression);
            }
        }
    }
}
