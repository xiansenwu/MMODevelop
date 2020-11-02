using FairyGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class APGMovieClip : GComponent
    {
        public string PackageName { get; protected set; }
        public string ResName { get; protected set; }
        public GMovieClip movieClip;
        public APGMovieClip(string packageName, string resName)
        {
            PackageName = packageName;
            ResName = resName;
            movieClip = UIPackageHelp.CreateObject(PackageName, ResName).asMovieClip;
            AddChild(movieClip);
        }
        public override void Dispose()
        {
            UIPackageHelp.RemovePackage(PackageName);
            base.Dispose();

        }
    }
}
