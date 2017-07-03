using System.ComponentModel;

namespace ScreepsGUI.Tools.MVVM
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        /*
         * 
         * Auto-generated with PropertyChanged.Fody package
         * 
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
         * 
         */
    }
}