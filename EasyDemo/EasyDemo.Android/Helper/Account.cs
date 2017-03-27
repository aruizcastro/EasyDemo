using SQLite.Net.Attributes;

namespace EasyDemo.Droid.Helper
{
    [PrimaryKey]
    public class Account
    {
        public string username { get; set; }
        public string status { get; set; }

    }
}