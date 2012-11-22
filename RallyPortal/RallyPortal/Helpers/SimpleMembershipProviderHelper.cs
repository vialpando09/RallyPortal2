using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using RallyPortal.Models;
using WebMatrix.WebData;

namespace RallyPortal.Helpers
{
    public static class SimpleMembershipProviderHelper
    {
        public static IEnumerable<UserProfile> GetUserProfiles(this string[] users, UsersContext userContext)
        {
            return userContext.UserProfiles.Where(user => users.Contains(user.UserName));
        }

        public static IEnumerable<UserProfile> GetUsersNotInRoles(this SimpleMembershipProvider provider, string[] roles, UsersContext userContext)
        {
            List<string> users = new List<string>();
            foreach (var role in roles)
            {
                users.AddRange(Roles.GetUsersInRole(role));
            }
            var usersArray = users.ToArray();
            return userContext.UserProfiles.Where(user => !usersArray.Contains(user.UserName));
        }

        public static string[] GetUsersNotInRoles(this string[] currentUsers, string[] roles)
        {
            List<string> users = new List<string>();
            foreach (var role in roles)
            {
                users.AddRange(Roles.GetUsersInRole(role));
            }
            var usersArray = users.ToArray();
            return currentUsers.Where(user => !usersArray.Contains(user)).ToArray();
        }

        public static IEnumerable<UserProfile> GetAllUsers(this SimpleMembershipProvider provider, UsersContext userContext)
        {
            return userContext.UserProfiles;
        }

        public static UserProfile GetUserByUserName(this SimpleMembershipProvider provider, string userName, UsersContext userContext)
        {
            return userContext.UserProfiles.Where(user => user.UserName == userName).FirstOrDefault();
        }

        public static UserProfile GetUserByUserId(this SimpleMembershipProvider provider, int userId, UsersContext userContext)
        {
            return userContext.UserProfiles.Where(user => user.UserId == userId).FirstOrDefault();
        }

        public static UserProfile GetUserByEmailAddress(this SimpleMembershipProvider provider, string emailAddress, UsersContext userContext)
        {
            return userContext.UserProfiles.Where(user => user.Email == emailAddress).FirstOrDefault();
        }
    }
}