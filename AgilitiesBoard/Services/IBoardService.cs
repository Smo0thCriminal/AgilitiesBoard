using AgilitiesBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilitiesBoard.Services
{
    public interface IBoardService
    {
        List<BoardModel> GetBoardData(string key);
        List<BoardModel> FindByNameOrMessage(string key, string searchString);
        void SaveChanges(string key, List<BoardModel> boardList);
    }
}
