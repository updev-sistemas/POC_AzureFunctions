using ValuesObjects;

namespace ManagerStates
{
    public interface IStateService
    {
        Task<IEnumerable<State?>> ListStatesForNotifiedAsync();
        Task<IEnumerable<State?>> ListStatesAsync();
        Task<State?> RemoveCityByStateAsync(State target, string cityName);
        Task<State?> RegisterStateAsync(State target);
        Task<State?> UpdateStateAsync(State target);
    }
}