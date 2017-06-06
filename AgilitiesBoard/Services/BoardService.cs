using AgilitiesBoard.Models;
using AgilitiesBoard.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace AgilitiesBoard.Services
{
    public class BoardService : IBoardService
    {
        private readonly IRepository _repository;

        public BoardService(IRepository repository)
        {
            _repository = repository;
        }

        public List<BoardModel> GetBoardData(string key)
        {
            return _repository.GetList<BoardModel>(key);
        }

        public List<BoardModel> FindByNameOrMessage(string key, string searchString)
        {
            return _repository.GetList<BoardModel>(key).Where(x => x.UserName.ToLower().Contains(searchString.ToLower()) || x.Message.ToLower().Contains(searchString.ToLower())).ToList();
        }
        
        public void SaveChanges(string key, List<BoardModel> boardList)
        {
            _repository.SetList<BoardModel>(key, boardList);
        }
    }
}