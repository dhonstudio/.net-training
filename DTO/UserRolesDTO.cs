namespace traningday2.DTO
{
    public class UserRolesDTO
    {
        public string Username { get; set; }
        public int IDRole { get; set; }
        public string RoleName { get
            {
                return IDRole switch
                {
                    1 => "Admin",
                    2 => "Pengelola",
                    _ => "User"
                };
            }
        }
    }
}
