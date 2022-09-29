
using Entities;
using Repository.Contracts;
using ValuesObjects;

namespace ManagerStates
{
    public class StateService : IStateService
    {
        private readonly IUnitOfWork? _repo;
        public StateService(IUnitOfWork unitOfWork)
        {
            this._repo = unitOfWork;
        }
        public async Task<IEnumerable<State?>> ListStatesForNotifiedAsync()
        {
            var states = await this._repo!.States!.QueryAsync(st => !st.NotifiedIn.HasValue).ConfigureAwait(false);
            var stateToReturn = new List<State>();

            foreach (var stateInDb in states)
            {
                var state = new State
                {
                    Name = stateInDb.Name,
                    Uf = stateInDb.Uf,
                    Id = stateInDb.Id,
                    CreatedAt = stateInDb.CreatedAt,
                    UpdatedAt = stateInDb.UpdatedAt
                };

                state.Cities = stateInDb!.Cities!.Select(ct => new City { Name = ct.Name }).OrderBy(ct => ct.Name!).ToArray();
                stateToReturn.Add(state);
            }

            return stateToReturn.ToArray();
        }
        public async Task<IEnumerable<State?>> ListStatesAsync()
        {
            var states = await this._repo!.States!.FindAllAsync().ConfigureAwait(false);
            var stateToReturn = new List<State>();

            foreach (var stateInDb in states)
            {
                var state = new State
                {
                    Name = stateInDb.Name,
                    Uf = stateInDb.Uf,
                    Id = stateInDb.Id,
                    CreatedAt = stateInDb.CreatedAt,
                    UpdatedAt = stateInDb.UpdatedAt
                };

                state.Cities = stateInDb!.Cities!.Select(ct => new City { Name = ct.Name }).OrderBy(ct => ct.Name!).ToArray();
                stateToReturn.Add(state);
            }

            return stateToReturn.ToArray();
        }
        public async Task<State?> RemoveCityByStateAsync(State target, string cityName)
        {
            var statement = await this._repo!.States!.QueryAsync(st => st.Name! == target.Name! || st.Uf == target.Uf!).ConfigureAwait(false);

            var stateInDb = statement.FirstOrDefault();
            if (stateInDb == null)
            {
                return null;
            }

            var list = stateInDb!.Cities!.Where(ct => !ct.Name!.Equals(cityName, StringComparison.OrdinalIgnoreCase)).ToList();
            stateInDb!.Cities = list;
            stateInDb.UpdatedAt = DateTime.Now;
            stateInDb.NotifiedIn = null;

            await this._repo!.States!.UpdateAsync(stateInDb.Id!, stateInDb).ConfigureAwait(false);

            target.Cities = list.Select(ct => new City { Name = ct.Name }).OrderBy(ct => ct.Name!).ToArray();
            target.Id = stateInDb.Id;
            return target;
        }
        public async Task<State?> RegisterStateAsync(State target)
        {
            var statement = await this._repo!.States!.QueryAsync(st => st.Name! == target.Name!).ConfigureAwait(false);

            var stateInDb = statement.FirstOrDefault();
            if (stateInDb == null)
            {
                stateInDb = new StateBson
                {
                    Id = null,
                    Cities = new List<CityBson>(),
                    CreatedAt = DateTime.UtcNow,
                };
            }

            stateInDb.Name = target.Name;
            stateInDb.Uf = target.Uf;
            stateInDb.UpdatedAt = DateTime.Now;
            stateInDb.NotifiedIn = null;

            if (target.Cities != null)
            {
                var list = stateInDb!.Cities!.ToList();

                foreach (var city in target.Cities)
                {
                    if (!list.Any(ct => ct.Name!.Equals(city.Name, StringComparison.OrdinalIgnoreCase)))
                    {
                        list.Add(new CityBson
                        {
                            Name = city.Name
                        });
                    }
                }

                stateInDb!.Cities = list;
            }

            if (string.IsNullOrEmpty(stateInDb.Id))
            {
                await this._repo!.States!.CreateAsync(stateInDb).ConfigureAwait(false);
            }
            else
            {
                await this._repo!.States!.UpdateAsync(stateInDb.Id!, stateInDb).ConfigureAwait(false);
            }

            target.Cities = stateInDb!.Cities!.Select(ct => new City { Name = ct.Name }).OrderBy(ct => ct.Name!).ToArray();
            target.Id = stateInDb.Id;

            return target;
        }
        public async Task<State?> UpdateStateAsync(State target)
        {
            var statement = await this._repo!.States!.QueryAsync(st => st.Name! == target.Name!).ConfigureAwait(false);

            var stateInDb = statement.FirstOrDefault();
            if (stateInDb == null)
            {
                return null;
            }

            stateInDb.UpdatedAt = DateTime.Now;
            stateInDb.NotifiedIn = DateTime.Now;

            await this._repo!.States!.UpdateAsync(stateInDb.Id!, stateInDb).ConfigureAwait(false);
            target.Id = stateInDb.Id;

            return target;
        }
    }
}
