namespace Vector.Common.BusinessLayer
{
    using System.ComponentModel;

    /// <summary>
    /// Email Template Enums
    /// </summary>
    public static class EmailTemplateEnums
    {
        /// <summary>
        /// Email Template GUIDs
        /// </summary>
        public enum EmailTemplateGUIDs
        {
            [Description("C1F0FB0D-0519-4760-8169-C70A9E3F6DCC")]
            CommonEmail,
            [Description("5AE54A61-CA10-4C0F-A9D2-C8036BD16ADF")]
            RequestBidEmail,
            [Description("913CBD68-A9EF-4A67-BD85-31F5F596E6F8")]
            TransmittingContractToHauler,
            [Description("37CB97CC-0653-41CB-8DB7-20EE2BC31F30")]
            TransmittingContractToClient,
            [Description("24CFDFE1-9343-4B60-8A7C-1276F126CA64")]
            TransmittingContractToVector,
            [Description("1A90BFA4-6569-4404-BD5B-AB7FD2558F78")]
            ReOpenException,
            [Description("23C19850-FC37-425D-88A0-823E0941E3B8")]
            TicketStatusUpdateEmail,
            [Description("02756F94-812E-4FD7-AE0C-7221DFACA31A")]
            ContractStatusToNegotiatorSP,
            [Description("90D79830-747F-4BBB-96F0-F46D184718A7")]
            ClientCreationEmail,
            [Description("4FADEF4C-5625-4AED-813D-72042CC5C98F")]
            NegotiationRevisitEmail,
            [Description("094C307C-74CD-4D6F-A25C-9079A91019FF")]
            VendorRequestBidEmail,
            [Description("8AD78FB8-6E23-4C08-B140-B25D81E9BFF1")]
            TicketRequestorEmail,
            [Description("761AC72C-B582-4B84-A9E7-7C80BE5378CC")]
            ContractVendorRejectedEmail,
            [Description("3AA2393C-7985-46E6-8854-BC27916F5B18")]
            NegotiationPreBidRequestEmail,
            [Description("19C5772C-951F-419F-A1A3-88B753275532")]
            RequestBidEmailIndividualVendor
        }

        /// <summary>
        /// Email Templates
        /// </summary>
        public enum EmailTemplates
        {
            /// <summary>
            /// Common Template
            /// </summary>
            [Description("Common/Common.xml")]
            Common,

            /// <summary>
            /// Negotiations Template
            /// </summary>
            [Description("Negotiations/Negotiations.xml")]
            Negotiations,

            [Description("Exceptions/Exceptions.xml")]
            Exceptions,


            [Description("Contracts/Contracts.xml")]
            Contracts
        }

        /// <summary>
        /// Xml Nodes
        /// </summary>
        public enum XmlNodes
        {
            EmailTemplates,
            Template,
            EGuid,
            EName,
            Subject,
            EmailBody,
            IsHtml,
            Images,
            Image,
            Path,
            ContentId,
            Signature
        }

    }
}
