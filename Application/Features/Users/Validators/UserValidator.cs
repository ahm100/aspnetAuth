using FluentValidation;
//using PhoneNumbers;
using Domain.Entities.Concrete;

namespace Application.Features.Users.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            //RuleFor(x => x.DateOfBirth).NotEmpty().LessThan(DateTime.Now);
            //RuleFor(x => x.Cellphone).NotEmpty().Must(BeAValidPhoneNumber);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            //RuleFor(x => x.BankAccountNumber).NotEmpty();
            //RuleFor(x => x.Skills).NotEmpty();
            //RuleFor(x => x.Experience).NotEmpty();
        }

        //private bool BeAValidPhoneNumber(string phoneNumber)
        //{
        //    var phoneUtil = PhoneNumberUtil.GetInstance();
        //    try
        //    {
        //        //var numberProto = phoneUtil.Parse(phoneNumber, "US");
        //        var numberProto = phoneUtil.Parse(phoneNumber, null);
        //        return phoneUtil.IsValidNumber(numberProto);
        //    }
        //    catch (NumberParseException)
        //    {
        //        return false;
        //    }
        //}
    }
}
