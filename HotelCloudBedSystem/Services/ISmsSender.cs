using System.Threading.Tasks;

namespace HotelCloudBedSystem.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
