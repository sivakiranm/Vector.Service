using System;
using System.Collections.Generic;
using Vector.Workbench.Entities;

namespace Vector.Workbench.BusinessLayer
{
    public class InboxOutboxBL
    {
        public InboxOutboxlist GetInboxInfo(string userid , string username , string searchinfo)
        {
            InboxOutboxlist inboxoutboxlist = null;
            List<InboxOutbox> InboxOutboxeinfo = null;
            InboxOutbox InboxOutbox = null;

            try
            {
                inboxoutboxlist = new InboxOutboxlist();
                InboxOutboxeinfo = new List<InboxOutbox>();

                InboxOutbox = new InboxOutbox();

                InboxOutbox.createUserAvatar = "elizabethSakala.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "Elizabeth Sakla";
                InboxOutbox.notificationDescription = "Ticket #0156 is resolved on 01-05-2021, please check";
                InboxOutbox.notificationid = "1";
                InboxOutbox.notificationtitle = "Ticket resolved";
                InboxOutboxeinfo.Add(InboxOutbox);

                InboxOutbox = new InboxOutbox();
                InboxOutbox.createUserAvatar = "micheleLlamas.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "Michele Llamas";
                InboxOutbox.notificationDescription = "Ticket #0234 is resolved on 01-05-2021, please check";
                InboxOutbox.notificationid = "2";
                InboxOutbox.notificationtitle = "T#0234";
                InboxOutboxeinfo.Add(InboxOutbox);

                InboxOutbox = new InboxOutbox();
                InboxOutbox.createUserAvatar = "kristinGarrett.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "Kristin Garrett";
                InboxOutbox.notificationDescription = "Ticket #0178 is resolved on 01-03-2021, please check";
                InboxOutbox.notificationid = "3";
                InboxOutbox.notificationtitle = "T#0178";
                InboxOutboxeinfo.Add(InboxOutbox);

                InboxOutbox = new InboxOutbox();
                InboxOutbox.createUserAvatar = "particJohnson.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "particJohnson";
                InboxOutbox.notificationDescription = "Ticket #0148 is resolved on 01-03-2021, please check";
                InboxOutbox.notificationid = "4";
                InboxOutbox.notificationtitle = "T#0148";
                InboxOutboxeinfo.Add(InboxOutbox);

                InboxOutbox = new InboxOutbox();
                InboxOutbox.createUserAvatar = "angieGabel.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "angieGabel";
                InboxOutbox.notificationDescription = "Ticket #0171 is resolved on 01-03-2021, please check";
                InboxOutbox.notificationid = "5";
                InboxOutbox.notificationtitle = "T#0171";
                InboxOutboxeinfo.Add(InboxOutbox);

                InboxOutbox = new InboxOutbox();
                InboxOutbox.createUserAvatar = "kristinGarrett.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "Kristin Garrett";
                InboxOutbox.notificationDescription = "Ticket #0145 is resolved on 01-03-2021, please check";
                InboxOutbox.notificationid = "6";
                InboxOutbox.notificationtitle = "T#0145";
                InboxOutboxeinfo.Add(InboxOutbox);

                InboxOutbox = new InboxOutbox();
                InboxOutbox.createUserAvatar = "micheleLlamas.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "micheleLlamas";
                InboxOutbox.notificationDescription = "Ticket #0187 is resolved on 01-03-2021, please check";
                InboxOutbox.notificationid = "7";
                InboxOutbox.notificationtitle = "T#087";
                InboxOutboxeinfo.Add(InboxOutbox);

                InboxOutbox = new InboxOutbox();
                InboxOutbox.createUserAvatar = "kristinGarrett.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "Kristin Garrett";
                InboxOutbox.notificationDescription = "Ticket #0178 is resolved on 01-03-2021, please check";
                InboxOutbox.notificationid = "8";
                InboxOutbox.notificationtitle = "T#0178";
                InboxOutboxeinfo.Add(InboxOutbox);

                InboxOutbox = new InboxOutbox();
                InboxOutbox.createUserAvatar = "kristinGarrett.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "Kristin Garrett";
                InboxOutbox.notificationDescription = "Ticket #0178 is resolved on 01-03-2021, please check";
                InboxOutbox.notificationid = "9";
                InboxOutbox.notificationtitle = "T#0178";
                InboxOutboxeinfo.Add(InboxOutbox);

                InboxOutbox = new InboxOutbox();
                InboxOutbox.createUserAvatar = "kristinGarrett.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "Kristin Garrett";
                InboxOutbox.notificationDescription = "Ticket #0178 is resolved on 01-03-2021, please check";
                InboxOutbox.notificationid = "10";
                InboxOutbox.notificationtitle = "T#0178";
                InboxOutboxeinfo.Add(InboxOutbox);


                inboxoutboxlist.InboxOutboxeinfo = new List<InboxOutbox>();
                inboxoutboxlist.InboxOutboxeinfo = InboxOutboxeinfo;





            }
            catch (Exception ex)
            {
                throw ex;
            }

            return inboxoutboxlist;

        }

        public InboxOutboxlist GetOutboxInfo(string userid, string username ,string searchinfo)
        {
            InboxOutboxlist inboxoutboxlist = null;
            List<InboxOutbox> InboxOutboxeinfo = null;
            InboxOutbox InboxOutbox = null;

            try
            {
                inboxoutboxlist = new InboxOutboxlist();
                InboxOutboxeinfo = new List<InboxOutbox>();
                InboxOutbox = new InboxOutbox();

                InboxOutbox.createUserAvatar = "particJohnson.png";
                InboxOutbox.createdDate = "01/02/2021";
                InboxOutbox.createdUser = "particJohnson";
                InboxOutbox.notificationDescription = "Ticket #0156 is resolved on 01-05-2021, please check";
                InboxOutbox.notificationid = "1";
                InboxOutbox.notificationtitle = "Ticket resolved";
                InboxOutboxeinfo.Add(InboxOutbox);

                InboxOutbox = new InboxOutbox();
                InboxOutbox.createUserAvatar = "saraSmith.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "saraSmith";
                InboxOutbox.notificationDescription = "Ticket #0176 is resolved on 01-05-2021, please check";
                InboxOutbox.notificationid = "2";
                InboxOutbox.notificationtitle = "T#176";
                InboxOutboxeinfo.Add(InboxOutbox);

                InboxOutbox = new InboxOutbox();
                InboxOutbox.createUserAvatar = "kristinGarrett.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "Kristin Garrett";
                InboxOutbox.notificationDescription = "Ticket #0178 is resolved on 01-03-2021, please check";
                InboxOutbox.notificationid = "3";
                InboxOutbox.notificationtitle = "T#0178";
                InboxOutboxeinfo.Add(InboxOutbox);

                InboxOutbox = new InboxOutbox();
                InboxOutbox.createUserAvatar = "particJohnson.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "particJohnson";
                InboxOutbox.notificationDescription = "Ticket #0178 is resolved on 01-03-2021, please check";
                InboxOutbox.notificationid = "4";
                InboxOutbox.notificationtitle = "T#018";
                InboxOutboxeinfo.Add(InboxOutbox);

                InboxOutbox = new InboxOutbox();
                InboxOutbox.createUserAvatar = "angieGabel.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "angieGabel";
                InboxOutbox.notificationDescription = "Ticket #0171 is resolved on 01-03-2021, please check";
                InboxOutbox.notificationid = "5";
                InboxOutbox.notificationtitle = "T#0171";
                InboxOutboxeinfo.Add(InboxOutbox);

                InboxOutbox = new InboxOutbox();
                InboxOutbox.createUserAvatar = "kristinGarrett.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "Kristin Garrett";
                InboxOutbox.notificationDescription = "Ticket #0145 is resolved on 01-03-2021, please check";
                InboxOutbox.notificationid = "6";
                InboxOutbox.notificationtitle = "T#0145";
                InboxOutboxeinfo.Add(InboxOutbox);

                InboxOutbox = new InboxOutbox();
                InboxOutbox.createUserAvatar = "micheleLlamas.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "micheleLlamas";
                InboxOutbox.notificationDescription = "Ticket #0187 is resolved on 01-03-2021, please check";
                InboxOutbox.notificationid = "7";
                InboxOutbox.notificationtitle = "T#087";
                InboxOutboxeinfo.Add(InboxOutbox);

                InboxOutbox = new InboxOutbox();
                InboxOutbox.createUserAvatar = "kristinGarrett.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "Kristin Garrett";
                InboxOutbox.notificationDescription = "Ticket #0178 is resolved on 01-03-2021, please check";
                InboxOutbox.notificationid = "8";
                InboxOutbox.notificationtitle = "T#0178";
                InboxOutboxeinfo.Add(InboxOutbox);

                InboxOutbox = new InboxOutbox();
                InboxOutbox.createUserAvatar = "kristinGarrett.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "Kristin Garrett";
                InboxOutbox.notificationDescription = "Ticket #0178 is resolved on 01-03-2021, please check";
                InboxOutbox.notificationid = "9";
                InboxOutbox.notificationtitle = "T#0178";
                InboxOutboxeinfo.Add(InboxOutbox);

                InboxOutbox = new InboxOutbox();
                InboxOutbox.createUserAvatar = "kristinGarrett.png";
                InboxOutbox.createdDate = "01-05-2021";
                InboxOutbox.createdUser = "Kristin Garrett";
                InboxOutbox.notificationDescription = "Ticket #0178 is resolved on 01-03-2021, please check";
                InboxOutbox.notificationid = "10";
                InboxOutbox.notificationtitle = "T#0178";
                InboxOutboxeinfo.Add(InboxOutbox);

                inboxoutboxlist.InboxOutboxeinfo = new List<InboxOutbox>();
                inboxoutboxlist.InboxOutboxeinfo = InboxOutboxeinfo;





            }
            catch (Exception ex)
            {
                throw ex;
            }

            return inboxoutboxlist;

        }



    } 
}
