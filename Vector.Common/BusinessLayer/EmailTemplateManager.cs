using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using static Vector.Common.BusinessLayer.EmailTemplateEnums;

namespace Vector.Common.BusinessLayer
{
    /// <summary>
    /// 
    /// </summary>
    public static class EmailTemplateManager
    {
        private enum EmailEntity
        {
            Subject,
            EmailBody,
            Signature
        }

        private const string EmailTemplatesFolder = "EmailTemplates";
        /// <summary>
        /// Gets Email Template
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="EmailTemplate"></param>
        /// <param name="eguid"></param>
        /// <returns></returns>
        public static EmailTemplate GetEmailTemplate(string basePath, EmailTemplates EmailTemplate, EmailTemplateGUIDs eguid)
        {
            var xmlEmailTemplates = XmlManager.LoadXmlDocument(Path.Combine(basePath, EmailTemplatesFolder, EmailTemplate.Desc()));
            var val =
                (from template in
                     xmlEmailTemplates.Element(XmlNodes.EmailTemplates.ToString())
                                      .Elements(XmlNodes.Template.ToString())
                 where StringManager.IsEqual(Convert.ToString(template.Attribute(XmlNodes.EGuid.ToString()).Value), eguid.Desc())
                 select template);

            if (val.ToList().Count() <= 0)
                return null;
            else
            {
                using (EmailTemplate emailTemplate = new EmailTemplate())
                {
                    emailTemplate.Subject = GetEmailEntity(val, EmailEntity.Subject);
                    emailTemplate.EmailBody = GetEmailEntity(val, EmailEntity.EmailBody);
                    emailTemplate.IsBodyHtml = GetIsBodyHTML(val);
                    emailTemplate.EmailImagesList = GetImages(val);
                    emailTemplate.Signature = GetEmailEntity(val, EmailEntity.Signature);
                    return emailTemplate;
                }
            }
        }

        private static string GetEmailEntity(IEnumerable<XElement> val, EmailEntity emailEntity)
        {
            if (StringManager.IsEqual(emailEntity.ToString(), EmailEntity.Subject.ToString()))
                return val.Elements(XmlNodes.Subject.ToString()).FirstOrDefault().Value;
            if (StringManager.IsEqual(emailEntity.ToString(), EmailEntity.EmailBody.ToString()))
                return val.Elements(XmlNodes.EmailBody.ToString()).FirstOrDefault().Value;
            if (StringManager.IsEqual(emailEntity.ToString(), EmailEntity.Signature.ToString()))
                return val.Elements(XmlNodes.Signature.ToString()).FirstOrDefault().Value;

            return string.Empty;
        }

        private static bool GetIsBodyHTML(IEnumerable<XElement> val)
        {
            //return val.Elements(XmlNodes.EmailBody.ToString()).FirstOrDefault().Attribute(XmlNodes.IsHtml.ToString()).Value.ToUpper() == ConstantMgr.True.ToUpper() ? true : false;
            return Convert.ToBoolean(val.Elements(XmlNodes.EmailBody.ToString()).FirstOrDefault().Attribute(XmlNodes.IsHtml.ToString()).Value);
        }

        private static List<EmailImage> GetImages(IEnumerable<XElement> val)
        {
            string value = string.Empty;
            List<EmailImage> lstImage = new List<EmailImage>();
            foreach (
                    var image in
                        val.Elements(XmlNodes.Images.ToString())
                           .Elements(XmlNodes.Image.ToString()))
            {
                lstImage.Add(new EmailImage() { Path = image.Element(XmlNodes.Path.ToString()).Value, ContentId = image.Element(XmlNodes.ContentId.ToString()).Value });
            }
            return lstImage;
        }
    }

    public class EmailTemplate : DisposeLogic
    {
        public string Subject { get; set; }

        public string EmailBody { get; set; }

        public bool IsBodyHtml { get; set; }

        public List<EmailImage> EmailImagesList { get; set; }

        public string Signature { get; set; }

        public string ToMailIds { get; set; }
    }


    public class EmailImage : DisposeLogic
    {
        public string Path { get; set; }

        public string ContentId { get; set; }
    }
}