using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hydro3
{

    public partial class SmallDevicesStyle : ResourceDictionary
    {
        public SmallDevicesStyle()
        {
            InitializeComponent();
        }

        public static SmallDevicesStyle SharedInstance { get; } = new SmallDevicesStyle();


        const int smallWightResolution = 768;
        const int smallHeightResolution = 1280;

        public static bool IsASmallDevice()
        {
            // Get Metrics
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;
            return (width <= smallWightResolution && height <= smallHeightResolution);
        }


    }
}