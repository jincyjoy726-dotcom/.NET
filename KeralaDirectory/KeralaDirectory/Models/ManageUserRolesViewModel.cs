namespace KeralaDirectory.Models
{
    public class ManageUserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<RoleCheckBox> Roles { get; set; }
    }

    public class RoleCheckBox
    {
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}