using HairApp.Common.Responses;
using System.Threading.Tasks;

namespace HairApp.Web.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);

        
    }

}
