using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pak_Maker.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Media;

    public class ValueData
    {
        public float FovPlayer { get; set; }
        public float FovSprint { get; set; }
        public float FovBlunderbuss { get; set; }
        public float FovEyeOfReach { get; set; }
        public float FovPistol { get; set; }
        public float FovCannon { get; set; }
        public float FovWheel { get; set; }
        public float FovMap { get; set; }

        public bool EnableFieldOfView { get; set; }
        public bool EnableStaticFov { get; set; }

        public bool Quickswap { get; set; }
        public bool FastShot { get; set; }
        public bool FastReload { get; set; }
        public bool InstantZoom { get; set; }
        public bool EnableColorBullets { get; set; }

        public Color SelectedColor { get; set; }
        public float BulletColorR { get; set; }
        public float BulletColorG { get; set; }
        public float BulletColorB { get; set; }
        public float BulletColorA { get; set; }

        public bool RemoveFog { get; set; }
        public bool EnableSkyColor { get; set; }
        public Color SkyColor { get; set; }
        public float SkyColorR { get; set; }
        public float SkyColorG { get; set; }
        public float SkyColorB { get; set; }

    }
}

