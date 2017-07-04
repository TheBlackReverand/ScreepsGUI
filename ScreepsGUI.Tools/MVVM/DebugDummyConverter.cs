using System;
using System.Diagnostics;
using System.Windows.Data;

namespace ScreepsGUI.Tools.MVVM
{
    /// <summary>
    ///     xmlns:diag="clr-namespace:System.Diagnostics;assembly=WindowsBase"
    ///     
    ///     <TextBlock Text="{Binding Title, diag:PresentationTraceSources.TraceLevel=High}" />
    ///     
    ///     
    ///     
    ///     <Window.Resources>
    ///             <self:DebugDummyConverter x:Key="DebugDummyConverter" />
    ///     </Window.Resources>
    ///     
    ///     <TextBlock Text="{Binding Title, ElementName=wnd, Converter={StaticResource DebugDummyConverter}}" />
    /// </summary>
    public class DebugDummyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }
    }
}