using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector.UserManagement.Entities
{
    class UserManagementEntities
    {
    }

    public class VectorGetUserManagementInfo
    {
        public string Action { get; set; }
        public int Id { get; set; }
        public string SearchValue1 { get; set; }
        public string SearchValue2 { get; set; }
        public string SearchValue3 { get; set; }
        public string SearchValue4 { get; set; }
        public int UserId { get; set; }
    }

    public class VectorGetWorkManifestInfo
    {
        public string Action { get; set; }
        public int Id { get; set; }
        public string SearchText1 { get; set; }
        public string SearchText2 { get; set; }
        public int UserId { get; set; }
    }


    public class VactorMasterData
    {
        public string Action { get; set; }
        public string UserId { get; set; }
        public string ActionId { get; set; }
        public bool Status { get; set; }
    }

    public class VectorManageWorkManifestInfo
    {
        public string Action { get; set; }
        public List<Features> Features { get; set; }
        public int UserId { get; set; }
        public int CurrentUserId { get; set; }
    }

    public class VectorManagePersonaFeatures
    {
        public string Action { get; set; }
        public List<Features> Features { get; set; }
        public int PersonaMasterId { get; set; }
        public int CurrentUserId { get; set; }
    }

    public class Clients
    {
        public int ClientId { get; set; }
    }

    public class Properties
    {
        public int PropertyId { get; set; }
    }

    public class Features
    {
        public int FeatureID { get; set; }
    }

    public class VectorMangerUserInfo
    {
        public string Action { get; set; }
        public List<Clients> Clients { get; set; }
        public List<Properties> Properties { get; set; }
        public List<Features> Features { get; set; }
        public int UserId { get; set; }
        public int CurrentUserId { get; set; }
        public string LoginId { get; set; }
        public int? DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNbr { get; set; }
        public string TimeZone { get; set; }
        public string UserPersona { get; set; }
        public int? UserPersonaId { get; set; }
        public string AppIds { get; set; }
        public string CommIds { get; set; }

        public string Country { get; set; }
        public string UserType { get; set; }

        public string SecondPhone { get; set; }
        public string Password { get; set; }
        public string ProfilePic { get; set; }

        public string ringCentralUserName { get; set; }
        public string ringCentralPassword { get; set; }
        public string ringCentralExtension { get; set; }
        public Boolean? assignTaskAccess { get; set; }
        public Int64 reporterUserId { get; set; }

    }


    public class ManageResources
    {
        public string Action { get; set; }
        public string InvendoryIds { get; set; }
        public Int64 ExistingResourceId { get; set; }
        public Int64 NewResourceId { get; set; }
        public string ResourceType { get; set; }
    }

    public class UserFeatureNavigationLogDetail
    {
        public int previousFeatureId { get; set; }
        public int currentFeatureId { get; set; }
        public string previousUrl { get; set; }
        public string currentUrl { get; set; }
    }

    public class ChangePassword
    {
        public string loginId { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }

    public class UserLogDetail
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string UserRole { get; set; }
        public string SourceIP { get; set; }
        public string BrowserName { get; set; }
        public string Platform { get; set; }
        public string Version { get; set; }
        public string LoginType { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimeZone { get; set; }

    }
}
