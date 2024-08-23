using AdminApp.Stores;
using AdminApp.ViewModel;

namespace AdminApp.Commands;

public class NavigateHomeCommand : CommandBase
{
    private readonly NavigationStore _navigationStore;

    public NavigateHomeCommand(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }

    public override bool CanExecute(object? parameter)
    {
        throw new NotImplementedException();
    }

    public override void Execute(object? parameter)
    {
        _navigationStore.CurrentViewModel = new HomeViewModel();
    }
}
