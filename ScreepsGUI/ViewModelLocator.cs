using ScreepsGUI.ViewModel;

namespace ScreepsGUI
{
    public class ViewModelLocator
    {
        private static MainWindowModel mainWindowModel;
        public static MainWindowModel MainWindowModelStatic
        {
            get
            {
                if (mainWindowModel == null)
                    mainWindowModel = new MainWindowModel();
                return mainWindowModel;
            }
        }
        public MainWindowModel MainWindowModel
        {
            get { return MainWindowModelStatic; }
        }
    }
}