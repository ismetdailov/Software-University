using System.Collections.Generic;

namespace MoiteRecepti.Services.Data
{
    public interface ISettingsService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();
    }
}
