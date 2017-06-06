using AgilitiesBoard.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace AgilitiesBoard.Models
{
    public class BoardModel
    {
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public bool Gender { get; set; }
        public string Message { get; set; }
        public int MessageNumber { get; set; }
        public string Photo { get; set; }
        public string ElapsedDays
        {
            get
            {
                return DateTimeHelper.ElapsedDateTime(Date);
            }
        }
    }
}