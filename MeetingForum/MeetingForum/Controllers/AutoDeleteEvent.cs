using MeetingForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MeetingForum.Controllers
{
    public class AutoDeleteEvent : IHttpModule
    {
        static Timer timer;
        long interval = 30000; //30 секунд
        static object synlock = new object();
        static bool sent = false;

        public void Init(HttpApplication app)
        {
            timer = new Timer(new TimerCallback(SendEmail), null, 0, interval);
        }

        private void SendEmail(object obj)
        {
            lock (synlock)
            {
                DateTime dd = DateTime.Now;
                ApplicationDbContext db = new ApplicationDbContext();

                if (db.Events.Any() != false)
                {
                    Event @event = db.Events.FirstOrDefault();
                    if(@event.Date.Day+1 < dd.Day)
                    {
                        db.Events.Remove(@event);
                        db.SaveChanges();
                    }

                    sent = true;
                }
                else
                {
                    sent = false;
                }
            }
        }
        public void Dispose() { }
    }
}