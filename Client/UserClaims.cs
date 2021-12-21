public class UserClaims
{
    public static CreateUserDTO? CreateUserOrDefault(ClaimsPrincipal userClaims)
    {
        if(userClaims.Identity?.IsAuthenticated == true)
        {
            var claims = userClaims.Claims;
            var email = claims.Where(c => c.Type == "preferred_username").SingleOrDefault();
            var name = claims.Where(c => c.Type == "name").SingleOrDefault();
            if(email != null && name != null)
            {
                var userDTO = new CreateUserDTO(name.ToString(), email.ToString());
                return userDTO;
            }
        }            
        return null;
    }
}