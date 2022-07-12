using System.Xml.Serialization;

namespace PaymentFile
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class File
    {

        private PmtRec[] pmtRecField;

        private Sender senderField;

        private Receiver receiverField;

        private Attachment attachmentField;

        private string[] validationErrorsField;

        private FileInfoGrp fileInfoGrpField;

        private bool validFlagField;

        private string companyIDField;

        private string documentTypeField;

        private string pmtRecCountField;

        private string pmtRecTotalField;

        public File()
        {
            this.validFlagField = true;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PmtRec")]
        public PmtRec[] PmtRec
        {
            get
            {
                return this.pmtRecField;
            }
            set
            {
                this.pmtRecField = value;
            }
        }

        /// <remarks/>
        public Sender Sender
        {
            get
            {
                return this.senderField;
            }
            set
            {
                this.senderField = value;
            }
        }

        /// <remarks/>
        public Receiver Receiver
        {
            get
            {
                return this.receiverField;
            }
            set
            {
                this.receiverField = value;
            }
        }

        /// <remarks/>
        public Attachment Attachment
        {
            get
            {
                return this.attachmentField;
            }
            set
            {
                this.attachmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ValidationErrors")]
        public string[] ValidationErrors
        {
            get
            {
                return this.validationErrorsField;
            }
            set
            {
                this.validationErrorsField = value;
            }
        }

        /// <remarks/>
        public FileInfoGrp FileInfoGrp
        {
            get
            {
                return this.fileInfoGrpField;
            }
            set
            {
                this.fileInfoGrpField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool ValidFlag
        {
            get
            {
                return this.validFlagField;
            }
            set
            {
                this.validFlagField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CompanyID
        {
            get
            {
                return this.companyIDField;
            }
            set
            {
                this.companyIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DocumentType
        {
            get
            {
                return this.documentTypeField;
            }
            set
            {
                this.documentTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PmtRecCount
        {
            get
            {
                return this.pmtRecCountField;
            }
            set
            {
                this.pmtRecCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PmtRecTotal
        {
            get
            {
                return this.pmtRecTotalField;
            }
            set
            {
                this.pmtRecTotalField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class PmtRec
    {

        private RefInfo[] refInfoField;

        private IDInfo[] iDInfoField;

        private DateInfo[] dateInfoField;

        private Message[] messageField;

        private Check checkField;

        private OrgnrParty orgnrPartyField;

        private RcvrParty rcvrPartyField;

        private OrgnrDepAcctID orgnrDepAcctIDField;

        private RcvrDepAcctID rcvrDepAcctIDField;

        private IntermediaryDepAcctID[] intermediaryDepAcctIDField;

        private VendorParty vendorPartyField;

        private PmtDetail pmtDetailField;

        private string[] validationErrorsField;

        private Attachment attachmentField;

        private string pmtIDField;

        private PmtSuppCCR pmtSuppCCRField;

        private string pDPHandlingCodeField;

        private DocDelivery docDeliveryField;

        private decimal curAmtField;

        private string curCodeField;

        private System.DateTime prcDateField;

        private bool prcDateFieldSpecified;

        private System.DateTime valueDateField;

        private bool valueDateFieldSpecified;

        private System.DateTime rlsDateField;

        private bool rlsDateFieldSpecified;

        private bool validFlagField;

        private PmtRecPmtFormat pmtFormatField;

        private bool pmtFormatFieldSpecified;

        private PmtRecPmtFormatIntl pmtFormatIntlField;

        private bool pmtFormatIntlFieldSpecified;

        private PmtRecPmtMethod pmtMethodField;

        private PmtRecDeliveryMethod deliveryMethodField;

        private bool deliveryMethodFieldSpecified;

        private PmtRecTranHandlingCode tranHandlingCodeField;

        private bool tranHandlingCodeFieldSpecified;

        private PmtRecPmtCrDr pmtCrDrField;

        private bool pmtCrDrFieldSpecified;

        public PmtRec()
        {
            this.validFlagField = true;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RefInfo")]
        public RefInfo[] RefInfo
        {
            get
            {
                return this.refInfoField;
            }
            set
            {
                this.refInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("IDInfo")]
        public IDInfo[] IDInfo
        {
            get
            {
                return this.iDInfoField;
            }
            set
            {
                this.iDInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DateInfo")]
        public DateInfo[] DateInfo
        {
            get
            {
                return this.dateInfoField;
            }
            set
            {
                this.dateInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Message")]
        public Message[] Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }

        /// <remarks/>
        public Check Check
        {
            get
            {
                return this.checkField;
            }
            set
            {
                this.checkField = value;
            }
        }

        /// <remarks/>
        public OrgnrParty OrgnrParty
        {
            get
            {
                return this.orgnrPartyField;
            }
            set
            {
                this.orgnrPartyField = value;
            }
        }

        /// <remarks/>
        public RcvrParty RcvrParty
        {
            get
            {
                return this.rcvrPartyField;
            }
            set
            {
                this.rcvrPartyField = value;
            }
        }

        /// <remarks/>
        public OrgnrDepAcctID OrgnrDepAcctID
        {
            get
            {
                return this.orgnrDepAcctIDField;
            }
            set
            {
                this.orgnrDepAcctIDField = value;
            }
        }

        /// <remarks/>
        public RcvrDepAcctID RcvrDepAcctID
        {
            get
            {
                return this.rcvrDepAcctIDField;
            }
            set
            {
                this.rcvrDepAcctIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("IntermediaryDepAcctID")]
        public IntermediaryDepAcctID[] IntermediaryDepAcctID
        {
            get
            {
                return this.intermediaryDepAcctIDField;
            }
            set
            {
                this.intermediaryDepAcctIDField = value;
            }
        }

        /// <remarks/>
        public VendorParty VendorParty
        {
            get
            {
                return this.vendorPartyField;
            }
            set
            {
                this.vendorPartyField = value;
            }
        }

        /// <remarks/>
        public PmtDetail PmtDetail
        {
            get
            {
                return this.pmtDetailField;
            }
            set
            {
                this.pmtDetailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ValidationErrors")]
        public string[] ValidationErrors
        {
            get
            {
                return this.validationErrorsField;
            }
            set
            {
                this.validationErrorsField = value;
            }
        }

        /// <remarks/>
        public Attachment Attachment
        {
            get
            {
                return this.attachmentField;
            }
            set
            {
                this.attachmentField = value;
            }
        }

        /// <remarks/>
        public string PmtID
        {
            get
            {
                return this.pmtIDField;
            }
            set
            {
                this.pmtIDField = value;
            }
        }

        /// <remarks/>
        public PmtSuppCCR PmtSuppCCR
        {
            get
            {
                return this.pmtSuppCCRField;
            }
            set
            {
                this.pmtSuppCCRField = value;
            }
        }

        /// <remarks/>
        public string PDPHandlingCode
        {
            get
            {
                return this.pDPHandlingCodeField;
            }
            set
            {
                this.pDPHandlingCodeField = value;
            }
        }

        /// <remarks/>
        public DocDelivery DocDelivery
        {
            get
            {
                return this.docDeliveryField;
            }
            set
            {
                this.docDeliveryField = value;
            }
        }

        /// <remarks/>
        public decimal CurAmt
        {
            get
            {
                return this.curAmtField;
            }
            set
            {
                this.curAmtField = value;
            }
        }

        /// <remarks/>
        public string CurCode
        {
            get
            {
                return this.curCodeField;
            }
            set
            {
                this.curCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime PrcDate
        {
            get
            {
                return this.prcDateField;
            }
            set
            {
                this.prcDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PrcDateSpecified
        {
            get
            {
                return this.prcDateFieldSpecified;
            }
            set
            {
                this.prcDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime ValueDate
        {
            get
            {
                return this.valueDateField;
            }
            set
            {
                this.valueDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValueDateSpecified
        {
            get
            {
                return this.valueDateFieldSpecified;
            }
            set
            {
                this.valueDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime RlsDate
        {
            get
            {
                return this.rlsDateField;
            }
            set
            {
                this.rlsDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RlsDateSpecified
        {
            get
            {
                return this.rlsDateFieldSpecified;
            }
            set
            {
                this.rlsDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool ValidFlag
        {
            get
            {
                return this.validFlagField;
            }
            set
            {
                this.validFlagField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public PmtRecPmtFormat PmtFormat
        {
            get
            {
                return this.pmtFormatField;
            }
            set
            {
                this.pmtFormatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PmtFormatSpecified
        {
            get
            {
                return this.pmtFormatFieldSpecified;
            }
            set
            {
                this.pmtFormatFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public PmtRecPmtFormatIntl PmtFormatIntl
        {
            get
            {
                return this.pmtFormatIntlField;
            }
            set
            {
                this.pmtFormatIntlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PmtFormatIntlSpecified
        {
            get
            {
                return this.pmtFormatIntlFieldSpecified;
            }
            set
            {
                this.pmtFormatIntlFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public PmtRecPmtMethod PmtMethod
        {
            get
            {
                return this.pmtMethodField;
            }
            set
            {
                this.pmtMethodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public PmtRecDeliveryMethod DeliveryMethod
        {
            get
            {
                return this.deliveryMethodField;
            }
            set
            {
                this.deliveryMethodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DeliveryMethodSpecified
        {
            get
            {
                return this.deliveryMethodFieldSpecified;
            }
            set
            {
                this.deliveryMethodFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public PmtRecTranHandlingCode TranHandlingCode
        {
            get
            {
                return this.tranHandlingCodeField;
            }
            set
            {
                this.tranHandlingCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TranHandlingCodeSpecified
        {
            get
            {
                return this.tranHandlingCodeFieldSpecified;
            }
            set
            {
                this.tranHandlingCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public PmtRecPmtCrDr PmtCrDr
        {
            get
            {
                return this.pmtCrDrField;
            }
            set
            {
                this.pmtCrDrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PmtCrDrSpecified
        {
            get
            {
                return this.pmtCrDrFieldSpecified;
            }
            set
            {
                this.pmtCrDrFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum PmtRecPmtFormat
    {

        /// <remarks/>
        CCD,

        /// <remarks/>
        CCP,

        /// <remarks/>
        PPD,

        /// <remarks/>
        PPP,

        /// <remarks/>
        CTX,

        /// <remarks/>
        BTF,

        /// <remarks/>
        NBT,

        /// <remarks/>
        IAT,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum PmtRecPmtFormatIntl
    {

        /// <remarks/>
        PAD,

        /// <remarks/>
        DEP,

        /// <remarks/>
        PEN,

        /// <remarks/>
        SAL,

        /// <remarks/>
        INS,

        /// <remarks/>
        ANN,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum PmtRecPmtMethod
    {

        /// <remarks/>
        DAC,

        /// <remarks/>
        IAC,

        /// <remarks/>
        CHK,

        /// <remarks/>
        IWI,

        /// <remarks/>
        MTS,

        /// <remarks/>
        FXC,

        /// <remarks/>
        SDC,

        /// <remarks/>
        CPC,

        /// <remarks/>
        WTX,

        /// <remarks/>
        CCR,

        /// <remarks/>
        FDP,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum PmtRecDeliveryMethod
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("000")]
        Item000,

        /// <remarks/>
        X,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("001")]
        Item001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("002")]
        Item002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("003")]
        Item003,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("004")]
        Item004,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("005")]
        Item005,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("006")]
        Item006,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("007")]
        Item007,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("008")]
        Item008,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("009")]
        Item009,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum PmtRecTranHandlingCode
    {

        /// <remarks/>
        C,

        /// <remarks/>
        D,

        /// <remarks/>
        U,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum PmtRecPmtCrDr
    {

        /// <remarks/>
        C,

        /// <remarks/>
        D,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class RefInfo
    {

        private string refIDField;

        private string refTypeField;

        /// <remarks/>
        public string RefID
        {
            get
            {
                return this.refIDField;
            }
            set
            {
                this.refIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RefType
        {
            get
            {
                return this.refTypeField;
            }
            set
            {
                this.refTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class IDInfo
    {

        private string idField;

        private string iDTypeField;

        /// <remarks/>
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IDType
        {
            get
            {
                return this.iDTypeField;
            }
            set
            {
                this.iDTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class DateInfo
    {

        private string dateField;

        private string dateTypeField;

        /// <remarks/>
        public string Date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DateType
        {
            get
            {
                return this.dateTypeField;
            }
            set
            {
                this.dateTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class Message
    {

        private string msgTextField;

        private MessageMsgType msgTypeField;

        private bool msgTypeFieldSpecified;

        /// <remarks/>
        public string MsgText
        {
            get
            {
                return this.msgTextField;
            }
            set
            {
                this.msgTextField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public MessageMsgType MsgType
        {
            get
            {
                return this.msgTypeField;
            }
            set
            {
                this.msgTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MsgTypeSpecified
        {
            get
            {
                return this.msgTypeFieldSpecified;
            }
            set
            {
                this.msgTypeFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum MessageMsgType
    {

        /// <remarks/>
        OBI,

        /// <remarks/>
        BBI,

        /// <remarks/>
        ACH,

        /// <remarks/>
        CHK,

        /// <remarks/>
        ADD,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class Check
    {

        private string chkNumField;

        private string chkDocNumField;

        private string chkCtrlNumField;

        /// <remarks/>
        public string ChkNum
        {
            get
            {
                return this.chkNumField;
            }
            set
            {
                this.chkNumField = value;
            }
        }

        /// <remarks/>
        public string ChkDocNum
        {
            get
            {
                return this.chkDocNumField;
            }
            set
            {
                this.chkDocNumField = value;
            }
        }

        /// <remarks/>
        public string ChkCtrlNum
        {
            get
            {
                return this.chkCtrlNumField;
            }
            set
            {
                this.chkCtrlNumField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class OrgnrParty
    {

        private Name nameField;

        private RefInfo[] refInfoField;

        private PostAddr postAddrField;

        private ContactInfo contactInfoField;

        /// <remarks/>
        public Name Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RefInfo")]
        public RefInfo[] RefInfo
        {
            get
            {
                return this.refInfoField;
            }
            set
            {
                this.refInfoField = value;
            }
        }

        /// <remarks/>
        public PostAddr PostAddr
        {
            get
            {
                return this.postAddrField;
            }
            set
            {
                this.postAddrField = value;
            }
        }

        /// <remarks/>
        public ContactInfo ContactInfo
        {
            get
            {
                return this.contactInfoField;
            }
            set
            {
                this.contactInfoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class Name
    {

        private string name1Field;

        private string name2Field;

        /// <remarks/>
        public string Name1
        {
            get
            {
                return this.name1Field;
            }
            set
            {
                this.name1Field = value;
            }
        }

        /// <remarks/>
        public string Name2
        {
            get
            {
                return this.name2Field;
            }
            set
            {
                this.name2Field = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class PostAddr
    {

        private string addr1Field;

        private string addr2Field;

        private string cityField;

        private string stateProvField;

        private string postalCodeField;

        private string countryField;

        private string countryNameField;

        /// <remarks/>
        public string Addr1
        {
            get
            {
                return this.addr1Field;
            }
            set
            {
                this.addr1Field = value;
            }
        }

        /// <remarks/>
        public string Addr2
        {
            get
            {
                return this.addr2Field;
            }
            set
            {
                this.addr2Field = value;
            }
        }

        /// <remarks/>
        public string City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }

        /// <remarks/>
        public string StateProv
        {
            get
            {
                return this.stateProvField;
            }
            set
            {
                this.stateProvField = value;
            }
        }

        /// <remarks/>
        public string PostalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                this.postalCodeField = value;
            }
        }

        /// <remarks/>
        public string Country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }

        /// <remarks/>
        public string CountryName
        {
            get
            {
                return this.countryNameField;
            }
            set
            {
                this.countryNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class DepAcctID
    {

        private BankInfo bankInfoField;

        private string acctIDField;

        private DepAcctIDAcctType acctTypeField;

        private bool acctTypeFieldSpecified;

        private string acctCurField;

        /// <remarks/>
        public BankInfo BankInfo
        {
            get
            {
                return this.bankInfoField;
            }
            set
            {
                this.bankInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AcctID
        {
            get
            {
                return this.acctIDField;
            }
            set
            {
                this.acctIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public DepAcctIDAcctType AcctType
        {
            get
            {
                return this.acctTypeField;
            }
            set
            {
                this.acctTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AcctTypeSpecified
        {
            get
            {
                return this.acctTypeFieldSpecified;
            }
            set
            {
                this.acctTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AcctCur
        {
            get
            {
                return this.acctCurField;
            }
            set
            {
                this.acctCurField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum DepAcctIDAcctType
    {

        /// <remarks/>
        C,

        /// <remarks/>
        S,

        /// <remarks/>
        D,

        /// <remarks/>
        G,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class BankInfo
    {

        private RefInfo[] refInfoField;

        private PostAddr postAddrField;

        private string bankIDField;

        private string nameField;

        private string branchIDField;

        private string branchNameField;

        private BankInfoBankIDType bankIDTypeField;

        private bool bankIDTypeFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RefInfo")]
        public RefInfo[] RefInfo
        {
            get
            {
                return this.refInfoField;
            }
            set
            {
                this.refInfoField = value;
            }
        }

        /// <remarks/>
        public PostAddr PostAddr
        {
            get
            {
                return this.postAddrField;
            }
            set
            {
                this.postAddrField = value;
            }
        }

        /// <remarks/>
        public string BankID
        {
            get
            {
                return this.bankIDField;
            }
            set
            {
                this.bankIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BranchID
        {
            get
            {
                return this.branchIDField;
            }
            set
            {
                this.branchIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BranchName
        {
            get
            {
                return this.branchNameField;
            }
            set
            {
                this.branchNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public BankInfoBankIDType BankIDType
        {
            get
            {
                return this.bankIDTypeField;
            }
            set
            {
                this.bankIDTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BankIDTypeSpecified
        {
            get
            {
                return this.bankIDTypeFieldSpecified;
            }
            set
            {
                this.bankIDTypeFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum BankInfoBankIDType
    {

        /// <remarks/>
        ABA,

        /// <remarks/>
        SWT,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class RcvrParty
    {

        private Name nameField;

        private RefInfo[] refInfoField;

        private PostAddr postAddrField;

        private ContactInfo contactInfoField;

        /// <remarks/>
        public Name Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RefInfo")]
        public RefInfo[] RefInfo
        {
            get
            {
                return this.refInfoField;
            }
            set
            {
                this.refInfoField = value;
            }
        }

        /// <remarks/>
        public PostAddr PostAddr
        {
            get
            {
                return this.postAddrField;
            }
            set
            {
                this.postAddrField = value;
            }
        }

        /// <remarks/>
        public ContactInfo ContactInfo
        {
            get
            {
                return this.contactInfoField;
            }
            set
            {
                this.contactInfoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class ContactInfo
    {

        private PhoneNum phoneNumField;

        private string emailAddrField;

        private string uRLField;

        private string nameField;

        /// <remarks/>
        public PhoneNum PhoneNum
        {
            get
            {
                return this.phoneNumField;
            }
            set
            {
                this.phoneNumField = value;
            }
        }

        /// <remarks/>
        public string EmailAddr
        {
            get
            {
                return this.emailAddrField;
            }
            set
            {
                this.emailAddrField = value;
            }
        }

        /// <remarks/>
        public string URL
        {
            get
            {
                return this.uRLField;
            }
            set
            {
                this.uRLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class PhoneNum
    {

        private PhoneNumPhoneType phoneTypeField;

        private bool phoneTypeFieldSpecified;

        private string phoneField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public PhoneNumPhoneType PhoneType
        {
            get
            {
                return this.phoneTypeField;
            }
            set
            {
                this.phoneTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PhoneTypeSpecified
        {
            get
            {
                return this.phoneTypeFieldSpecified;
            }
            set
            {
                this.phoneTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum PhoneNumPhoneType
    {

        /// <remarks/>
        FX,

        /// <remarks/>
        TE,

        /// <remarks/>
        TL,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class PmtDetail
    {

        private InvoiceInfo[] invoiceInfoField;

        private Payroll payrollField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("InvoiceInfo")]
        public InvoiceInfo[] InvoiceInfo
        {
            get
            {
                return this.invoiceInfoField;
            }
            set
            {
                this.invoiceInfoField = value;
            }
        }

        /// <remarks/>
        public Payroll Payroll
        {
            get
            {
                return this.payrollField;
            }
            set
            {
                this.payrollField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class InvoiceInfo
    {

        private RefInfo[] refInfoField;

        private Note[] noteField;

        private TaxInfo taxInfoField;

        private InvoiceAdj[] invoiceAdjField;

        private POInfo[] pOInfoField;

        private string pmtActionCodeField;

        private string customerNameField;

        private string accountNumberField;

        private decimal netCurAmtField;

        private bool netCurAmtFieldSpecified;

        private System.DateTime invocieDateField;

        private bool invocieDateFieldSpecified;

        private System.DateTime servicePeriodFromField;

        private bool servicePeriodFromFieldSpecified;

        private System.DateTime servicePeriodToField;

        private bool servicePeriodToFieldSpecified;

        private string invoiceNumField;

        private string transactionIDField;

        private InvoiceInfoInvoiceType invoiceTypeField;

        private bool invoiceTypeFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RefInfo")]
        public RefInfo[] RefInfo
        {
            get
            {
                return this.refInfoField;
            }
            set
            {
                this.refInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Note")]
        public Note[] Note
        {
            get
            {
                return this.noteField;
            }
            set
            {
                this.noteField = value;
            }
        }

        /// <remarks/>
        public TaxInfo TaxInfo
        {
            get
            {
                return this.taxInfoField;
            }
            set
            {
                this.taxInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("InvoiceAdj")]
        public InvoiceAdj[] InvoiceAdj
        {
            get
            {
                return this.invoiceAdjField;
            }
            set
            {
                this.invoiceAdjField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("POInfo")]
        public POInfo[] POInfo
        {
            get
            {
                return this.pOInfoField;
            }
            set
            {
                this.pOInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PmtActionCode
        {
            get
            {
                return this.pmtActionCodeField;
            }
            set
            {
                this.pmtActionCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CustomerName
        {
            get
            {
                return this.customerNameField;
            }
            set
            {
                this.customerNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AccountNumber
        {
            get
            {
                return this.accountNumberField;
            }
            set
            {
                this.accountNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal NetCurAmt
        {
            get
            {
                return this.netCurAmtField;
            }
            set
            {
                this.netCurAmtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NetCurAmtSpecified
        {
            get
            {
                return this.netCurAmtFieldSpecified;
            }
            set
            {
                this.netCurAmtFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime InvoiceDate
        {
            get
            {
                return this.invocieDateField;
            }
            set
            {
                this.invocieDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool InvocieDateSpecified
        {
            get
            {
                return this.invocieDateFieldSpecified;
            }
            set
            {
                this.invocieDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime ServicePeriodFrom
        {
            get
            {
                return this.servicePeriodFromField;
            }
            set
            {
                this.servicePeriodFromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ServicePeriodFromSpecified
        {
            get
            {
                return this.servicePeriodFromFieldSpecified;
            }
            set
            {
                this.servicePeriodFromFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime ServicePeriodTo
        {
            get
            {
                return this.servicePeriodToField;
            }
            set
            {
                this.servicePeriodToField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ServicePeriodToSpecified
        {
            get
            {
                return this.servicePeriodToFieldSpecified;
            }
            set
            {
                this.servicePeriodToFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InvoiceNum
        {
            get
            {
                return this.invoiceNumField;
            }
            set
            {
                this.invoiceNumField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PaymentId
        {
            get;
            set;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TransactionID
        {
            get
            {
                return this.transactionIDField;
            }
            set
            {
                this.transactionIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public InvoiceInfoInvoiceType InvoiceType
        {
            get
            {
                return this.invoiceTypeField;
            }
            set
            {
                this.invoiceTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool InvoiceTypeSpecified
        {
            get
            {
                return this.invoiceTypeFieldSpecified;
            }
            set
            {
                this.invoiceTypeFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum InvoiceInfoInvoiceType
    {

        /// <remarks/>
        IV,

        /// <remarks/>
        CM,

        /// <remarks/>
        PO,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class TaxInfo
    {

        private string orgIDField;

        private decimal curAmtField;

        private bool curAmtFieldSpecified;

        private decimal rateField;

        private bool rateFieldSpecified;

        private string taxTypeField;

        /// <remarks/>
        public string OrgID
        {
            get
            {
                return this.orgIDField;
            }
            set
            {
                this.orgIDField = value;
            }
        }

        /// <remarks/>
        public decimal CurAmt
        {
            get
            {
                return this.curAmtField;
            }
            set
            {
                this.curAmtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CurAmtSpecified
        {
            get
            {
                return this.curAmtFieldSpecified;
            }
            set
            {
                this.curAmtFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal Rate
        {
            get
            {
                return this.rateField;
            }
            set
            {
                this.rateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RateSpecified
        {
            get
            {
                return this.rateFieldSpecified;
            }
            set
            {
                this.rateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TaxType
        {
            get
            {
                return this.taxTypeField;
            }
            set
            {
                this.taxTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class InvoiceAdj
    {

        private string invoiceAdjNumField;

        private Note[] noteField;

        private decimal curAmtField;

        private bool curAmtFieldSpecified;

        private System.DateTime effDtField;

        private bool effDtFieldSpecified;

        private string descField;

        private RAInfo[] rAInfoField;

        private string adjTypeField;

        private string adjNumTypeField;

        /// <remarks/>
        public string InvoiceAdjNum
        {
            get
            {
                return this.invoiceAdjNumField;
            }
            set
            {
                this.invoiceAdjNumField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Note")]
        public Note[] Note
        {
            get
            {
                return this.noteField;
            }
            set
            {
                this.noteField = value;
            }
        }

        /// <remarks/>
        public decimal CurAmt
        {
            get
            {
                return this.curAmtField;
            }
            set
            {
                this.curAmtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CurAmtSpecified
        {
            get
            {
                return this.curAmtFieldSpecified;
            }
            set
            {
                this.curAmtFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime EffDt
        {
            get
            {
                return this.effDtField;
            }
            set
            {
                this.effDtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EffDtSpecified
        {
            get
            {
                return this.effDtFieldSpecified;
            }
            set
            {
                this.effDtFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string Desc
        {
            get
            {
                return this.descField;
            }
            set
            {
                this.descField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RAInfo")]
        public RAInfo[] RAInfo
        {
            get
            {
                return this.rAInfoField;
            }
            set
            {
                this.rAInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AdjType
        {
            get
            {
                return this.adjTypeField;
            }
            set
            {
                this.adjTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AdjNumType
        {
            get
            {
                return this.adjNumTypeField;
            }
            set
            {
                this.adjNumTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class Payroll
    {

        private PyrlCurr pyrlCurrField;

        private PyrlYTD pyrlYTDField;

        private EarningsItem[] earningsItemField;

        private TaxItem[] taxItemField;

        private BefrTaxDctnItem[] befrTaxDctnItemField;

        private AftrTaxDctnItem[] aftrTaxDctnItemField;

        private GrnshmtDctnItem[] grnshmtDctnItemField;

        private System.DateTime pyrlPrdEndDateField;

        private bool pyrlPrdEndDateFieldSpecified;

        /// <remarks/>
        public PyrlCurr PyrlCurr
        {
            get
            {
                return this.pyrlCurrField;
            }
            set
            {
                this.pyrlCurrField = value;
            }
        }

        /// <remarks/>
        public PyrlYTD PyrlYTD
        {
            get
            {
                return this.pyrlYTDField;
            }
            set
            {
                this.pyrlYTDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EarningsItem")]
        public EarningsItem[] EarningsItem
        {
            get
            {
                return this.earningsItemField;
            }
            set
            {
                this.earningsItemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TaxItem")]
        public TaxItem[] TaxItem
        {
            get
            {
                return this.taxItemField;
            }
            set
            {
                this.taxItemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("BefrTaxDctnItem")]
        public BefrTaxDctnItem[] BefrTaxDctnItem
        {
            get
            {
                return this.befrTaxDctnItemField;
            }
            set
            {
                this.befrTaxDctnItemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AftrTaxDctnItem")]
        public AftrTaxDctnItem[] AftrTaxDctnItem
        {
            get
            {
                return this.aftrTaxDctnItemField;
            }
            set
            {
                this.aftrTaxDctnItemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GrnshmtDctnItem")]
        public GrnshmtDctnItem[] GrnshmtDctnItem
        {
            get
            {
                return this.grnshmtDctnItemField;
            }
            set
            {
                this.grnshmtDctnItemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime PyrlPrdEndDate
        {
            get
            {
                return this.pyrlPrdEndDateField;
            }
            set
            {
                this.pyrlPrdEndDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PyrlPrdEndDateSpecified
        {
            get
            {
                return this.pyrlPrdEndDateFieldSpecified;
            }
            set
            {
                this.pyrlPrdEndDateFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class PyrlCurr
    {

        private EarningsGrp earningsGrpField;

        /// <remarks/>
        public EarningsGrp EarningsGrp
        {
            get
            {
                return this.earningsGrpField;
            }
            set
            {
                this.earningsGrpField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class PyrlYTD
    {

        private EarningsGrp earningsGrpField;

        /// <remarks/>
        public EarningsGrp EarningsGrp
        {
            get
            {
                return this.earningsGrpField;
            }
            set
            {
                this.earningsGrpField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class EarningsGrp
    {

        private decimal earningsAmtField;

        private bool earningsAmtFieldSpecified;

        private decimal taxAmtField;

        private bool taxAmtFieldSpecified;

        private decimal dctnAmtField;

        private bool dctnAmtFieldSpecified;

        private decimal netPayAmtField;

        private bool netPayAmtFieldSpecified;

        /// <remarks/>
        public decimal EarningsAmt
        {
            get
            {
                return this.earningsAmtField;
            }
            set
            {
                this.earningsAmtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EarningsAmtSpecified
        {
            get
            {
                return this.earningsAmtFieldSpecified;
            }
            set
            {
                this.earningsAmtFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal TaxAmt
        {
            get
            {
                return this.taxAmtField;
            }
            set
            {
                this.taxAmtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TaxAmtSpecified
        {
            get
            {
                return this.taxAmtFieldSpecified;
            }
            set
            {
                this.taxAmtFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal DctnAmt
        {
            get
            {
                return this.dctnAmtField;
            }
            set
            {
                this.dctnAmtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DctnAmtSpecified
        {
            get
            {
                return this.dctnAmtFieldSpecified;
            }
            set
            {
                this.dctnAmtFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal NetPayAmt
        {
            get
            {
                return this.netPayAmtField;
            }
            set
            {
                this.netPayAmtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NetPayAmtSpecified
        {
            get
            {
                return this.netPayAmtFieldSpecified;
            }
            set
            {
                this.netPayAmtFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class EarningsItem
    {

        private string currHoursField;

        private string yTDHoursField;

        private AmtGroup amtGroupField;

        /// <remarks/>
        public string CurrHours
        {
            get
            {
                return this.currHoursField;
            }
            set
            {
                this.currHoursField = value;
            }
        }

        /// <remarks/>
        public string YTDHours
        {
            get
            {
                return this.yTDHoursField;
            }
            set
            {
                this.yTDHoursField = value;
            }
        }

        /// <remarks/>
        public AmtGroup AmtGroup
        {
            get
            {
                return this.amtGroupField;
            }
            set
            {
                this.amtGroupField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class AmtGroup
    {

        private decimal currAmtField;

        private bool currAmtFieldSpecified;

        private decimal yTDAmtField;

        private bool yTDAmtFieldSpecified;

        private string descField;

        /// <remarks/>
        public decimal CurrAmt
        {
            get
            {
                return this.currAmtField;
            }
            set
            {
                this.currAmtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CurrAmtSpecified
        {
            get
            {
                return this.currAmtFieldSpecified;
            }
            set
            {
                this.currAmtFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal YTDAmt
        {
            get
            {
                return this.yTDAmtField;
            }
            set
            {
                this.yTDAmtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool YTDAmtSpecified
        {
            get
            {
                return this.yTDAmtFieldSpecified;
            }
            set
            {
                this.yTDAmtFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string Desc
        {
            get
            {
                return this.descField;
            }
            set
            {
                this.descField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class TaxItem
    {

        private AmtGroup amtGroupField;

        /// <remarks/>
        public AmtGroup AmtGroup
        {
            get
            {
                return this.amtGroupField;
            }
            set
            {
                this.amtGroupField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class BefrTaxDctnItem
    {

        private AmtGroup amtGroupField;

        /// <remarks/>
        public AmtGroup AmtGroup
        {
            get
            {
                return this.amtGroupField;
            }
            set
            {
                this.amtGroupField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class AftrTaxDctnItem
    {

        private AmtGroup amtGroupField;

        /// <remarks/>
        public AmtGroup AmtGroup
        {
            get
            {
                return this.amtGroupField;
            }
            set
            {
                this.amtGroupField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class GrnshmtDctnItem
    {

        private AmtGroup amtGroupField;

        /// <remarks/>
        public AmtGroup AmtGroup
        {
            get
            {
                return this.amtGroupField;
            }
            set
            {
                this.amtGroupField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class Sender
    {

        private string name1Field;

        private string idField;

        /// <remarks/>
        public string Name1
        {
            get
            {
                return this.name1Field;
            }
            set
            {
                this.name1Field = value;
            }
        }

        /// <remarks/>
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class Receiver
    {

        private string name1Field;

        private string idField;

        /// <remarks/>
        public string Name1
        {
            get
            {
                return this.name1Field;
            }
            set
            {
                this.name1Field = value;
            }
        }

        /// <remarks/>
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class Attachment
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class FileInfoGrp
    {

        private System.String fileDateField;

        private System.DateTime fileTimeField;

        private bool fileTimeFieldSpecified;

        private string fileControlNumberField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "string")]
        public System.String FileDate
        {
            get
            {
                return this.fileDateField;
            }
            set
            {
                this.fileDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "time")]
        public System.DateTime FileTime
        {
            get
            {
                return this.fileTimeField;
            }
            set
            {
                this.fileTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FileTimeSpecified
        {
            get
            {
                return this.fileTimeFieldSpecified;
            }
            set
            {
                this.fileTimeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FileControlNumber
        {
            get
            {
                return this.fileControlNumberField;
            }
            set
            {
                this.fileControlNumberField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class OrgnrDepAcctID
    {

        private DepAcctID depAcctIDField;

        /// <remarks/>
        public DepAcctID DepAcctID
        {
            get
            {
                return this.depAcctIDField;
            }
            set
            {
                this.depAcctIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class RcvrDepAcctID
    {

        private DepAcctID depAcctIDField;

        /// <remarks/>
        public DepAcctID DepAcctID
        {
            get
            {
                return this.depAcctIDField;
            }
            set
            {
                this.depAcctIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class IntermediaryDepAcctID
    {

        private DepAcctID depAcctIDField;

        /// <remarks/>
        public DepAcctID DepAcctID
        {
            get
            {
                return this.depAcctIDField;
            }
            set
            {
                this.depAcctIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class VendorParty
    {

        private Name nameField;

        private RefInfo[] refInfoField;

        private PostAddr postAddrField;

        private ContactInfo contactInfoField;

        /// <remarks/>
        public Name Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RefInfo")]
        public RefInfo[] RefInfo
        {
            get
            {
                return this.refInfoField;
            }
            set
            {
                this.refInfoField = value;
            }
        }

        /// <remarks/>
        public PostAddr PostAddr
        {
            get
            {
                return this.postAddrField;
            }
            set
            {
                this.postAddrField = value;
            }
        }

        /// <remarks/>
        public ContactInfo ContactInfo
        {
            get
            {
                return this.contactInfoField;
            }
            set
            {
                this.contactInfoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class POInfo
    {

        private string pONumField;

        private string descField;

        private string pOTypeField;

        /// <remarks/>
        public string PONum
        {
            get
            {
                return this.pONumField;
            }
            set
            {
                this.pONumField = value;
            }
        }

        /// <remarks/>
        public string Desc
        {
            get
            {
                return this.descField;
            }
            set
            {
                this.descField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string POType
        {
            get
            {
                return this.pOTypeField;
            }
            set
            {
                this.pOTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class Note
    {

        private string noteTextField;

        private string noteTypeField;

        /// <remarks/>
        public string NoteText
        {
            get
            {
                return this.noteTextField;
            }
            set
            {
                this.noteTextField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NoteType
        {
            get
            {
                return this.noteTypeField;
            }
            set
            {
                this.noteTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class RAInfo
    {

        private string rANumField;

        private System.DateTime rADateField;

        private bool rADateFieldSpecified;

        private string descField;

        private string rANumTypeField;

        /// <remarks/>
        public string RANum
        {
            get
            {
                return this.rANumField;
            }
            set
            {
                this.rANumField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime RADate
        {
            get
            {
                return this.rADateField;
            }
            set
            {
                this.rADateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RADateSpecified
        {
            get
            {
                return this.rADateFieldSpecified;
            }
            set
            {
                this.rADateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string Desc
        {
            get
            {
                return this.descField;
            }
            set
            {
                this.descField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RANumType
        {
            get
            {
                return this.rANumTypeField;
            }
            set
            {
                this.rANumTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class DocDelivery
    {

        private string eDDBillerIDField;

        private FileOut[] fileOutField;

        /// <remarks/>
        public string EDDBillerID
        {
            get
            {
                return this.eDDBillerIDField;
            }
            set
            {
                this.eDDBillerIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FileOut")]
        public FileOut[] FileOut
        {
            get
            {
                return this.fileOutField;
            }
            set
            {
                this.fileOutField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class FileOut
    {

        private string fileTypeField;

        private string fileFormatField;

        private Delivery[] deliveryField;

        /// <remarks/>
        public string FileType
        {
            get
            {
                return this.fileTypeField;
            }
            set
            {
                this.fileTypeField = value;
            }
        }

        /// <remarks/>
        public string FileFormat
        {
            get
            {
                return this.fileFormatField;
            }
            set
            {
                this.fileFormatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Delivery")]
        public Delivery[] Delivery
        {
            get
            {
                return this.deliveryField;
            }
            set
            {
                this.deliveryField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class Delivery
    {

        private string deliveryTypeField;

        private string deliveryContactNameField;

        private string deliveryFaxNumberField;

        private string deliveryEmailAddressField;

        private string deliveryUserIDField;

        private string deliveryCompanyIDField;

        private string secureTypeField;

        private string secureQuestion01Field;

        private string securePassword01Field;

        private string secureQuestion02Field;

        private string securePassword02Field;

        private string receivingAddress1Field;

        private string receivingAddress2Field;

        private string receivingCityField;

        private string receivingStateField;

        private string receivingZipField;

        private string receivingCountryField;

        private string receivingCountryCodeField;

        /// <remarks/>
        public string DeliveryType
        {
            get
            {
                return this.deliveryTypeField;
            }
            set
            {
                this.deliveryTypeField = value;
            }
        }

        /// <remarks/>
        public string DeliveryContactName
        {
            get
            {
                return this.deliveryContactNameField;
            }
            set
            {
                this.deliveryContactNameField = value;
            }
        }

        /// <remarks/>
        public string DeliveryFaxNumber
        {
            get
            {
                return this.deliveryFaxNumberField;
            }
            set
            {
                this.deliveryFaxNumberField = value;
            }
        }

        /// <remarks/>
        public string DeliveryEmailAddress
        {
            get
            {
                return this.deliveryEmailAddressField;
            }
            set
            {
                this.deliveryEmailAddressField = value;
            }
        }

        /// <remarks/>
        public string DeliveryUserID
        {
            get
            {
                return this.deliveryUserIDField;
            }
            set
            {
                this.deliveryUserIDField = value;
            }
        }

        /// <remarks/>
        public string DeliveryCompanyID
        {
            get
            {
                return this.deliveryCompanyIDField;
            }
            set
            {
                this.deliveryCompanyIDField = value;
            }
        }

        /// <remarks/>
        public string SecureType
        {
            get
            {
                return this.secureTypeField;
            }
            set
            {
                this.secureTypeField = value;
            }
        }

        /// <remarks/>
        public string SecureQuestion01
        {
            get
            {
                return this.secureQuestion01Field;
            }
            set
            {
                this.secureQuestion01Field = value;
            }
        }

        /// <remarks/>
        public string SecurePassword01
        {
            get
            {
                return this.securePassword01Field;
            }
            set
            {
                this.securePassword01Field = value;
            }
        }

        /// <remarks/>
        public string SecureQuestion02
        {
            get
            {
                return this.secureQuestion02Field;
            }
            set
            {
                this.secureQuestion02Field = value;
            }
        }

        /// <remarks/>
        public string SecurePassword02
        {
            get
            {
                return this.securePassword02Field;
            }
            set
            {
                this.securePassword02Field = value;
            }
        }

        /// <remarks/>
        public string ReceivingAddress1
        {
            get
            {
                return this.receivingAddress1Field;
            }
            set
            {
                this.receivingAddress1Field = value;
            }
        }

        /// <remarks/>
        public string ReceivingAddress2
        {
            get
            {
                return this.receivingAddress2Field;
            }
            set
            {
                this.receivingAddress2Field = value;
            }
        }

        /// <remarks/>
        public string ReceivingCity
        {
            get
            {
                return this.receivingCityField;
            }
            set
            {
                this.receivingCityField = value;
            }
        }

        /// <remarks/>
        public string ReceivingState
        {
            get
            {
                return this.receivingStateField;
            }
            set
            {
                this.receivingStateField = value;
            }
        }

        /// <remarks/>
        public string ReceivingZip
        {
            get
            {
                return this.receivingZipField;
            }
            set
            {
                this.receivingZipField = value;
            }
        }

        /// <remarks/>
        public string ReceivingCountry
        {
            get
            {
                return this.receivingCountryField;
            }
            set
            {
                this.receivingCountryField = value;
            }
        }

        /// <remarks/>
        public string ReceivingCountryCode
        {
            get
            {
                return this.receivingCountryCodeField;
            }
            set
            {
                this.receivingCountryCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prokarma.com/PF.xsd", IsNullable = false)]
    public partial class PmtSuppCCR
    {

        private object payeeTypeField;

        private object expDateField;

        private object merchantIDField;

        private object mCCCodeField;

        private object divisionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object PayeeType
        {
            get
            {
                return this.payeeTypeField;
            }
            set
            {
                this.payeeTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object ExpDate
        {
            get
            {
                return this.expDateField;
            }
            set
            {
                this.expDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object MerchantID
        {
            get
            {
                return this.merchantIDField;
            }
            set
            {
                this.merchantIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object MCCCode
        {
            get
            {
                return this.mCCCodeField;
            }
            set
            {
                this.mCCCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object Division
        {
            get
            {
                return this.divisionField;
            }
            set
            {
                this.divisionField = value;
            }
        }
    }
}

