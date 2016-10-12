using System.Threading.Tasks;
using community.Models;
using Microsoft.AspNetCore.Http;
using community.Models.DBModels;

namespace community.Business
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class BusinessFacade
    {   
        public static string GetHej() {
            var a = new BusinessLogic().GetEntries();
            return a;
        }

        public static void InsertEntry(EntryDB entry) {
            new BusinessLogic().InsertEntry(entry);
        }
    }
}
