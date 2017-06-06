using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilitiesBoard.Utility
{
    public interface IRepository
    {
        List<T> GetList<T>(string key);
        void SetList<T>(string key, List<T> entry);
    }
}
