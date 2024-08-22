
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AdminApp.ViewModel;

public partial class AllReceptionistsViewModel : ObservableObject
{
    public ObservableCollection<ReceptionistModel> receptionists { get; set; } = new();
    public AllReceptionistsViewModel()
    {

    }

    [RelayCommand]
    public async Task CreateNewReceptionist() => CreateReceptionist();

    [RelayCommand]
    public async Task DeleteReceptionist() => await DeleteReceptionist();

    private async Task DeleteReceptionist(ReceptionistModel receptionist)
    {
        if (receptionists.Contains(receptionist))
        {
            //await _httpClient.Receptionist.Delete();
            receptionists.Remove(receptionist);
        }
    }

    private void CreateReceptionist()
    {
        // Логика перехода на страницу создания рецепциониста
    }
}
