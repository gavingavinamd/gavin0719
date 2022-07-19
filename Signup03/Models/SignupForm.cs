using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Signup03.Models
{
    public class SignupForm: TblSignup
    {
       
        public List<TblActiveItem>? ActiveItem { get; set; } //放入活動項目
        public List<string>? Activities { get; set; }//存放選擇的checkbox 的 Value  要對應到checkbox的名字
    }
}
