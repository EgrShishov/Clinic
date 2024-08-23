using AdminApp.Common.Abstractions;
using AdminApp.Stores;

namespace AdminApp.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    private readonly NavigationStore _navigationStore;
    
    public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
    
    public MainViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;

        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}
