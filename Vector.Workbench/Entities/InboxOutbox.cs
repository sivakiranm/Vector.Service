using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Workbench.Entities
{
    public class InboxOutboxlist
    {
        public List<InboxOutbox> InboxOutboxeinfo { get; set; }
    }

   public class InboxOutbox
    {
        public string notificationid { get; set; }

        public string notificationtitle { get; set; }


        public  string createUserAvatar { get; set; }

        public string createdDate { get; set; }

        public string createdUser { get; set; }

        public string notificationDescription { get; set; }

    }

}
