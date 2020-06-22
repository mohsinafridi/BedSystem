using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Data
{
    public class AppUserIEFRepository : IAppUserIEFRepository
    {
        private HotelCloudDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public AppUserIEFRepository(HotelCloudDbContext context, UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager
            )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        #region UserCount

        #region App User Count

        public int ToTalUserCount()
        {
            return _context.appUsers.Count();
        }
        #endregion

        #region Roles Count

        public int RolesCount()
        {
            return _context.Roles.Count();
        }

        #endregion

        #region Count user by Role

        public int EnDUserCount()
        {
            return _context.appUsers
                .Where(p => p.AppRole.Name == "User")
                .Count();
        }



        public int ManagerCount()
        {
            return _context.appUsers
                .Where(p => p.AppRole.Name == "Manager")
                .Count();
        }

        public int AdminCount()
        {
            return _context.appUsers
                .Where(p => p.AppRole.Name == "Admin")
                .Count();
        }



        #region EnableUser
        public int EnabledUsers()
        {
            return _context.appUsers
                .Where(p => p.IsEnable == true)
                .Count();
        }
        #endregion


        #region EnableUser
        public int DisabledUsers()
        {
            return _context.appUsers
                .Where(p => p.IsEnable == false)
                .Count();
        }
        #endregion

        #region HotelCount
        public int HotelCount()
        {
            return _context.hotels.Count();
        }
        #endregion

        #endregion
        #endregion

        public PagedList<AppUser> ManagerialList(QueryOptions options, string roleid1, string roleid2, string userId, bool IncludeRole = false)
        {

            if (IncludeRole && roleid1 != null && roleid2 != null && userId != null)
            {


                return new PagedList<AppUser>(_userManager.Users
                    .Include(p => p.AppRole)
                    .Where(p => p.AppRole.Id == roleid1)
                    .Where(p => p.AppRole.Id == roleid2)
                    .Where(p => p.Id == userId)
                    , options);
            }


            if (IncludeRole && roleid1 != null && roleid2 != null && userId == null)
            {


                return new PagedList<AppUser>(_userManager.Users
                    .Include(p => p.AppRole)
                    .Where(p => p.AppRole.Id == roleid1)
                    .Where(p => p.AppRole.Id == roleid2)
                    , options);
            }




            return new PagedList<AppUser>(_userManager.Users, options);

        }


        public PagedList<AppUser> UsersList(QueryOptions options, string Roleid, string userId, bool IncludeRole = false)
        {

            if (IncludeRole && Roleid != null && userId != null)
            {


                return new PagedList<AppUser>(_userManager.Users
                    .Include(p => p.AppRole)
                    .Where(p => p.AppRole.Id == Roleid)
                    .Where(p => p.Id == userId)
                    , options);
            }


            if (IncludeRole && Roleid != null && userId == null)
            {


                return new PagedList<AppUser>(_userManager.Users
                    .Include(p => p.AppRole)
                    .Where(p => p.AppRole.Id == Roleid)
                    , options);
            }




            return new PagedList<AppUser>(_userManager.Users, options);

        }



        public async Task<PagedList<AppUser>> AllUsersList(QueryOptions options, string Roleid, bool IncludeRole = false)
        {

            if (IncludeRole && Roleid != null)
            {


                return new PagedList<AppUser>(_userManager.Users
                    .Include(p => p.AppRole)
                    .Where(p => p.AppRole.Id == Roleid)

                    , options);
            }

            if (IncludeRole && Roleid == null)
            {


                return new PagedList<AppUser>(_userManager.Users
                    .Include(p => p.AppRole)
                    , options);
            }


            return new PagedList<AppUser>(_userManager.Users, options);

        }


        public PagedList<AppUser> NotinRoleUser(QueryOptions options, bool IncludeRole = false)
        {

            if (IncludeRole)
            {


                return new PagedList<AppUser>(_userManager.Users
                    .Include(p => p.AppRole)
                    .Where(p => p.AppRole.Id == null)
                    , options);
            }



            return new PagedList<AppUser>(_userManager.Users, options);

        }


        public PagedList<AppRole> GetRoles(QueryOptions options, string roleid, bool IncludeRole = false)
        {

            if (roleid != null)
            {


                return new PagedList<AppRole>(_roleManager.Roles
                    .Where(p => p.Id == roleid)
                    , options);
            }



            return new PagedList<AppRole>(_roleManager.Roles, options);

        }


        //public PagedList<AppUser> RoleList(QueryOptions options)
        //{


        //    return new PagedList<AppUser>(_roleManager.Roles,options);

        //}
        //List<UserListViewModel> userlist = new List<UserListViewModel>();
        //UserListViewModel model = new UserListViewModel();
        //var users = _userManager.Users;
        //foreach (var user in users)
        //{
        //    var findroles = _userManager.GetRolesAsync(user).Result;
        //    if (findroles.Count > 0)
        //    {
        //        foreach (var role in findroles)
        //        {
        //            model = new UserListViewModel()
        //            {
        //                AppUserId = user.Id,
        //                FirstName = user.FirstName,
        //                LastName = user.LastName,
        //                Email = user.Email,
        //                PhoneNo = user.PhoneNumber,
        //                Created = user.Created,
        //                role = role,
        //                AccountStatus = user.IsEnable
        //            };
        //            userlist.Add(model);



        //        }
        //    }
        //    if (findroles.Count == 0)
        //    {
        //        model = new UserListViewModel()
        //        {
        //            AppUserId = user.Id,
        //            FirstName = user.FirstName,
        //            LastName = user.LastName,
        //            Email = user.Email,
        //            PhoneNo = user.PhoneNumber,
        //            Created = user.Created,
        //            role = "Not in role"
        //        };

        //        userlist.Add(model);
        //    }


        //}
        //return new PagedList<AppUser>(userlist, options);


        //}




        public async Task<PagedList<AppUser>> AccountRequest(QueryOptions options, bool IncludeRole = false)
        {

            if (IncludeRole)
            {


                return new PagedList<AppUser>(_userManager.Users
                    .Include(p => p.AppRole)
                    .Where(p => p.RequestAccept == false)

                    , options);

            }


            return new PagedList<AppUser>(_userManager.Users
                .Where(p => p.RequestAccept == false),

                options);


        }


        public async Task<PagedList<AppUser>> AcceptedRequest(QueryOptions options, bool IncludeRole = false)
        {

            if (IncludeRole)
            {


                return new PagedList<AppUser>(_userManager.Users
                    .Include(p => p.AppRole)
                    .Where(p => p.RequestAccept == true)
                    .Where(p => p.AppRole.Name == "Manager")
                    , options);

            }


            return new PagedList<AppUser>(_userManager.Users
                .Where(p => p.RequestAccept == true)
                .Where(p => p.AppRole.Name == "Manager"),
                options);


        }

    }
}
