using Microsoft.AspNetCore.Identity;

namespace HeadHunterVer1._0.Services;

public class RussianErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError()
        {
            Code = nameof(DuplicateEmail),
            Description = $"Этот email {email} уже занят . Используйте другой."
        };
    }

    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError()
        {
            Code = nameof(DuplicateUserName),
            Description = $"Это Имя пользователя {userName} уже занято. Используйте другой."
        };    }

    public override IdentityError PasswordMismatch()
    {
        return new IdentityError()
        {
            Code = nameof(PasswordMismatch),
            Description = $"Ваш старый пароль не подходит введите правильный старый пароль. После же новый который будет соотвествовать требованиям"
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