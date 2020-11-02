/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Logic
{
    public partial class WorldWindowView : GComponent
    {
        public GComponent m_mapFloor;
        public GComponent m_city;
        public GComponent m_cache;
        public static string PackageName = "World";
        public static string ResName = "World_Window";
        public const string URL = "ui://qvviy45li1zx48";

        public static WorldWindowView CreateInstance()
        {
            return (WorldWindowView)UIPackage.CreateObject(PackageName, ResName);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);
            this.fairyBatching = true;

            m_mapFloor = (GComponent)GetChild("mapFloor");
            m_city = (GComponent)GetChild("city");
            m_cache = (GComponent)GetChild("cache");
        }
    }
}