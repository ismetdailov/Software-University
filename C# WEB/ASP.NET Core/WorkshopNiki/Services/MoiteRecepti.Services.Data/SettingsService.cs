using System.Collections.Generic;
using System.Linq;

using MoiteRecepti.Data.Common.Repositories;
using MoiteRecepti.Data.Models;
using MoiteRecepti.Services.Mapping;

namespace MoiteRecepti.Services.Data
{
    public class SettingsService : ISettingsService
    {
        private readonly IDeletableEntityRepository<Setting> settingsRepository;

        public SettingsService(IDeletableEntityRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public int GetCount()
        {
            return this.settingsRepository.AllAsNoTracking().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.settingsRepository.All().To<T>().ToList();
        }
    }
}
