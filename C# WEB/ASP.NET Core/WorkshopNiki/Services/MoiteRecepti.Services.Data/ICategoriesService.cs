﻿namespace MoiteRecepti.Services.Data
{
    public interface ICategoriesService
    {
       IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
