using AdminApp.Stores;
using AdminApp.ViewModel;

namespace AdminApp.Commands;

public class NavigateSigninCommand : CommandBase
{
    private readonly NavigationStore _navigationStore;

    public NavigateSigninCommand(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }
    public override bool CanExecute(object? parameter)
    {
        throw new NotImplementedException();
    }

    public override void Execute(object? parameter)
    {
        _navigationStore.CurrentViewModel = new SignInViewModel(_navigationStore);
    }
}
