using Microsoft.AspNetCore.Identity;

namespace HeadHunterVer1._0.Services;

public class RussianErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError()
        {
            Code = nameof(DuplicateEmail),
            Description = $"Этот email уже занят. Используйте другой: {email}."
        };
    }

    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError()
        {
            Code = nameof(DuplicateUserName),
            Description = $"Это Имя пользователя уже занято. Используйте другой: {userName}."
        };    }

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError()
        {
            Code = nameof(PasswordTooShort),
            Description = $"Вы вводите слишком короткий пароль. Минимальная длина: {length}."
        };
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError()
        {
            Code = nameof(PasswordRequiresUpper),
            Description = $"пароль не содержит буквы в верхнем регистре смените пароль."
        };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError()
        {
            Code = nameof(PasswordRequiresLower),
            Description = $"пароль не содержит буквы в нижнем регистре смените пароль."
        };    }

   
      
    
}