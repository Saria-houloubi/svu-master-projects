using System.Collections.Generic;

namespace AMW.Data.Abstraction.Sorting
{
    public interface ISorter<T>
    {
        IEnumerable<T> Sort(IEnumerable<T> list);
    }
}
