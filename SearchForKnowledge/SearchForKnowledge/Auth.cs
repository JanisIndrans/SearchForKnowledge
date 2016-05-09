using System.Web;
using SearchForKnowledge.Database;
using SearchForKnowledge.Models;

namespace SearchForKnowledge
{
    namespace Forum
    {
        public static class Auth
        {
            private const string UserKey = "SearchForKnowledge.Auth.UserKey";

            public static User User
            {
                get
                {
                    if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        return null;
                    }
                    var user = HttpContext.Current.Items[UserKey] as User;
                    if (user == null)
                    {
                        UserDb db = new UserDb();
                        user = db.GetCurrentUser();

                        if (user == null)
                        {
                            return null;
                        }
                        HttpContext.Current.Items[UserKey] = user;
                    }
                    return user;
                }
            }
        }
    }
}