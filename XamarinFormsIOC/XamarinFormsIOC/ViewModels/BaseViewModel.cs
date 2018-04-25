using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using XamarinFormsIOC.Attributes;

namespace XamarinFormsIOC.ViewModels
{
    [SelfInjection]
    public abstract class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        private bool _isBusy;

        /// <summary>
        ///     Shows wheather ViewModel is busy making some request or smth like this
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (IsBusy == value)
                {
                    return;
                }

                _isBusy = value;
                NotifyPropertyChanged();
            }
        }

        public void Dispose()
        {
        }

        public virtual async Task LoadData()
        {
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}