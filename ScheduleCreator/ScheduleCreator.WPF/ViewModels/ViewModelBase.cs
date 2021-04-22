using ScheduleCreator.Domain.DTO.Observable;

namespace ScheduleCreator.WPF.ViewModels
{
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;

    public class ViewModelBase : ObservableObject
    {
    }
}
