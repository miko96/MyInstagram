using MyInstagram.Data;
using MyInstagram.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.Identity;
using MyInstagram.Data.Infrastructure;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;

namespace MyInstagram.WebUI.Controllers
{
    public class ArtController : ApiController
    {
        //MyInstagramEntities my = new MyInstagramEntities();

        


        [HttpPost]
        public string FollowUnfollow()
        {
            var obj = Request.Content.ReadAsAsync<JObject>();
            string fromUserId = obj.Result["fromUserId"].ToString();
            string toUserId = obj.Result["toUserId"].ToString();

            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();


            var user = userManager.Users.Where(x => x.Id == fromUserId).FirstOrDefault();
            var followingUser = userManager.Users.Include(x=>x.Followers).Where(x => x.Id == toUserId).FirstOrDefault();

            var isFollower = followingUser.Followers.Contains(user);

            if (isFollower)
            {
                followingUser.Followers.Remove(user);
            }
            else
            {
                followingUser.Followers.Add(user);
            }
            userManager.Update(user);
            //var asyncContent = httpContent.ReadAsStringAsync().Result;
            //List<string> contact = JsonConvert.DeserializeObject<List<string>>(asyncContent);
            return followingUser.Followers.Count.ToString();
        }

    }
}
