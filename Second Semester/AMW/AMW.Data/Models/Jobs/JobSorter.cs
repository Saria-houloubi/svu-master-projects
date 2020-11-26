using AMW.Data.Abstraction.Sorting;
using AMW.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMW.Data.Models.Jobs
{
    public class JobSorter : ISorter<Job>
    {
        public Dictionary<string, SortType> SortByProperties { get; set; }

        public IEnumerable<Job> Sort(IEnumerable<Job> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("Can not sort a null list");
            }

            if (SortByProperties.Count == 0)
            {
                return list;
            }

            var orderQuery = list.AsQueryable();

            foreach (var item in SortByProperties)
            {
                var sortByProperty = typeof(Job).GetProperties().FirstOrDefault(prop => string.Compare(prop.Name, item.Key, true) == 0);

                if (sortByProperty == null)
                {
                    continue;
                }

                switch (item.Value)
                {
                    case SortType.Asc:
                        orderQuery = orderQuery.OrderBy((j) => sortByProperty.GetValue(j));
                        break;
                    case SortType.Desc:
                        orderQuery = orderQuery.OrderByDescending((j) => sortByProperty.GetValue(j));
                        break;
                }
            }

            return orderQuery.ToList();
        }
    }
}
