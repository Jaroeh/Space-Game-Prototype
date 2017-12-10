//**** Default Units of Measurement ****//
//
//  Mass             :   Kilograms
//  Volume          :   Meters^2
//  Temperature     :   Kelvins
//  Distance        :   Kilometers
//  Dist From Star  :   Astronomical Units
//
//****                              ****//

using System.Windows.Forms;

namespace Space_Explorer.main.utils
{
    internal static class ObjectUtils {
        internal static void DisplayException(string source, string message, string functionName, string stackTrace)
        {
            MessageBox.Show(text: $"The object {source} has triggered an exception of type {message} in {functionName}.\nStack Trace:\n{stackTrace}");
        }
    }
}

