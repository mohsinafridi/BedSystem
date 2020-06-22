using HotelCloudBedSystem.Models;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Data
{
    public interface IAppUserIEFRepository
    {
        Task<PagedList<AppUser>> AcceptedRequest(QueryOptions options, bool IncludeRole = false);
        Task<PagedList<AppUser>> AccountRequest(QueryOptions options, bool IncludeRole = false);
        int AdminCount();
        Task<PagedList<AppUser>> AllUsersList(QueryOptions options, string Roleid, bool IncludeRole = false);
        int DisabledUsers();
        int EnabledUsers();
        int EnDUserCount();
        PagedList<AppRole> GetRoles(QueryOptions options, string roleid, bool IncludeRole = false);
        int HotelCount();
        int ManagerCount();
        PagedList<AppUser> ManagerialList(QueryOptions options, string roleid1, string roleid2, string userId, bool IncludeRole = false);
        PagedList<AppUser> NotinRoleUser(QueryOptions options, bool IncludeRole = false);
        int RolesCount();
        int ToTalUserCount();
        PagedList<AppUser> UsersList(QueryOptions options, string Roleid, string userId, bool IncludeRole = false);
    }
}