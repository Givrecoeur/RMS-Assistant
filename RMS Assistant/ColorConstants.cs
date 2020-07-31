using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RMS_Assistant
{
    public class ColorConstants
    {
        //Colors
        public static Color VeryDarkGrey = Color.FromArgb(255, 30, 30, 30);
        public static Color VeryLigthGrey = Color.FromArgb(255, 200, 200, 200);

        public SolidColorBrush TreeBackground;

        public ColorConstants(bool darkMode)
        {
            SwitchMode(darkMode);
        }

        public void SwitchMode(bool isDarkMode)
        {
            if(isDarkMode) { TreeBackground = new SolidColorBrush(VeryDarkGrey); }
            else { TreeBackground = new SolidColorBrush(VeryLigthGrey); }
        }
    }
}
